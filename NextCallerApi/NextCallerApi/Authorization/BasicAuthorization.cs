using System;
using System.Text;


namespace NextCallerApi.Authorization
{
	internal class BasicAuthorization
	{

		private const string TokenTemplate = "Basic {0}";

		public static string GetToken(string consumerKey, string consumerSecret)
		{
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(consumerKey), "consumerKey");
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(consumerSecret), "consumerSecret");

			string tokenValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(consumerKey + ":" + consumerSecret));

			return string.Format(TokenTemplate, tokenValue);
		}

	}
}