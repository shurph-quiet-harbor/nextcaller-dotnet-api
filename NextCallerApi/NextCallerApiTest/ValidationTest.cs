using Microsoft.VisualStudio.TestTools.UnitTesting;

using NextCallerApi.Entities;


namespace NextCallerApiTest
{
	[TestClass]
	public class ValidationTest
	{
		[TestMethod]
		public void PhoneNumberValidation_TooShortNumber_ValidationFailed()
		{
			//Arrange
			Phone phone = new Phone
			{
				Number = "202"
			};


			//Action
			ValidationResult validationResult = phone.IsValid();

			//Assert
			Assert.IsNotNull(validationResult);
			Assert.IsFalse(validationResult.IsValid);
			Assert.IsFalse(string.IsNullOrEmpty(validationResult.Message));
		}

		[TestMethod]
		public void PhoneNumberValidation_NumberContainsLetters_ValidationFailed()
		{
			//Arrange
			Phone phone = new Phone
			{
				Number = "202asd2324"
			};


			//Action
			ValidationResult validationResult = phone.IsValid();


			//Assert
			Assert.IsNotNull(validationResult);
			Assert.IsFalse(validationResult.IsValid);
			Assert.IsFalse(string.IsNullOrEmpty(validationResult.Message));
		}

		[TestMethod]
		public void PhoneNumberValidation_NumberInEmpty_ValidationFailed()
		{
			//Arrange
			Phone phone = new Phone
			{
				Number = ""
			};


			//Action
			ValidationResult validationResult = phone.IsValid();


			//Assert
			Assert.IsNotNull(validationResult);
			Assert.IsFalse(validationResult.IsValid);
			Assert.IsFalse(string.IsNullOrEmpty(validationResult.Message));
		}

		[TestMethod]
		public void PhoneNumberValidation_NumberIsValid_ValidationSucceded()
		{
			//Arrange
			Phone phone = new Phone
			{
				Number = "2020327123"
			};


			//Action
			ValidationResult validationResult = phone.IsValid();


			//Assert
			Assert.IsNotNull(validationResult);
			Assert.IsTrue(validationResult.IsValid);
			Assert.IsTrue(string.IsNullOrEmpty(validationResult.Message));
		}
	}
}
