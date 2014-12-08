using System;
using System.Collections.Generic;
using System.Net;

using NextCallerApi;
using NextCallerApi.Entities;
using NextCallerApi.Entities.Common;
using NextCallerApi.Exceptions;

using FormatException = NextCallerApi.Exceptions.FormatException;


namespace NextCallerApiSample.NextCallerPlatformClientExamples
{
	public static class GetFraudLevelExample
	{
		public static void Run()
		{
			const string Username = "";
			const string Password = "";
			const bool Sandbox = true;

			NextCallerPlatformClient client = new NextCallerPlatformClient(Username, Password, Sandbox);

			const string Phone = "1234567890";
			const string PlatformUsername = "username";

			try
			{
				FraudLevel fraudLevel = client.GetFraudLevel(Phone, PlatformUsername);

				//fraudLevel.FraudRisk = "low";
				//fraudLevel.Spoofed = "no";

			}
			catch (FormatException formatException)
			{

				HttpWebRequest request = formatException.Request;
				HttpWebResponse response = formatException.Response;

				HttpStatusCode code = response.StatusCode;
				Console.WriteLine("Status code: {0}", code);

				string reasonPhrase = response.StatusDescription;
				Console.WriteLine("Reason Phrase: {0}", reasonPhrase);

				string responseContent = formatException.Content;
				Console.WriteLine("Content : {0}", responseContent);

			}
			catch (BadResponseException badRequestException)
			{

				HttpWebRequest request = badRequestException.Request;
				HttpWebResponse response = badRequestException.Response;

				Error parsedError = badRequestException.Error;

				string errorCode = parsedError.Code;
				string message = parsedError.Message;
				string type = parsedError.Type;

				Dictionary<string, string[]> description = parsedError.Description;

				Console.WriteLine(parsedError.ToString());

			}

		}
	}
}
