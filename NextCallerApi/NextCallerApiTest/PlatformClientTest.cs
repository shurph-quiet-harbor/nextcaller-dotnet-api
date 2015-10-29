using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using NextCallerApi;
using NextCallerApi.Entities.Common;
using NextCallerApi.Entities.Platform;
using NextCallerApi.Interfaces;


namespace NextCallerApiTest
{
	[TestClass]
	public class PlatformClientTest
	{
        [TestMethod]
        public void GetProfileEmail_ValidEmail_ProfileReturned()
        {
            //Arrange
            const string ValidEmail = "test@mail.com";
            const string AccountId = "TestUser1";

            var jsonProfile = Properties.Resources.JsonProfile;

            Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);

            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()))
                .Returns(jsonProfile);

            //Action
            NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
            string profile = client.GetByEmailJson(ValidEmail, AccountId);

            //Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()), Times.Once);

            Assert.IsNotNull(profile);
            Assert.AreEqual(profile, jsonProfile);
        }

		[TestMethod]
		public void GetProfileByPhone_EmptyAccountID_ProfilesReturned()
		{
			//Arrange
			const string PhoneNumber = "2020327123";
			const string AccountId = "";

            string jsonProfiles = Properties.Resources.JsonProfiles;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()))
                .Returns(jsonProfiles);

            //Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
		    string profiles = client.GetByPhoneJson(PhoneNumber, AccountId);

            //Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()), Times.Once);

            Assert.IsNotNull(profiles);
            Assert.AreEqual(jsonProfiles, profiles);
		}

		[TestMethod]
		public void CreatingClientInstance_EmptyUsername_ArgumentExceptionThrown()
		{
			//Arrange
			const string Username = "";
			const string Password = "adaSfaqwfasfasdasdfasfasfasd";

			try
			{
				//Action
				NextCallerPlatformClient client = new NextCallerPlatformClient(Username, Password);
				Assert.Fail("An exception should have been thrown");
			}
			catch (ArgumentException argumentException)
			{
				//Assert
				Assert.AreEqual("username", argumentException.ParamName);
			}

		}
		[TestMethod]
		public void GetProfileById_ValidId_ProfileReturned()
		{
			//Arrange
			const string AccountId = "TestUser1";
			const string ProfileId = "adaSfaqwfasfasdasdfasfasfasd";
			string jsonProfile = Properties.Resources.JsonProfile;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()))
				.Returns(jsonProfile);


			//Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
			string profile = client.GetByProfileIdJson(ProfileId, AccountId);

			//Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()), Times.Once);

			Assert.IsNotNull(profile);
			Assert.AreEqual(jsonProfile, profile);
		}

		[TestMethod]
		public void GetProfilesByPhone_ValidPhone_ProfilesReturned()
		{
			//Arrange
			const string PhoneNumber = "2020327000";
			const string AccountId = "TestUser1";
			string jsonProfiles = Properties.Resources.JsonProfiles;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()))
				.Returns(jsonProfiles);


			//Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
			string profiles = client.GetByPhoneJson(PhoneNumber, AccountId);

			//Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()), Times.Once);

			Assert.IsNotNull(profiles);
			Assert.AreEqual(jsonProfiles, profiles);
		}

        [TestMethod]
        public void GetProfilesByNameAddress_ValidNA_ProfilesReturned()
        {
            //Arrange
            NameAddress nameAddress = new NameAddress
            {
                AddressLine = "129 West 81st Street",
                FirstName = "Jerry",
                LastName = "Seinfeld",
                City = "New York",
                State = "NY"
            };
            const string AccountId = "TestUser1";

            string jsonProfiles = Properties.Resources.JsonProfiles;

            Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(),  It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()))
                .Returns(jsonProfiles);


            //Action
            NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
            string profiles = client.GetByNameAddressJson(nameAddress, AccountId);

            //Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()), Times.Once);

            Assert.IsNotNull(profiles);
            Assert.AreEqual(jsonProfiles, profiles);
        }

		[TestMethod]
		public void GetFraudLevel_ValidPhone_FraudLevelReturned()
		{
			//Arrange
			const string PhoneNumber = "2020327000";
			const string AccountId = "TestUser1";
			string jsonFraudLevel = Properties.Resources.JsonFraudLevel;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()))
				.Returns(jsonFraudLevel);


			//Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
			string fraudLevel = client.GetFraudLevelJson(PhoneNumber, AccountId);

			//Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()), Times.Once);

			Assert.IsNotNull(fraudLevel);
			Assert.AreEqual(jsonFraudLevel, fraudLevel);
		}

		[TestMethod]
		public void GetPlatformAccount_ValidUsername_AccountIdReturned()
		{
			//Arrange
			const string AccountId = "TestUser1";
			string jsonUser = Properties.Resources.JsonPlatformUser;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(),  It.IsIn("GET", "POST"), null, null))
				.Returns(jsonUser);


			//Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
			string user = client.GetPlatformAccountJson(AccountId);

			//Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(),  It.IsIn("GET", "POST"), null, null), Times.Once);

			Assert.IsNotNull(user);
			Assert.AreEqual(jsonUser, user);
		}

		[TestMethod]
		public void GetPlatformStatistics_AllRight_PlatformStatisticsReturned()
		{
			//Arrange
			string jsonStats = Properties.Resources.JsonPlatformUser;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(),  It.IsIn("GET", "POST"), null, null))
				.Returns(jsonStats);


			//Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
			string stats = client.GetPlatformStatisticsJson();

			//Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(),  It.IsIn("GET", "POST"), null, null), Times.Once);

			Assert.IsNotNull(stats);
			Assert.AreEqual(jsonStats, stats);
		}

		[TestMethod]
		public void GetPlatformStatistics_InvalidPageNumber_ArgumentExceptionThrown()
		{
			//Arrange
			string jsonStats = Properties.Resources.JsonPlatformUser;
			const int page = -10;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(),  It.IsIn("GET", "POST"), null, null))
				.Returns(jsonStats);
			
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);

			try
			{
				//Action
				client.GetPlatformStatisticsJson(page);
				Assert.Fail("An exception should have been thrown");
			}
			catch (ArgumentException argumentException)
			{
				//Assert
				Assert.AreEqual("page", argumentException.ParamName);
			}

		}

		[TestMethod]
		public void PostProfile_ValidIdAndValidProfile_NoExceptionsThrown()
		{
			//Arrange
			const string ProfileId = "adaSfaqwfasfasdasdfasfasfasd";
            const string AccountId = "TestUser1";

			ProfileToPost profile = new ProfileToPost
			{
				FirstName = "NewFirstName",
				LastName = "NewLastName"
			};

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), "POST", It.IsAny<string>(), It.IsAny<IEnumerable<Header>>()))
				.Returns(string.Empty);


			//Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
            client.UpdateByProfileId(ProfileId, profile, AccountId);

			//Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), "POST", It.IsAny<string>(), It.IsAny<IEnumerable<Header>>()), Times.Once);
		}

		[TestMethod]
		public void CreatePlatformAccount_ValidUsernameAndValidUser_NoExceptionsThrown()
		{
			//Arrange
			PlatformAccountToPost user = new PlatformAccountToPost
			{
                Id = "TestUser1",
                FirstName = "FirstName",
				LastName = "LastName",
				Email = "email@email.com",
				CompanyName = "Malibu"
			};

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), "POST", It.IsAny<string>(), It.IsAny<IEnumerable<Header>>()))
				.Returns(string.Empty);

			//Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
            client.CreatePlatformAccount(user);

			//Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), "POST", It.IsAny<string>(), It.IsAny<IEnumerable<Header>>()), Times.Once);
		}

        [TestMethod]
        public void UpdatePlatformAccount_ValidUsernameAndValidUser_NoExceptionsThrown()
        {
            //Arrange
            const string AccountId = "TestUser1";

            PlatformAccountToPost user = new PlatformAccountToPost
            {
                Id = "NewTestUser1",
                FirstName = "NewFirstName",
                LastName = "NewLastName",
                Email = "email@email.com",
                CompanyName = "Malibu"
            };

            Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), "PUT", It.IsAny<string>(), It.IsAny<IEnumerable<Header>>()))
                .Returns(string.Empty);


            //Action
            NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
            client.UpdatePlatformAccount(user, AccountId);

            //Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), "PUT", It.IsAny<string>(), It.IsAny<IEnumerable<Header>>()), Times.Once);
        }
    }
}
