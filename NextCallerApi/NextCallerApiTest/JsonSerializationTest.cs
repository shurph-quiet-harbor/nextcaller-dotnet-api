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
			Assert.AreEqual("demo@nextcaller.com", profiles[0].Email);

			Assert.IsNotNull(profiles[0].Addresses);
			Assert.AreEqual(1, profiles[0].Addresses.Length);
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
			Assert.AreEqual("demo@nextcaller.com", profile.Email);

			Assert.IsNotNull(profile.Addresses);
			Assert.AreEqual(1, profile.Addresses.Length);
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
			PlatformUser user = JsonSerializer.Deserialize<PlatformUser>(json);


			//Assert
			Assert.IsNotNull(user);

			Assert.AreEqual("2014­04­16T13:42:00", user.CreatedTime);
			Assert.AreEqual(24, user.NumberOfOperations);
			Assert.AreEqual("user12345", user.Username);
			Assert.AreEqual("/api/v2/platform_users/user12345/", user.ResourceUri);

			Assert.IsNotNull(user.TotalCalls);
			Assert.AreEqual(3, user.TotalCalls.Count);
			Assert.AreEqual(7, user.TotalCalls["201403"]);
			Assert.AreEqual(12, user.TotalCalls["201404"]);
			Assert.AreEqual(10, user.TotalCalls["201405"]);

			Assert.IsNotNull(user.SuccessfulCalls);
			Assert.AreEqual(3, user.SuccessfulCalls.Count);
			Assert.AreEqual(6, user.SuccessfulCalls["201403"]);
			Assert.AreEqual(10, user.SuccessfulCalls["201404"]);
			Assert.AreEqual(8, user.SuccessfulCalls["201405"]);

			Assert.IsNotNull(user.BillableCalls);
			Assert.AreEqual(2, user.BillableCalls.Count);
			Assert.AreEqual(1, user.BillableCalls["201404"]);
			Assert.AreEqual(1, user.BillableCalls["201405"]);

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

			Assert.IsNotNull(stats.TotalPlatformCalls);
			Assert.AreEqual(3, stats.TotalPlatformCalls.Count);
			Assert.AreEqual(7, stats.TotalPlatformCalls["201403"]);
			Assert.AreEqual(12, stats.TotalPlatformCalls["201404"]);
			Assert.AreEqual(2, stats.TotalPlatformCalls["201405"]);

			Assert.IsNotNull(stats.SuccessfulPlatformCalls);
			Assert.AreEqual(3, stats.SuccessfulPlatformCalls.Count);
			Assert.AreEqual(6, stats.SuccessfulPlatformCalls["201403"]);
			Assert.AreEqual(10, stats.SuccessfulPlatformCalls["201404"]);
			Assert.AreEqual(9, stats.SuccessfulPlatformCalls["201405"]);

			Assert.IsNotNull(stats.BillablePlatformCalls);
			Assert.AreEqual(2, stats.BillablePlatformCalls.Count);
			Assert.AreEqual(1, stats.BillablePlatformCalls["201404"]);
			Assert.AreEqual(1, stats.BillablePlatformCalls["201405"]);
			
			Assert.IsNotNull(stats.PlatformUsers);
			Assert.AreEqual(2, stats.PlatformUsers.Length);

			Assert.AreEqual("2014­03­16T10:40:12", stats.PlatformUsers[0].CreatedTime);
			Assert.AreEqual(1, stats.PlatformUsers[0].NumberOfOperations);
			Assert.AreEqual("pl2_un1", stats.PlatformUsers[0].Username);
			Assert.AreEqual("/api/v2/platform_users/pl2_un2/", stats.PlatformUsers[0].ResourceUri);

			Assert.IsNotNull(stats.PlatformUsers[0].TotalCalls);
			Assert.AreEqual(1, stats.PlatformUsers[0].TotalCalls.Count);
			Assert.AreEqual(2, stats.PlatformUsers[0].TotalCalls["201405"]);

			Assert.IsNotNull(stats.PlatformUsers[0].SuccessfulCalls);
			Assert.AreEqual(1, stats.PlatformUsers[0].SuccessfulCalls.Count);
			Assert.AreEqual(1, stats.PlatformUsers[0].SuccessfulCalls["201405"]);

			Assert.IsNotNull(stats.PlatformUsers[0].BillableCalls);
			Assert.AreEqual(0, stats.PlatformUsers[0].BillableCalls.Count);

			Assert.AreEqual("2014­04­16T13:42:00", stats.PlatformUsers[1].CreatedTime);
			Assert.AreEqual(24, stats.PlatformUsers[1].NumberOfOperations);
			Assert.AreEqual("pl2_un2", stats.PlatformUsers[1].Username);
			Assert.AreEqual("/api/v2/platform_users/pl2_un1/", stats.PlatformUsers[1].ResourceUri);

			Assert.IsNotNull(stats.PlatformUsers[1].TotalCalls);
			Assert.AreEqual(3, stats.PlatformUsers[1].TotalCalls.Count);
			Assert.AreEqual(7, stats.PlatformUsers[1].TotalCalls["201403"]);
			Assert.AreEqual(12, stats.PlatformUsers[1].TotalCalls["201404"]);
			Assert.AreEqual(10, stats.PlatformUsers[1].TotalCalls["201405"]);

			Assert.IsNotNull(stats.PlatformUsers[1].SuccessfulCalls);
			Assert.AreEqual(3, stats.PlatformUsers[1].SuccessfulCalls.Count);
			Assert.AreEqual(6, stats.PlatformUsers[1].SuccessfulCalls["201403"]);
			Assert.AreEqual(10, stats.PlatformUsers[1].SuccessfulCalls["201404"]);
			Assert.AreEqual(8, stats.PlatformUsers[1].SuccessfulCalls["201405"]);

			Assert.IsNotNull(stats.PlatformUsers[1].BillableCalls);
			Assert.AreEqual(2, stats.PlatformUsers[1].BillableCalls.Count);
			Assert.AreEqual(1, stats.PlatformUsers[1].BillableCalls["201404"]);
			Assert.AreEqual(1, stats.PlatformUsers[1].BillableCalls["201405"]);
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
