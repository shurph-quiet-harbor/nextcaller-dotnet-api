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
		[DataMember(Name = "data")]
		public PlatformAccount[] PlatformAccounts { get; set; }

		[DataMember(Name = "total_platform_operations")]
		public Dictionary<string, int> TotalPlatformOperations { get; set; }

		[DataMember(Name = "billed_platform_operations")]
		public Dictionary<string, int> BilledPlatformOperations { get; set; }

	}
}
