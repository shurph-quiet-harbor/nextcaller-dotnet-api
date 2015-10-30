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
    public static class AnalyzeCallExample
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
                AnalyzeCallData callData = new AnalyzeCallData
                {
                    Ani = "12125551212",
                    Dnis = "18005551212",
                    Headers = new Dictionary<string, object>
                    {
                        { "from", "\"John Smith\" <sip:12125551212@example.com>" },
                        { "via", new List<string> { "SIP/2.0//UDP 1.1.1.1:5060;branch=z9hG4bK3fe1.9a945462b4c1880c5f6fdc0214a205ca.1"} }
                    },
                    Meta = new Dictionary<string, string>
                    {
                        { "caller_id", "12125551212" },
                        { "charge_number", "12125551212" },
                        { "ani2", "0" },
                        { "private", "true" }
                    }
                };

                FraudLevel fraudLevel = client.AnalyzeCall(callData, AccountId);
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
