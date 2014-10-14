using System;
using System.IO;
using System.Net;
using System.Text;

using Newtonsoft.Json;

using NextCallerApi.Authorization;
using NextCallerApi.Entities;
using NextCallerApi.Exceptions;
using NextCallerApi.Interfaces;


namespace NextCallerApi.Http
{
	internal class HttpTransport : IHttpTransport
	{

		private const string PostMethod = "POST";
		private const string GetMethod = "GET";

		private readonly string authorizationToken;

		public HttpTransport(string consumerKey, string consumerSecret)
		{
			authorizationToken = BasicAuthorization.GetToken(consumerKey, consumerSecret);
		}

		public string Request(string url, ContentType contentType, string data = null)
		{

			HttpWebRequest request = GetWebRequest(url, contentType);

			HttpWebResponse response = data == null ? Get(request) : Post(request, data);

			string responseContent;

			using (StreamReader stream = new StreamReader(response.GetResponseStream()))
			{
				responseContent = stream.ReadToEnd();
			}

			if (IsSuccessfulStatusCode(response.StatusCode))
			{
				return responseContent;
			}

			// Request was failed.
			// HttpException has to be thrown.

			string exceptionMessage;

			try
			{
				if (IsFailedPostRequest(response))
				{
					// Response might contain information about invalid data fields in POST request.
					// So here is an attempt to parse response.
					PostErrorResponse postError = TryParsePostError(responseContent);
					exceptionMessage = postError.ToString();
				}
				else
				{
					// Response might contain information about reason of failure.
					// So here is an attempt to parse response.
					ErrorResponse error = TryParseError(responseContent);
					exceptionMessage = error.ToString();
				}
			}
			catch (JsonException)
			{
				// Failed to parse response.
				// So using response content as an exception message.
				exceptionMessage = responseContent;
			}

			throw new HttpException(response.StatusCode, response.StatusDescription, exceptionMessage);

		}

		private static HttpWebResponse Post(WebRequest request, string data)
		{

			request.Method = PostMethod;
			byte[] bytes = Encoding.UTF8.GetBytes(data);
			request.ContentLength = bytes.Length;

			try
			{
				Stream requestStream = request.GetRequestStream();
				requestStream.Write(bytes, 0, bytes.Length);

				return (HttpWebResponse) request.GetResponse();
			}
			catch (WebException e)
			{
				return (HttpWebResponse) e.Response;
			}
		}

		private static HttpWebResponse Get(WebRequest request)
		{
			try
			{
				request.Method = GetMethod;
				return (HttpWebResponse) request.GetResponse();
			}
			catch (WebException e)
			{
				return (HttpWebResponse) e.Response;
			}
		}

		private HttpWebRequest GetWebRequest(string url, ContentType contentType)
		{
			HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(url);

			webRequest.Accept = GetHttpContentType(ContentType.Json) + ';' + GetHttpContentType(ContentType.Xml);
			webRequest.ContentType = GetHttpContentType(contentType);
			webRequest.Headers.Add(HttpRequestHeader.Authorization, authorizationToken);

			return webRequest;
		}

		private static PostErrorResponse TryParsePostError(string response)
		{
			return Serialization.JsonSerializer.ParsePostError(response);
		}

		private static ErrorResponse TryParseError(string response)
		{
			return Serialization.JsonSerializer.ParseError(response);
		}

		private static bool IsFailedPostRequest(HttpWebResponse response)
		{
			return response.Method.Equals(PostMethod, StringComparison.InvariantCultureIgnoreCase) && response.StatusCode == HttpStatusCode.BadRequest;
		}

		private static bool IsSuccessfulStatusCode(HttpStatusCode statusCode)
		{
			return (int) statusCode < 300;
		}

		private static string GetHttpContentType(ContentType contentType)
		{
			return contentType.GetDescription();
		}
	}
}
