using System.Runtime.Serialization;


namespace NextCallerApi.Entities.Common
{
	/// <summary>
	/// Represents phone's fraud level.
	/// </summary>
	[DataContract]
	public class FraudLevel
	{
		[DataMember(Name = "spoofed")]
		public string Spoofed { get; set; }

		[DataMember(Name = "fraud_risk")]
		public string FraudRisk { get; set; }
	}
}
