using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;

using Newtonsoft.Json;

using NextCallerApi.Authorization;
using NextCallerApi.Entities;
using NextCallerApi.Exceptions;
using NextCallerApi.Interfaces;

using FormatException = NextCallerApi.Exceptions.FormatException;
using Header = NextCallerApi.Entities.Common.Header;


namespace NextCallerApi.Http
{
	internal class HttpTransport : IHttpTransport
	{

        private const string GetMethod = "GET";
        private const string PostMethod = "POST";
        private const string PutMethod = "PUT";
        private readonly string _paginationErrorCode;

		private readonly string _authorizationToken;

		public HttpTransport(string username, string password)
		{
			_paginationErrorCode = Properties.Resources.PaginationExceptionCode;
			_authorizationToken = BasicAuthorization.GetToken(username, password);
		}

		public string Request(string url, ContentType contentType, string method = "GET", string data = null, IEnumerable<Header> headers = null)
		{

			HttpWebRequest request = CreateWebRequest(url, contentType, headers);

		    Func<HttpWebRequest, string, HttpWebResponse> methodFunc;

		    switch (method)
		    {
		        case "GET":
		            methodFunc = Get;
		            break;
                case "POST":
		            methodFunc = Post;
		            break;
                case "PUT":
                    methodFunc = Put;
                    break;
                default:
		            methodFunc = Get;
		            break;
		    }

//            MethodInfo methodInvoker = GetType().GetMethod(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(method.ToLower()), BindingFlags.NonPublic | BindingFlags.Instance);
//		    if (methodInvoker == null)
//		    {
//		        throw new BadMethodException(method);
//		    }
//
//		    HttpWebResponse response = methodInvoker.Invoke(this, new object[]{request, data}) as HttpWebResponse;

		    var response = methodFunc(request, data);

			string responseContent;

			using (StreamReader stream = new StreamReader(response.GetResponseStream()))
			{
				responseContent = stream.ReadToEnd();
			}

			if (IsSuccessfulStatusCode(response.StatusCode))
			{
				return responseContent;
			}

			Error requestError;

		    if ((int) response.StatusCode == 429)
		    {
                throw new RateLimitExceededException(request, response, responseContent);
		    }

			try
			{
				requestError = Serialization.JsonSerializer.ParseError(responseContent);
			}
			catch (JsonException)
			{
				throw new FormatException(request, response, responseContent);
			}

			if (requestError.Code == _paginationErrorCode)
			{
				throw new PaginationException(request, response, responseContent);
			}

			throw new BadRequestException(request, response, responseContent, requestError);

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

        private static HttpWebResponse Put(WebRequest request, string data)
        {

            request.Method = PutMethod;
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            request.ContentLength = bytes.Length;

            try
            {
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);

                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException e)
            {
                return (HttpWebResponse)e.Response;
            }
        }

        private static HttpWebResponse Get(WebRequest request, string data)
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

        private HttpWebRequest CreateWebRequest(string url, ContentType contentType, IEnumerable<Header> headers = null)
		{
			HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(url);

			webRequest.Accept = GetHttpContentType(ContentType.Json);
			webRequest.ContentType = GetHttpContentType(contentType);
			webRequest.Headers.Add(HttpRequestHeader.Authorization, _authorizationToken);
            if (headers == null) return webRequest;
            foreach (var header in headers)
            {
                webRequest.Headers.Add(header.Name, header.Value);
            }

            return webRequest;
		}

		private static bool IsSuccessfulStatusCode(HttpStatusCode statusCode)
		{
			return (int) statusCode < 400;
		}

		private static string GetHttpContentType(ContentType contentType)
		{
			return contentType.GetDescription();
		}
	}
}
