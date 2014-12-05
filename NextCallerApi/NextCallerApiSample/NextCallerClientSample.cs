using System;
using System.Collections.Generic;

using NextCallerApi;
using NextCallerApi.Entities.Common;
using NextCallerApi.Exceptions;

using FormatException = NextCallerApi.Exceptions.FormatException;


namespace NextCallerApiSample
{
	public static class NextCallerClientSample
	{
		private const string RealPhone = "phone";
		private const string SandboxPhone = "fakePhone";
		private const string RealId = "fakeId";
		private const string SandboxId = "fakeId";

		private static readonly string username;
		private static readonly string password;

		static NextCallerClientSample()
		{
			username = "username";
			password = "password";
		}

		private static string GetPhone(bool sandbox = false)
		{
			return sandbox ? SandboxPhone : RealPhone;
		}

		private static string GetId(bool sandbox = false)
		{
			return sandbox ? SandboxId : RealId;
		}

		public static void GetProfileByIdSample(bool sandbox = false)
		{

			NextCallerClient client = new NextCallerClient(username, password, sandbox);

			try
			{
				Profile profile = client.GetByProfileId(GetId(sandbox));

				string firstName = profile.FirstName;
				string lastName = profile.LastName;
				string phoneNumber = profile.Phones[0].Number;
				// More fields available
			}
			catch (BadResponseException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.Message);
			}

		}

		public static void GetProfilesByPhoneSample(bool sandbox = false)
		{

			NextCallerClient client = new NextCallerClient(username, password, sandbox);

			try
			{
				IList<Profile> profiles = client.GetByPhone(GetPhone(sandbox));

				string firstName = profiles[0].FirstName;
				string lastName = profiles[0].LastName;
				string phoneNumber = profiles[0].Phones[0].Number;
				// More fields available
			}
			catch (BadResponseException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.Message);
			}

		}


		public static void PostProfileSample(bool sandbox = false)
		{
			NextCallerClient client = new NextCallerClient(username, password, sandbox);

			try
			{
				ProfileToPost profile = new ProfileToPost
				{
					Email = "email",
					FirstName = "Brand",
					LastName = "New"
				};

				client.UpdateByProfileId(GetId(sandbox), profile);
			}
			catch (BadResponseException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public static void GetProfileByIdJsonSample(bool sandbox = false)
		{

			NextCallerClient client = new NextCallerClient(username, password, sandbox);

			try
			{
				string profileInJson = client.GetByProfileIdJson(GetId(sandbox));
			}
			catch (BadResponseException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public static void GetProfilesByPhoneJsonSample(bool sandbox = false)
		{

			NextCallerClient client = new NextCallerClient(username, password, sandbox);

			try
			{
				string profilesInJson = client.GetByPhoneJson(GetPhone(sandbox));
			}
			catch (BadResponseException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public static void GetFraudLevelSample(bool sandbox = false)
		{

			NextCallerClient client = new NextCallerClient(username, password, sandbox);

			try
			{
				FraudLevel fraudLevel = client.GetFraudLevel(GetPhone(sandbox));
				string spoofed = fraudLevel.Spoofed;
				string fraudRisk = fraudLevel.FraudRisk;
			}
			catch (BadResponseException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public static void GetFraudLevelJsonSample(bool sandbox = false)
		{

			NextCallerClient client = new NextCallerClient(username, password, sandbox);

			try
			{
				string fraudLevel = client.GetFraudLevelJson(GetPhone(sandbox));
			}
			catch (BadResponseException e)
			{
				Console.WriteLine(e.Message);
			}
			catch (FormatException e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public static void Test()
		{
			GetProfileByIdSample();
			GetProfileByIdJsonSample();
			GetProfilesByPhoneSample();
			GetProfilesByPhoneJsonSample();
			GetFraudLevelSample();
			GetFraudLevelJsonSample();
		}

	}
}
