using System;
using System.Collections.Generic;

using NextCallerApi;
using NextCallerApi.Entities;
using NextCallerApi.Exceptions;


namespace NextCallerApiSample
{
	class Program
	{
		private static readonly string consumerKey;
		private static readonly string consumerSecret;

		static Program()
		{
			consumerKey = "consumerKey";
			consumerSecret = "consumerSecret";
		}

		private static void GetProfileByIdSample()
		{

			Client client = new Client(consumerKey, consumerSecret);

			try
			{
				Profile profile = client.GetProfileById("profileId");

				string firstName = profile.FirstName;
				string lastName = profile.LastName;
				string phoneNumber = profile.Phones[0].Number;
			}
			catch (HttpException e)
			{
				Console.WriteLine(e.Message);
			}

		}

		private static void GetProfilesByPhoneSample()
		{

			Client client = new Client(consumerKey, consumerSecret);

			try
			{
				IList<Profile> profiles = client.GetProfilesByPhone("profilePhone");

				string firstName = profiles[0].FirstName;
				string lastName = profiles[0].LastName;
				string phoneNumber = profiles[0].Phones[0].Number;
			}
			catch (HttpException e)
			{
				Console.WriteLine(e.Message);
			}

		}


		private static void PostProfileSample()
		{
			Client client = new Client(consumerKey, consumerSecret);

			try
			{
				ProfilePostRequest profile = new ProfilePostRequest
				{
					Email = "newemail@emails.com",
					FirstName = "Brand",
					LastName = "New"
				};

				client.PostProfile(profile, "profileId");
			}
			catch (HttpException e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private static void GetProfileByIdJsonSample()
		{

			Client client = new Client(consumerKey, consumerSecret);

			try
			{
				string profileInJson = client.GetProfileByIdJson("profileId");
			}
			catch (HttpException e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private static void GetProfilesByPhoneJsonSample()
		{

			Client client = new Client(consumerKey, consumerSecret);

			try
			{
				string profilesInJson = client.GetProfilesByPhoneJson("profilePhone");
			}
			catch (HttpException e)
			{
				Console.WriteLine(e.Message);
			}
		}

		static void Main()
		{

		}
	}
}
