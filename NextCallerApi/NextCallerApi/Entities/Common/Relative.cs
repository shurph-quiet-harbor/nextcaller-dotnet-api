using System.Runtime.Serialization;


namespace NextCallerApi.Entities.Common
{
	/// <summary>
	/// Represents profile linked with the requested phone. Most often linked profile is a relative, which uses the same landline phone.
	/// </summary>
	[DataContract]
	public class Relative
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "id")]
		public string Id { get; set; }
	}
}
