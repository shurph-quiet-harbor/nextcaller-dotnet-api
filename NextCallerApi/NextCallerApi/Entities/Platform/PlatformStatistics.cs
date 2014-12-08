using System.Collections.Generic;
using System.Runtime.Serialization;


namespace NextCallerApi.Entities.Platform
{
	/// <summary>
	/// Represents summary of all API calls made and all users.
	/// </summary>
	[DataContract]
	public class PlatformStatistics
	{
		[DataMember(Name = "platform_users")]
		public PlatformUser[] PlatformUsers { get; set; }

		[DataMember(Name = "total_platform_calls")]
		public Dictionary<string, int> TotalPlatformCalls { get; set; }

		[DataMember(Name = "successful_platform_calls")]
		public Dictionary<string, int> SuccessfulPlatformCalls { get; set; }

		[DataMember(Name = "billable_platform_calls")]
		public Dictionary<string, int> BillablePlatformCalls { get; set; }
	}
}
