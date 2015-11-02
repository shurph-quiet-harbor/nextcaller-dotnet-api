using System.Runtime.Serialization;


namespace NextCallerApi.Entities.Common
{
	/// <summary>
	/// Represents profile's phone number.
	/// </summary>
	[DataContract]
	public class Phone
	{
        [DataMember(Name = "number")]
        public string Number { get; set; }

        [DataMember(Name = "resource_uri")]
        public string ResourceUri { get; set; }

        [DataMember(Name = "carrier")]
        public string Carrier { get; set; }

        [DataMember(Name = "line_type")]
        public string LineType { get; set; }
    }
}
