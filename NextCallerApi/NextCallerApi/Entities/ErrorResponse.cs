using System.Runtime.Serialization;


namespace NextCallerApi.Entities
{
	[DataContract]
	internal class ErrorResponse
	{
		[DataMember(Name = "message")]
		public string Error { get; set; }
		[DataMember(Name = "type")]
		public string Type { get; set; }
		[DataMember(Name = "code")]
		public int Code { get; set; }
	}
}
