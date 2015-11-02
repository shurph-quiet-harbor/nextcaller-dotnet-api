﻿using System;
using System.Collections.Generic;
using System.Net;

using NextCallerApi;
using NextCallerApi.Entities;
using NextCallerApi.Entities.Common;
using NextCallerApi.Exceptions;

using FormatException = NextCallerApi.Exceptions.FormatException;


namespace NextCallerApiSample.NextCallerClientExamples
{
	public class UpdateByProfileId
	{
		public static void Run()
		{
			const string Username = "";
			const string Password = "";
			const bool Sandbox = true;

			NextCallerClient client = new NextCallerClient(Username, Password, Sandbox);

			const string ProfileId = "profileId";

			try
			{
				ProfileToPost profile = new ProfileToPost
				{
					Email = "clarkkent@supermail.com",
					FirstName = "Clark",
					LastName = "Kent",
					Phone1 = "1234567890",
					Phone2 = "2345678901",
					Phone3 = "3456789012",
					PrimaryAddress = new Address
					{
						Line1 = "223 Kryptonite Ave.",
						Line2 = "",
						City = "Smallville",
						State = "KS",
						ZipCode = "66002"
					},
					SecondaryAddress = new Address
					{
						Line1 = "223 Kryptonite Ave.",
						Line2 = "",
						City = "Smallville",
						State = "KS",
						ZipCode = "66002"
					},
				};

				client.UpdateByProfileId(ProfileId, profile);

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
			catch (BadRequestException badRequestException)
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
