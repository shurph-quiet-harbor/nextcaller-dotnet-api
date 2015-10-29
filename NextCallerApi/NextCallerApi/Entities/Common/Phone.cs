using System.Runtime.Serialization;


namespace NextCallerApi.Entities.Common
{
	/// <summary>
	/// Represents profile's phone number.
	/// </summary>
	[DataContract]
	public class Phone
	{
		private const string EmptyNumberTemplate = "Phone number cannot be empty or null.";

		[DataMember(Name = "number")]
		public string Number { get; set; }

		[DataMember(Name = "resource_uri")]
		public string ResourceUri { get; set; }
	}
}
