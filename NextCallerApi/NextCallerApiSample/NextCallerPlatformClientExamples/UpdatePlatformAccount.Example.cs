using System;
using System.Collections.Generic;
using System.Net;

using NextCallerApi;
using NextCallerApi.Entities;
using NextCallerApi.Entities.Platform;
using NextCallerApi.Exceptions;

using FormatException = NextCallerApi.Exceptions.FormatException;


namespace NextCallerApiSample.NextCallerPlatformClientExamples
{
	public static class UpdatePlatformAccount
	{
		public static void Run()
		{
			const string Username = "";
			const string Password = "";
			const bool Sandbox = true;

			NextCallerPlatformClient client = new NextCallerPlatformClient(Username, Password, Sandbox);

			const string AccountId = "TestUser1";

			try
			{
				PlatformAccountToPost user = new PlatformAccountToPost
				{
                    Id = "NewTestUser1",
                    CompanyName = "new_platform_company1_name",
					Email = "new_company_email@company1.com",
					FirstName = "new_platform_user1_fname",
					LastName = "new_platform_user1_lname"
				};

				client.UpdatePlatformAccount(user, AccountId);

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
