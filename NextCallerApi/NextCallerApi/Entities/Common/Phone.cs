using System.Runtime.Serialization;


namespace NextCallerApi.Entities.Common
{
	/// <summary>
	/// Represents profile's phone number.
	/// </summary>
	[DataContract]
	public class Phone
	{

		private const string EmptyNumberTemplate = "Phone number cannot be empty or null.";

		[DataMember(Name = "number")]
		public string Number { get; set; }

		[DataMember(Name = "resource_uri")]
		public string ResourceUri { get; set; }

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

			return new ValidationResult(true, null);
		}
	}

	
}
