using System;
using System.Net;


namespace NextCallerApi.Exceptions
{
	/// <summary>
	/// Thrown in case of failed HTTP request.
	/// </summary>
	public class HttpException : Exception
	{
		private const string MessageTemplate = "Http Exception: {0} - {1}. Response: {2}";

		/// <summary>
		/// Failed HTTP request status code.
		/// </summary>
		public HttpStatusCode StatusCode
		{
			get;
			private set;
		}
		/// <summary>
		/// Failed HTTP request reason phrase.
		/// </summary>
		public string ReasonPhrase
		{
			get;
			private set;
		}
		/// <summary>
		/// Failed HTTP response content.
		/// </summary>
		public string Content
		{
			get;
			private set;
		}

		public override string Message
		{
			get
			{
				return ToString();
			}
		}

		/// <summary>
		/// Initializes HttpException instance.
		/// </summary>
		/// <param name="statusCode">Failed HTTP request status code.</param>
		/// <param name="reasonPhrase">Failed HTTP request status code.</param>
		/// <param name="content">Failed HTTP request content.</param>
		public HttpException(HttpStatusCode statusCode, string reasonPhrase, string content)
		{
			StatusCode = statusCode;
			ReasonPhrase = reasonPhrase;
			Content = content;
		}

		public override string ToString()
		{
			return string.Format(MessageTemplate, (int) StatusCode, ReasonPhrase, Content);
		}
	}
}
