using System.Runtime.Serialization;


namespace NextCallerApi.Entities
{
	/// <summary>
	/// Represents profile, including information that might be posted to Next Caller service.
	/// </summary>
	[DataContract]
	public class ProfilePostRequest
	{

		[DataMember(Name = "first_name")]
		public string FirstName { get; set; }

		[DataMember(Name = "last_name")]
		public string LastName { get; set; }

		[DataMember(Name = "email")]
		public string Email { get; set; }

		[DataMember(Name = "phone1")]
		public string Phone1 { get; set; }

		[DataMember(Name = "phone2")]
		public string Phone2 { get; set; }

		[DataMember(Name = "phone3")]
		public string Phone3 { get; set; }

		[DataMember(Name = "shipping_address1")]
		public Address PrimaryAddress { get; set; }

		[DataMember(Name = "shipping_address2")]
		public Address SecondaryAddress { get; set; }

	}
}