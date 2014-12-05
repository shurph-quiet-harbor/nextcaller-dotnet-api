using System;
using System.Net;


namespace NextCallerApi.Exceptions
{
	/// <summary>
	/// Thrown when the request was failed and response could not be parsed.
	/// </summary>
	public class FormatException: BaseException
	{
		/// <summary>
		/// Initializes FormatException
		/// </summary>
		/// <param name="request">Failed request</param>
		/// <param name="response">Response</param>
		/// <param name="content">Response content</param>
		public FormatException(HttpWebRequest request, HttpWebResponse response, string content) : base(request, response, content)
		{
		}

		public override string Message
		{
			get
			{
				return ToString();
			}
		}

		public override string ToString()
		{
			string template = "Format Exception: {0} - {1}." + Environment.NewLine + "{2}";

			return string.Format(template, (int) Response.StatusCode, Response.StatusDescription, Content);
		}
	}
}
