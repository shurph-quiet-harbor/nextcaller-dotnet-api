using System;
using System.Net;


namespace NextCallerApi.Exceptions
{
	/// <summary>
	/// Thrown in case of failed HTTP request.
	/// </summary>
	public class BaseException : Exception
	{

		/// <summary>
		/// Failed HTTP response content.
		/// </summary>
		public string Content
		{
			get;
			private set;
		}

		/// <summary>
		/// Failed request.
		/// </summary>
		public HttpWebRequest Request
		{
			get;
			private set; 
		}

		/// <summary>
		/// Failed response.
		/// </summary>
		public HttpWebResponse Response
		{
			get;
			private set;
		}

		/// <summary>
		/// Initializes BaseException instance.
		/// </summary>
		/// <param name="request">Failed request.</param>
		/// <param name="response">Response.</param>
		/// <param name="content">Response content.</param>
		public BaseException( HttpWebRequest request, HttpWebResponse response, string content)
		{
			Content = content;
			Request = request;
			Response = response;
		}
	}
}
