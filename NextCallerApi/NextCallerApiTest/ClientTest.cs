using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using NextCallerApi;
using NextCallerApi.Entities.Common;
using NextCallerApi.Interfaces;


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
            var jsonProfile = Properties.Resources.JsonProfile;

            Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);

            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()))
                .Returns(jsonProfile);

            //Action
            NextCallerClient client = new NextCallerClient(httpTransportMock.Object);
            string profile = client.GetByEmailJson(ValidEmail);

            //Assert
            httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsIn("GET", "POST"), null, It.IsAny<IEnumerable<Header>>()), Times.Once);

            Assert.IsNotNull(profile);
            Assert.AreEqual(profile, jsonProfile);
        }
        
		[TestMethod]
		public void GetProfileById_EmptyId_ArgumentExceptionThrown()
		{
			//Arrange
			const string Id = "";

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
				Assert.AreEqual("id", argumentException.ParamName);
			}

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
