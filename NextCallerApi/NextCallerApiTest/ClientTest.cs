﻿using System;
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
		public void GetProfileByPhone_InvalidPhone_ArgumentExceptionThrown()
		{
			//Arrange
			const string InvalidNumber = "";

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);

			NextCallerClient client = new NextCallerClient(httpTransportMock.Object);


			try
			{
				//Action
				IList<Profile> profiles = client.GetByPhone(InvalidNumber);
				Assert.Fail("An exception should have been thrown");
			}
			catch (ArgumentException argumentException)
			{
				//Assert
				Assert.AreEqual("phone", argumentException.ParamName);
			}

		}

        [TestMethod]
        public void GetProfilesByNameAddress_InvalidNA_ArgumentExceptionThrown()
        {
            //Arrange
            NameAddress nameAddress = new NameAddress
            {
                AddressLine = "129 West 81st Street",
                FirstName = "Jerry",
                LastName = "Seinfeld",
                State = "NY"
            };

            string jsonProfiles = Properties.Resources.JsonProfiles;

            Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null))
                .Returns(jsonProfiles);

            NextCallerClient client = new NextCallerClient(httpTransportMock.Object);
            //Action
            try
            {
                client.GetByNameAddressJson(nameAddress);
                Assert.Fail("An exception should have been thrown");
            }
            catch (ArgumentException argumentException)
            {
                //Assert
                Assert.AreEqual("name and address", argumentException.ParamName);
            }
        }

        [TestMethod]
        public void GetProfilesByNameAddress_InvalidNAZip_ArgumentExceptionThrown()
        {
            //Arrange
            NameAddress nameAddress = new NameAddress
            {
                AddressLine = "129 West 81st Street",
                FirstName = "Jerry",
                LastName = "Seinfeld",
                ZipCode = 1002
            };

            string jsonProfiles = Properties.Resources.JsonProfiles;

            Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null))
                .Returns(jsonProfiles);

            NextCallerClient client = new NextCallerClient(httpTransportMock.Object);
            //Action
            try
            {
                client.GetByNameAddressJson(nameAddress);
                Assert.Fail("An exception should have been thrown");
            }
            catch (ArgumentException argumentException)
            {
                //Assert
                Assert.AreEqual("name and address", argumentException.ParamName);
            }
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
			httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null))
				.Returns(jsonProfile);


			//Action
			NextCallerClient client = new NextCallerClient(httpTransportMock.Object);
			string profile = client.GetByProfileIdJson(ProfileId);

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
			NextCallerClient client = new NextCallerClient(httpTransportMock.Object);
			string profiles = client.GetByPhoneJson(PhoneNumber);

			//Assert
			httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null), Times.Once);

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
            httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), null))
                .Returns(jsonProfiles);


            //Action
            NextCallerClient client = new NextCallerClient(httpTransportMock.Object);
            string profiles = client.GetByNameAddressJson(nameAddress);

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

			ProfileToPost profile = new ProfileToPost
			{
				FirstName = "NewFirstName",
				LastName = "NewLastName"
			};

			Mock<IHttpTransport> httpTransportMock = new Mock<IHttpTransport>(MockBehavior.Strict);
			httpTransportMock.Setup(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsAny<string>()))
				.Returns(string.Empty);


			//Action
			NextCallerClient client = new NextCallerClient(httpTransportMock.Object);
			client.UpdateByProfileId(ProfileId, profile);

			//Assert
			httpTransportMock.Verify(httpTransport => httpTransport.Request(It.IsAny<string>(), It.IsAny<ContentType>(), It.IsAny<string>()), Times.Once);
		}

	}
}
