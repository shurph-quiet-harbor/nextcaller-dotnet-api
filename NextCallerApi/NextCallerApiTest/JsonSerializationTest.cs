using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NextCallerApi.Entities;
using NextCallerApi.Entities.Common;
using NextCallerApi.Entities.Platform;
using NextCallerApi.Serialization;


namespace NextCallerApiTest
{
	[TestClass]
	public class JsonSerializationTest
	{
		[TestMethod]
		public void JsonParse_NextCallerDocsProfileSample_OneProfileSuccessfullyParsed()
		{
			//Arrange
			//json response from https://dev.nextcaller.com/documentation/get-profile/#get-profile-json-1
			string json = Properties.Resources.JsonProfiles;


			//Action
			IList<Profile> profiles = JsonSerializer.ParseProfileList(json);


			//Assert
			Assert.IsNotNull(profiles);
			Assert.AreEqual(profiles.Count, 1);

			Assert.AreEqual("Jerry", profiles[0].FirstName);
			Assert.AreEqual("Seinfeld", profiles[0].LastName);
			Assert.AreEqual("jerry@example.org", profiles[0].Email);

			Assert.IsNotNull(profiles[0].Addresses);
			Assert.AreEqual(2, profiles[0].Addresses.Length);
			Assert.AreEqual("USA", profiles[0].Addresses[0].Country);

			Assert.IsNotNull(profiles[0].Phones);
			Assert.AreEqual(1, profiles[0].Phones.Length);
			Assert.AreEqual("2125558383", profiles[0].Phones[0].Number);
		}

		[TestMethod]
		public void JsonParse_SingleProfile_ProfileSuccessfullyParsed()
		{
			//Arrange
			string json = Properties.Resources.JsonProfile;


			//Action
			Profile profile = JsonSerializer.Deserialize<Profile>(json);


			//Assert
			Assert.IsNotNull(profile);

			Assert.AreEqual("Jerry", profile.FirstName);
			Assert.AreEqual("Seinfeld", profile.LastName);
			Assert.AreEqual("jerry@example.org", profile.Email);

			Assert.IsNotNull(profile.Addresses);
			Assert.AreEqual(2, profile.Addresses.Length);
			Assert.AreEqual("USA", profile.Addresses[0].Country);

			Assert.IsNotNull(profile.Phones);
			Assert.AreEqual(1, profile.Phones.Length);
			Assert.AreEqual("2125558383", profile.Phones[0].Number);
		}

		[TestMethod]
		public void JsonSerialize_ProfileContainsNameAddressPhone_JsonContainsNameAddressPhone()
		{
			//Arrange
			ProfileToPost profile = new ProfileToPost
			{
				FirstName = "Ivan",
				LastName = "Petrov",
				PrimaryAddress = new Address
					{
						Country = "Russia",
						City = "Omsk"
					},
				Phone1 = "2020327"
			};


			//Action
			string json = JsonSerializer.Serialize(profile);


			//Assert
			Assert.IsNotNull(json);

			Assert.IsTrue(json.Contains("\"first_name\":\"Ivan\""));
			Assert.IsTrue(json.Contains("\"last_name\":\"Petrov\""));
			Assert.IsTrue(json.Contains("\"phone1\":\"2020327\""));
			Assert.IsTrue(json.Contains("\"shipping_address1\":{\"country\":\"Russia\",\"city\":\"Omsk\"}"));

		}

		[TestMethod]
		public void JsonParse_NextCallerDocsFraudLevelSample_FraudLevelSuccessfullyParsed()
		{
			//Arrange
			string json = Properties.Resources.JsonFraudLevel;


			//Action
			FraudLevel fraudLevel = JsonSerializer.Deserialize<FraudLevel>(json);


			//Assert
			Assert.IsNotNull(fraudLevel);
			Assert.AreEqual("low", fraudLevel.FraudRisk);
			Assert.AreEqual("false", fraudLevel.Spoofed);

		}

		[TestMethod]
		public void JsonParse_NextCallerDocsPlatformUserSampe_PlatformUserSuccessfullyParsed()
 		{
			//Arrange
			string json = Properties.Resources.JsonPlatformUser;


			//Action
			PlatformAccount user = JsonSerializer.Deserialize<PlatformAccount>(json);


			//Assert
			Assert.IsNotNull(user);

            Assert.AreEqual("test_platform_username1", user.Id);
            Assert.AreEqual("John", user.FirstName);
            Assert.AreEqual("Doe", user.LastName);
            Assert.AreEqual("NC*", user.CompanyName);
            Assert.AreEqual("test1@test.com", user.Email);
            Assert.AreEqual("/api/v2.1/accounts/test_platform_username1/", user.ResourceUri);
            Assert.AreEqual(5, user.NumberOfOperations);

            Assert.IsNotNull(user.TotalOperations);
			Assert.AreEqual(5, user.TotalOperations["2015-07"]);


			Assert.IsNotNull(user.BilledOperations);
			Assert.AreEqual(5, user.BilledOperations["2015-07"]);
		}

		[TestMethod]
		public void JsonParse_NextCallerDocsPlatformStatisticsSampe_PlatformStatisticsSuccessfullyParsed()
		{
			//Arrange
			string json = Properties.Resources.JsonPlatformStatistics;

			//Action
			PlatformStatistics stats = JsonSerializer.Deserialize<PlatformStatistics>(json);

			//Assert
			Assert.IsNotNull(stats);

			Assert.IsNotNull(stats.TotalPlatformOperations);
			Assert.AreEqual(59, stats.TotalPlatformOperations["2015-07"]);

			Assert.IsNotNull(stats.BilledPlatformOperations);
			Assert.AreEqual(59, stats.BilledPlatformOperations["2015-07"]);
			
			Assert.IsNotNull(stats.PlatformAccounts);
			Assert.AreEqual(2, stats.PlatformAccounts.Length);

            Assert.AreEqual("test_platform_username1", stats.PlatformAccounts[0].Id);
            Assert.AreEqual("John", stats.PlatformAccounts[0].FirstName);
            Assert.AreEqual("Doe", stats.PlatformAccounts[0].LastName);
            Assert.AreEqual("NC*", stats.PlatformAccounts[0].CompanyName);
            Assert.AreEqual("test1@test.com", stats.PlatformAccounts[0].Email);
            Assert.AreEqual("/api/v2.1/accounts/test_platform_username1/", stats.PlatformAccounts[0].ResourceUri);
            Assert.AreEqual(5, stats.PlatformAccounts[0].NumberOfOperations);

            Assert.IsNotNull(stats.PlatformAccounts[0].TotalOperations);
            Assert.AreEqual(5, stats.PlatformAccounts[0].TotalOperations["2015-07"]);

            Assert.IsNotNull(stats.PlatformAccounts[0].BilledOperations);
            Assert.AreEqual(5, stats.PlatformAccounts[0].BilledOperations["2015-07"]);

            Assert.AreEqual("me", stats.PlatformAccounts[1].Id);
            Assert.AreEqual("", stats.PlatformAccounts[1].FirstName);
            Assert.AreEqual("", stats.PlatformAccounts[1].LastName);
            Assert.AreEqual("", stats.PlatformAccounts[1].CompanyName);
            Assert.AreEqual("", stats.PlatformAccounts[1].Email);
            Assert.AreEqual("/api/v2.1/accounts/me/", stats.PlatformAccounts[1].ResourceUri);
            Assert.AreEqual(4, stats.PlatformAccounts[1].NumberOfOperations);

            Assert.IsNotNull(stats.PlatformAccounts[1].TotalOperations);
            Assert.AreEqual(4, stats.PlatformAccounts[1].TotalOperations["2015-07"]);

            Assert.IsNotNull(stats.PlatformAccounts[1].BilledOperations);
            Assert.AreEqual(4, stats.PlatformAccounts[1].BilledOperations["2015-07"]);
        }

		[TestMethod]
		public void JsonParse_NextCallerDocsErrorSample_ErrorSuccessfullyParsed()
		{
			//Arrange
			string json = Properties.Resources.JsonError;


			//Action
			Error error = JsonSerializer.ParseError(json);


			//Assert
			Assert.IsNotNull(error);

			Assert.AreEqual("There are validation errors:", error.Message);
			Assert.AreEqual("1054", error.Code);
			Assert.AreEqual("Validation", error.Type);

			Assert.IsNotNull(error.Description);
			Assert.AreEqual(1, error.Description.Count);
			Assert.AreEqual("email", error.Description.Keys.First());
			Assert.AreEqual("Invalid email address", error.Description.Values.First()[0]);

		}
	}

}
