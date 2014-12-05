using System;
using System.Collections.Generic;

using NextCallerApi;
using NextCallerApi.Entities.Common;
using NextCallerApi.Entities.Platform;
using NextCallerApi.Exceptions;

using FormatException = NextCallerApi.Exceptions.FormatException;

namespace NextCallerApiSample
{
	public static class NextCallerPlatformClientSample
	{
		public static bool Sandbox = true;

		public static string Username = "username";
		public static string Password = "password";

		public static string Phone = "phone";
		public static string ProfileId = "profileId";

		public static string PlatformUsername = "platformUsername";

		public static NextCallerPlatformClient Client;

		static NextCallerPlatformClientSample()
		{
			Client = new NextCallerPlatformClient(Username, Password, Sandbox);
		}

		public static void GetByPhone()
		{
			try
			{
				IList<Profile> profiles = Client.GetByPhone(Phone, PlatformUsername);
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

		public static void GetByPhoneJson()
		{
			try
			{
				string profiles = Client.GetByPhoneJson(Phone, PlatformUsername);
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

		public static void GetByProfielId()
		{
			try
			{
				Profile profile = Client.GetByProfileId(ProfileId, PlatformUsername);
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

		public static void GetByProfileIdJson()
		{
			try
			{
				string profile = Client.GetByProfileIdJson(ProfileId, PlatformUsername);
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

		public static void GetFraudLevel()
		{
			try
			{
				FraudLevel fraudLevel = Client.GetFraudLevel(Phone, PlatformUsername);
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

		public static void GetFraudLevelJson()
		{
			try
			{
				string fraudLevel = Client.GetFraudLevelJson(Phone, PlatformUsername);
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

		public static void GetPlatformUser()
		{
			try
			{
				PlatformUser user = Client.GetPlatformUser(PlatformUsername);
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

		public static void GetPlatformUserJson()
		{
			try
			{
				string platformUser = Client.GetPlatformUserJson(PlatformUsername);
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

		public static void GetPlatformStatistics()
		{
			try
			{
				PlatformStatistics statistics = Client.GetPlatformStatistics();
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

		public static void GetPlatformStatisticsJson()
		{
			try
			{
				string stats = Client.GetPlatformStatisticsJson();
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

		public static void PostProfile()
		{
			try
			{
				ProfileToPost profile = new ProfileToPost
				{
					Email = "cat@email.com",
					LastName = "cat"
				};
				Client.UpdateByProfileId(ProfileId, profile, PlatformUsername);
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

		public static void PostPlatformUser()
		{
			try
			{
				PlatformUserToPost user = new PlatformUserToPost
				{
					Email = "cat@email.com",
					LastName = "cat"
				};
				Client.UpdatePlatformUser(PlatformUsername, user);
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
			GetByPhone();
			GetByPhoneJson();
			GetByProfielId();
			GetByProfileIdJson();
			GetFraudLevel();
			GetFraudLevelJson();
			GetPlatformUser();
			GetPlatformUserJson();
			GetPlatformStatistics();
			GetPlatformStatisticsJson();
			PostProfile();
			PostPlatformUser();
		}
	}
}
