using System.Collections.Generic;
using System.Runtime.Serialization;


namespace NextCallerApi.Entities.Common
{
	/// <summary>
	/// Represents profile's address.
	/// </summary>
	[DataContract(Name = "object")]
	public class Address
	{
		[DataMember(Name = "country")]
		public string Country { get; set; }

		[DataMember(Name = "state")]
		public string State { get; set; }

		[DataMember(Name = "city")]
		public string City { get; set; }

		[DataMember(Name = "line1")]
		public string Line1 { get; set; }

		[DataMember(Name = "line2")]
		public string Line2 { get; set; }

		[DataMember(Name = "zip_code")]
		public string ZipCode { get; set; }

		[DataMember(Name = "extended_zip")]
		public string ExtendedZip { get; set; }

        [DataMember(Name = "home_data")]
        public Dictionary<string, object> HomeData { get; set; }
    }
}
