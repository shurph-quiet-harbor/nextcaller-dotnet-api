using System.Runtime.Serialization;


namespace NextCallerApi.Entities.Common
{
	/// <summary>
	/// Represents Next Caller profile.
	/// </summary>
	[DataContract]
	public class Profile
	{

		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "first_name")]
		public string FirstName { get; set; }

		[DataMember(Name = "middle_name")]
		public string MiddleName { get; set; }

		[DataMember(Name = "last_name")]
		public string LastName { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "language")]
		public string Language { get; set; }

		[DataMember(Name = "phone")]
		public Phone[] Phones { get; set; }

		[DataMember(Name = "carrier")]
		public string Carrier { get; set; }

		[DataMember(Name = "line_type")]
		public string LineType { get; set; }

		[DataMember(Name = "address")]
		public Address[] Addresses { get; set; }

		[DataMember(Name = "email")]
		public string Email { get; set; }

		[DataMember(Name = "linked_emails")]
		public string[] LinkedEmails { get; set; }

		[DataMember(Name = "relatives")]
		public Relative[] Relatives { get; set; }

		[DataMember(Name = "dob")]
		public string DateOfBirth { get; set; }

		[DataMember(Name = "age")]
		public string Age { get; set; }

		[DataMember(Name = "gender")]
		public string Gender { get; set; }

		[DataMember(Name = "household_income")]
		public string HouseholdIncome { get; set; }

		[DataMember(Name = "marital_status")]
		public string MaritalStatus { get; set; }

		[DataMember(Name = "presence_of_children")]
		public string PresenceOfChildren { get; set; }

		[DataMember(Name = "home_owner_status")]
		public string HomeOwnerStatus { get; set; }

		[DataMember(Name = "market_value")]
		public string MarketValue { get; set; }

		[DataMember(Name = "length_of_residence")]
		public string LengthOfResidence { get; set; }

		[DataMember(Name = "high_net_worth")]
		public string HignNetWorth { get; set; }

		[DataMember(Name = "occupation")]
		public string Occupation { get; set; }

		[DataMember(Name = "education")]
		public string Education { get; set; }

		[DataMember(Name = "department")]
		public string Department { get; set; }

	}
}
