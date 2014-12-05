using System.Collections.Generic;
using System.Runtime.Serialization;


namespace NextCallerApi.Entities.Platform
{
	/// <summary>
	/// Represents detailed information about a specific user.
	/// </summary>
	[DataContract]
	public class PlatformUser
	{
		[DataMember(Name = "created_time")]
		public string CreatedTime { get; set; }

		[DataMember(Name = "number_of_operations")]
		public int NumberOfOperations { get; set; }

		[DataMember(Name = "resource_uri")]
		public string ResourceUri { get; set; }

		[DataMember(Name = "total_calls")]
		public Dictionary<string, int> TotalCalls { get; set; }

		[DataMember(Name = "successful_calls")]
		public Dictionary<string, int> SuccessfulCalls { get; set; }

		[DataMember(Name = "billable_calls")]
		public Dictionary<string, int> BillableCalls { get; set; }

		[DataMember(Name = "username")]
		public string Username { get; set; }
	}
}
