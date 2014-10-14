using System.Runtime.Serialization;
using System.Text.RegularExpressions;


namespace NextCallerApi.Entities
{
	/// <summary>
	/// Represents profile's phone number.
	/// </summary>
	[DataContract]
	public class Phone
	{
		/// <summary>
		/// Valid phone number length.
		/// </summary>
		public const int AllowedNumberLength = 10;

		private const string EmptyNumberTemplate = "Phone number cannot be empty or null.";
		private const string NumberWrongLengthTemplate = "Phone number should contain {0} digits.";
		private const string OnlyDigitsAllowedTemplate = "Phone number should contain only digits.";

		[DataMember(Name = "number")]
		public string Number { get; set; }

		/// <summary>
		/// Validates phone number.
		/// </summary>
		/// <returns>Object, representing validation status and message.</returns>
		public ValidationResult IsValid()
		{
			return IsNumberValid(Number);
		}

		/// <summary>
		/// Validates phone number.
		/// </summary>
		/// <param name="number">Phone number to validate.</param>
		/// <returns>Object, representing validation status and message.</returns>
		public static ValidationResult IsNumberValid(string number)
		{
			if (string.IsNullOrEmpty(number))
			{
				return new ValidationResult(false, EmptyNumberTemplate);
			}

			if (number.Length != AllowedNumberLength)
			{
				return new ValidationResult(false, string.Format(NumberWrongLengthTemplate, AllowedNumberLength));
			}

			if (!Regex.IsMatch(number, @"^\d+$"))
			{
				return new ValidationResult(false, OnlyDigitsAllowedTemplate);
			}

			return new ValidationResult(true, null);
		}
	}

	
}
