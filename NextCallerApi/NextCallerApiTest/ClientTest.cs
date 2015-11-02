using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using NextCallerApi;
using NextCallerApi.Entities.Common;
using NextCallerApi.Interfaces;
using NextCallerApi.Serialization;

namespace NextCallerApiTest
{
	[TestClass]
	public class ClientTest
	{
		[TestMethod]
        public void GetProfileEmail_ValidEmail_ProfileReturned()
        {
            //Arrange
            const string ValidEmail = "test@mail.com";
            var jsonProfiles = Properties.Resources.JsonProfiles;

            Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);

            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()))
                .Returns(jsonProfiles);

            //Action
            NextCallerClient client = new NextCallerClient(httpTransportMock.Object);
            string profiles = client.GetByEmailJson(ValidEmail);

            //Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()), Times.Once);

            Assert.IsNotNull(profiles);
            Assert.AreEqual(profiles, jsonProfiles);
        }
        
		[TestMethod]
		public void GetProfileById_NullId_ArgumentExceptionThrown()
		{
			//Arrange
			const string Id = null;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);

			NextCallerClient client = new NextCallerClient(httpTransportMock.Object);


			try
			{
				//Action
				Profile profile = client.GetByProfileId(Id);
				Assert.Fail("An exception should have been thrown");
			}
			catch (ArgumentException argumentException)
			{
				//Assert
				Assert.AreEqual("profileId", argumentException.ParamName);
			}

		}

		[TestMethod]
		public void CreatingClientInstance_NullUsername_ArgumentExceptionThrown()
		{
			//Arrange
			const string Username = null;
			const string Password = "adaSfaqwfasfasdasdfasfasfasd";

			try
			{
				//Action
				NextCallerClient client = new NextCallerClient(Username, Password);
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
			const string ProfileId = "adaSfaqwfasfasdasdfasfasfasd";
			string jsonProfile = Properties.Resources.JsonProfile;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(),  It.IsIn("GET", "POST"), null, null))
				.Returns(jsonProfile);


			//Action
			NextCallerClient client = new NextCallerClient(httpTransportMock.Object);
			string profile = client.GetByProfileIdJson(ProfileId);

			//Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(),  It.IsIn("GET", "POST"), null, null), Times.Once);

			Assert.IsNotNull(profile);
			Assert.AreEqual(jsonProfile, profile);
		}

		[TestMethod]
		public void GetProfilesByphone_ValidPhone_ProfilesReturned()
		{
			//Arrange
			const string PhoneNumber = "2020327000";
			string jsonProfiles = Properties.Resources.JsonProfiles;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(),  It.IsIn("GET", "POST"), null, null))
				.Returns(jsonProfiles);


			//Action
			NextCallerClient client = new NextCallerClient(httpTransportMock.Object);
			string profiles = client.GetByPhoneJson(PhoneNumber);

			//Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(),  It.IsIn("GET", "POST"), null, null), Times.Once);

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

            string jsonProfiles = Properties.Resources.JsonProfiles;

            Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(),  It.IsIn("GET", "POST"), null, null))
                .Returns(jsonProfiles);


            //Action
            NextCallerClient client = new NextCallerClient(httpTransportMock.Object);
            string profiles = client.GetByNameAddressJson(nameAddress);

            //Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(),  It.IsIn("GET", "POST"), null, null), Times.Once);

            Assert.IsNotNull(profiles);
            Assert.AreEqual(jsonProfiles, profiles);
        }

        [TestMethod]
        public void GetFraudLevel_ValidPhone_FraudLevelReturned()
        {
            //Arrange
            const string PhoneNumber = "2020327000";
            string jsonFraudLevel = Properties.Resources.JsonFraudLevel;

            Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()))
                .Returns(jsonFraudLevel);


            //Action
            NextCallerClient client = new NextCallerClient(httpTransportMock.Object);
            string fraudLevel = client.GetFraudLevelJson(PhoneNumber);

            //Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()), Times.Once);

            Assert.IsNotNull(fraudLevel);
            Assert.AreEqual(jsonFraudLevel, fraudLevel);
        }

        [TestMethod]
        public void AnalyzeCall_ValidData_FraudLevelReturned()
        {
            //Arrange
            string jsonFraudLevel = Properties.Resources.JsonFraudLevel;

            AnalyzeCallData data = new AnalyzeCallData
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

            Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), It.IsAny<string>(), It.IsAny<IEnumerable<Header>>()))
                .Returns(jsonFraudLevel);

            //Action
            NextCallerClient client = new NextCallerClient(httpTransportMock.Object);
            string jsonData = JsonSerializer.Serialize(data);
            string fraudLevel = client.AnalyzeCallJson(jsonData);

            //Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), It.IsAny<string>(), It.IsAny<IEnumerable<Header>>()), Times.Once);

            Assert.IsNotNull(fraudLevel);
            Assert.AreEqual(jsonFraudLevel, fraudLevel);
        }

        [TestMethod]
		public void PostProfile_ValidIdAndValidProfile_NoExceptionsThrown()
		{
			//Arrange
			const string ProfileId = "adaSfaqwfasfasdasdfasfasfasd";

			ProfileToPost profile = new ProfileToPost
			{
				FirstName = "NewFirstName",
				LastName = "NewLastName"
			};

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
			httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), "POST", It.IsAny<string>(), null))
				.Returns(string.Empty);


			//Action
			NextCallerClient client = new NextCallerClient(httpTransportMock.Object);
			client.UpdateByProfileId(ProfileId, profile);

			//Assert
			httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), "POST", It.IsAny<string>(), null), Times.Once);
		}

	}
}
