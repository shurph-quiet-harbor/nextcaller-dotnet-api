using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NextCallerApi.Entities;
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
			Profile profile = JsonSerializer.ParseProfile(json);


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
			ProfilePostRequest profile = new ProfilePostRequest
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

	}
}
