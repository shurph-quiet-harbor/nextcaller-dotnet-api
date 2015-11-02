using System.Runtime.Serialization;


namespace NextCallerApi.Entities.Common
{
	/// <summary>
	/// Represents profile, including information that might be posted to Next Caller service.
	/// </summary>
	[DataContract]
	public class ProfileToPost
	{

		[DataMember(Name = "first_name")]
		public string FirstName { get; set; }

		[DataMember(Name = "last_name")]
		public string LastName { get; set; }

		[DataMember(Name = "email")]
		public string Email { get; set; }

		[DataMember(Name = "shipping_address1")]
		public Address PrimaryAddress { get; set; }

		[DataMember(Name = "shipping_address2")]
		public Address SecondaryAddress { get; set; }

	}
}