using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using NextCallerApi;
using NextCallerApi.Entities;
using NextCallerApi.Entities.Common;
using NextCallerApi.Exceptions;

using FormatException = NextCallerApi.Exceptions.FormatException;


namespace NextCallerApiSample.NextCallerClientExamples
{
    public static class GetByNameAddressExample
    {
        public static void Run()
        {
            const string Username = "";
            const string Password = "";
            const bool Sandbox = true;
            const string Version = "2.1";

            NextCallerClient client = new NextCallerClient(Username, Password, Sandbox, Version);

            NameAddress nameAddress = new NameAddress
            {
                AddressLine = "129 West 81st Street",
                FirstName = "Jerry",
                LastName = "Seinfeld",
                City = "New York",
                State = "NY"
            };

            try
            {
                IList<Profile> profiles = client.GetByNameAddress(nameAddress);

                Profile profile = profiles.First();

                //profile.Id = "97d949a413f4ea8b85e9586e1f2d9a";
                //profile.FirstName = "Jerry";
                //profile.LastName = "Seinfeld";
                //profile.Name = "Jerry K Seingeld";
                //profile.Language = "English";

                //Phone phone = profile.Phones.First();
                //phone.Number = "2125558383";
                //phone.ResourceUri = "/v2/records/2125558383/";

                //profile.Carrier = "Verizon Wireless";
                //profile.LineType = "LAN";

                //Address address = profile.Addresses.First();
                //address.City = "New York";
                //address.State = "NY";
                //address.Country = "USA";
                //address.Line1 = "129 West 81st Street";
                //address.Line2 = "Apt 5a";
                //address.ZipCode = "10024";
                //address.ExtendedZip = "";

                //profile.Email = "demo@nextcaller.com";
                //profile.LinkedEmails = new[]
                //{
                //	"demo@nextcaller.com", "demo@gmail.com", "demo@yahoo.com"
                //};
                //profile.SocialLinks = new[]
                //{
                //	new Dictionary<string, object>()
                //	{
                //		{ "type", "twitter" },
                //		{ "url", "http://www.twitter.com/example" },
                //		{ "followers", 323 }
                //	}
                //};
                //profile.Relatives = new []
                //{
                //	new Relative
                //	{
                //		Id = "a4bf8cff1bfd61d8611d396997bb29",
                //		Name = "Clark Kent",
                //		ResourceUri = "/v2/users/90c6a00c6391421d3a764716927cd8/"
                //	}
                //};
                //profile.DateOfBirth = "05/15/1970";
                //profile.Gender = "Male";
                //profile.Age = "45-54";
                //profile.HouseholdIncome = "50k-75k";
                //profile.HomeOwnerStatus = "Rent";
                //profile.LengthOfResidence = "12 Years";
                //profile.Occupation = "Entertainer";
                //profile.Education = "Completed College";
                //profile.ResourceUri = "/v2/users/97d949a413f4ea8b85e9586e1f2d9a/";


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
