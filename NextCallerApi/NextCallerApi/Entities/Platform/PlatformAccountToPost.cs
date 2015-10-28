using System.Runtime.Serialization;


namespace NextCallerApi.Entities.Platform
{
	/// <summary>
	/// Represents a user, that can be created or updated.
	/// </summary>
	[DataContract]
	public class PlatformAccountToPost
	{
		[DataMember(Name = "first_name")]
		public string FirstName { get; set; }
		[DataMember(Name = "last_name")]
		public string LastName { get; set; }
		[DataMember(Name = "company_name")]
		public string CompanyName { get; set; }
		[DataMember(Name = "email")]
		public string Email { get; set; }
	}
}
