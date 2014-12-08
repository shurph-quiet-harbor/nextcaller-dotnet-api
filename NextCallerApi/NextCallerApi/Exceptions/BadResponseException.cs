using System;
using System.Net;

using NextCallerApi.Entities;


namespace NextCallerApi.Exceptions
{
	/// <summary>
	/// Thrown in case of failed request and parsed response error.
	/// </summary>
	public class BadResponseException : BaseException
	{

		/// <summary>
		/// Parsed response.
		/// </summary>
		public Error Error
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
		/// Initializes BadResponseException instance.
		/// </summary>
		/// <param name="request">Failed request.</param>
		/// <param name="response">Response.</param>
		/// <param name="content">Response content.</param>
		/// <param name="error"> Parsed response content.</param>
		public BadResponseException(HttpWebRequest request, HttpWebResponse response, string content, Error error) : base(request, response, content)
		{
			Error = error;
		}

		public override string ToString()
		{
			string template = "Bad Response Exception: {0} - {1}." + Environment.NewLine + "{2}";

			return string.Format(template, (int) Response.StatusCode, Response.StatusDescription, Error);
		}
	}
}
