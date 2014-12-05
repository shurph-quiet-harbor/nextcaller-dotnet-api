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
		public void GetProfileByPhone_InvalidPhone_ArgumentExceptionThrown()
		{
			//Arrange
			const string InvalidNumber = "";
			const string PlatformUserName = "username";

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);

			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);


			try
			{
				//Action
				IList<Profile> profiles = client.GetByPhone(InvalidNumber, PlatformUserName);
				Assert.Fail("An exception should have been thrown");
			}
			catch (ArgumentException argumentException)
			{
				//Assert
				Assert.AreEqual("phone", argumentException.ParamName);
			}

		}

		[TestMethod]
		public void GetProfileByPhone_InvalidPlatformUserName_ArgumentExceptionThrown()
		{
			//Arrange
			const string InvalidNumber = "2020327123";
			const string PlatformUserName = "";

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);

			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);


			try
			{
				//Action
				IList<Profile> profiles = client.GetByPhone(InvalidNumber, PlatformUserName);
				Assert.Fail("An exception should have been thrown");
			}
			catch (ArgumentException argumentException)
			{
				//Assert
				Assert.AreEqual("platformUsername", argumentException.ParamName);
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
			const string PlatformUsername = "username";
			const string ProfileId = "adaSfaqwfasfasdasdfasfasfasd";
			string jsonProfile = Properties.Resources.JsonProfile;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
			httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null))
				.Returns(jsonProfile);


			//Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
			string profile = client.GetByProfileIdJson(ProfileId, PlatformUsername);

			//Assert
			httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null), Times.Once);

			Assert.IsNotNull(profile);
			Assert.AreEqual(jsonProfile, profile);
		}

		[TestMethod]
		public void GetProfilesByphone_ValidPhone_ProfilesReturned()
		{
			//Arrange
			const string PhoneNumber = "2020327000";
			const string PlatformUsername = "username";
			string jsonProfiles = Properties.Resources.JsonProfiles;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
			httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null))
				.Returns(jsonProfiles);


			//Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
			string profiles = client.GetByPhoneJson(PhoneNumber, PlatformUsername);

			//Assert
			httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null), Times.Once);

			Assert.IsNotNull(profiles);
			Assert.AreEqual(jsonProfiles, profiles);
		}

		[TestMethod]
		public void GetFraudLevel_ValidPhone_FraudLevelReturned()
		{
			//Arrange
			const string PhoneNumber = "2020327000";
			const string PlatformUsername = "username";
			string jsonFraudLevel = Properties.Resources.JsonFraudLevel;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
			httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null))
				.Returns(jsonFraudLevel);


			//Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
			string fraudLevel = client.GetFraudLevelJson(PhoneNumber, PlatformUsername);

			//Assert
			httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null), Times.Once);

			Assert.IsNotNull(fraudLevel);
			Assert.AreEqual(jsonFraudLevel, fraudLevel);
		}

		[TestMethod]
		public void GetPlatformUser_ValidUsername_PlatformUsernameReturned()
		{
			//Arrange
			const string PlatformUsername = "username";
			string jsonUser = Properties.Resources.JsonPlatformUser;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
			httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null))
				.Returns(jsonUser);


			//Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
			string user = client.GetPlatformUserJson(PlatformUsername);

			//Assert
			httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null), Times.Once);

			Assert.IsNotNull(user);
			Assert.AreEqual(jsonUser, user);
		}

		[TestMethod]
		public void GetPlatformStatistics_AllRight_PlatformStatisticsReturned()
		{
			//Arrange
			string jsonStats = Properties.Resources.JsonPlatformUser;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
			httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null))
				.Returns(jsonStats);


			//Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
			string stats = client.GetPlatformStatisticsJson();

			//Assert
			httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null), Times.Once);

			Assert.IsNotNull(stats);
			Assert.AreEqual(jsonStats, stats);
		}

		[TestMethod]
		public void PostProfile_ValidIdAndValidProfile_NoExceptionsThrown()
		{
			//Arrange
			const string ProfileId = "adaSfaqwfasfasdasdfasfasfasd";
			const string PlatformUsername = "username";

			ProfileToPost profile = new ProfileToPost
			{
				FirstName = "NewFirstName",
				LastName = "NewLastName"
			};

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
			httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsAny<string>()))
				.Returns(string.Empty);


			//Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
			client.UpdateByProfileId(ProfileId, profile, PlatformUsername);

			//Assert
			httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		public void PostPlatformUser_ValidUsernameAndValidUser_NoExceptionsThrown()
		{
			//Arrange
			const string PlatformUsername = "username";

			PlatformUserToPost user = new PlatformUserToPost
			{
				FirstName = "NewFirstName",
				LastName = "NewLastName",
				Email = "email@email.com",
				CompanyName = "Malibu"
			};

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
			httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsAny<string>()))
				.Returns(string.Empty);


			//Action
			NextCallerPlatformClient client = new NextCallerPlatformClient(httpTransportMock.Object);
			client.UpdatePlatformUser(PlatformUsername, user);

			//Assert
			httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsAny<string>()), Times.Once);
		}
	}
}
