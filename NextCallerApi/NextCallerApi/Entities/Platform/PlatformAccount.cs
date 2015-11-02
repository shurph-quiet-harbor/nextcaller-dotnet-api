using System.Collections.Generic;
using System.Runtime.Serialization;


namespace NextCallerApi.Entities.Platform
{
	/// <summary>
	/// Represents detailed information about a specific platform account.
	/// </summary>
	[DataContract]
	public class PlatformAccount
	{
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "company_name")]
        public string CompanyName { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "number_of_operations")]
        public int NumberOfOperations { get; set; }

        [DataMember(Name = "resource_uri")]
        public string ResourceUri { get; set; }

        [DataMember(Name = "total_operations")]
        public Dictionary<string, int> TotalOperations { get; set; }

        [DataMember(Name = "billed_operations")]
        public Dictionary<string, int> BilledOperations { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }
	}
}
