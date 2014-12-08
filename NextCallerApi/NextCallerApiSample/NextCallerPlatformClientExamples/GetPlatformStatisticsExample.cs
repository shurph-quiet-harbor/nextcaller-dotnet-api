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
	public static class GetPlatformStatisticsExample
	{
		public static void Run()
		{
			const string Username = "";
			const string Password = "";
			const bool Sandbox = true;

			NextCallerPlatformClient client = new NextCallerPlatformClient(Username, Password, Sandbox);

			try
			{
				PlatformStatistics stats = client.GetPlatformStatistics();

				//stats.PlatformUsers = new []
				//{
				//	new PlatformUser
				//	{
				//		CreatedTime = "2014­04­16T13:42:00",
				//		NumberOfOperations = 24,
				//		ResourceUri = "/api/v2/platform_users/pl2_un1/",
				//		Username = "pl2_un2",
				//		TotalCalls = new Dictionary<string, int>
				//		{
				//			{ "201403", 7 },
				//			{ "201404", 7 },
				//			{ "201405", 7 }
				//		},
				//		SuccessfulCalls = new Dictionary<string, int>
				//		{
				//			{ "201403", 7 },
				//			{ "201404", 7 },
				//			{ "201405", 7 }
				//		},
				//		BillableCalls = new Dictionary<string, int>
				//		{
				//			{ "201403", 7 },
				//			{ "201404", 7 },
				//			{ "201405", 7 }
				//		}
				//	}
				//};

				//stats.SuccessfulPlatformCalls = new Dictionary<string, int>
				//{
				//	{ "201403", 7 },
				//	{ "201404", 7 },
				//	{ "201405", 7 }
				//};

				//stats.TotalPlatformCalls = new Dictionary<string, int>
				//{
				//	{ "201403", 7 },
				//	{ "201404", 7 },
				//	{ "201405", 7 }
				//};

				//stats.BillablePlatformCalls = new Dictionary<string, int>
				//{
				//	{ "201403", 7 },
				//	{ "201404", 7 },
				//	{ "201405", 7 }
				//};

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
