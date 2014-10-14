using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using NextCallerApi;
using NextCallerApi.Entities;
using NextCallerApi.Interfaces;


namespace NextCallerApiTest
{
	[TestClass]
	public class ClientTest
	{
		[TestMethod]
		public void GetProfileByPhone_InvalidPhone_ArgumentExceptionThrown()
		{
			//Arrange
			const string InvalidNumber = "#4sdasfasf";

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);

			Client client = new Client(httpTransportMock.Object);


			try
			{
				//Action
				IList<Profile> profiles = client.GetProfilesByPhone(InvalidNumber);
				Assert.Fail("An exception should have been thrown");
			}
			catch (ArgumentException argumentException)
			{
				//Assert
				Assert.AreEqual("phone", argumentException.ParamName);
			}

		}

		[TestMethod]
		public void GetProfileById_EmptyId_ArgumentExceptionThrown()
		{
			//Arrange
			const string Id = "";

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);

			Client client = new Client(httpTransportMock.Object);


			try
			{
				//Action
				Profile profile = client.GetProfileById(Id);
				Assert.Fail("An exception should have been thrown");
			}
			catch (ArgumentException argumentException)
			{
				//Assert
				Assert.AreEqual("id", argumentException.ParamName);
			}

		}

		[TestMethod]
		public void CreatingClientInstance_EmptyConsumerKey_ArgumentExceptionThrown()
		{
			//Arrange
			const string ConsumerKey = "";
			const string ConsumerSecret = "adaSfaqwfasfasdasdfasfasfasd";

			try
			{
				//Action
				Client client = new Client(ConsumerKey, ConsumerSecret);
				Assert.Fail("An exception should have been thrown");
			}
			catch (ArgumentException argumentException)
			{
				//Assert
				Assert.AreEqual("consumerKey", argumentException.ParamName);
			}

		}

		[TestMethod]
		public void GetProfileById_SuccessfulRequestSuccessfullParsing_1Json()
		{
			//Arrange
			const string ConsumerKey = "";
			const string ConsumerSecret = "adaSfaqwfasfasdasdfasfasfasd";


			try
			{
				//Action
				Client client = new Client(ConsumerKey, ConsumerSecret);
				Assert.Fail("An exception should have been thrown");
			}
			catch (ArgumentException argumentException)
			{
				//Assert
				Assert.AreEqual("consumerKey", argumentException.ParamName);
			}

		}

		[TestMethod]
		public void GetProfileById_ValidId_ProfileReturned()
		{
			//Arrange
			const string ProfileId = "adaSfaqwfasfasdasdfasfasfasd";
			string jsonProfile = Properties.Resources.JsonProfile;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
			httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null))
				.Returns(jsonProfile);


			//Action
			Client client = new Client(httpTransportMock.Object);
			string profile = client.GetProfileByIdJson(ProfileId);

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
			string jsonProfiles = Properties.Resources.JsonProfiles;

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
			httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null))
				.Returns(jsonProfiles);


			//Action
			Client client = new Client(httpTransportMock.Object);
			string profiles = client.GetProfilesByPhoneJson(PhoneNumber);

			//Assert
			httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null), Times.Once);

			Assert.IsNotNull(profiles);
			Assert.AreEqual(jsonProfiles, profiles);
		}

		[TestMethod]
		public void PostProfile_ValidIdAndValidProfile_NoExceptionsThrown()
		{
			//Arrange
			const string ProfileId = "adaSfaqwfasfasdasdfasfasfasd";

			ProfilePostRequest profile = new ProfilePostRequest
			{
				FirstName = "NewFirstName",
				LastName = "NewLastName"
			};

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
			httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsAny<string>()))
				.Returns(string.Empty);


			//Action
			Client client = new Client(httpTransportMock.Object);
			client.PostProfile(profile, ProfileId);

			//Assert
			httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsAny<string>()), Times.Once);
		}

	}
}
