using System.Runtime.Serialization;


namespace NextCallerApi.Entities
{
	[DataContract]
	internal class PostErrorResponse
	{

		[DataMember(Name = "email")]
		public string EmailError { get; set; }

		[DataMember(Name = "phone1")]
		public string Phone1Error { get; set; }

		[DataMember(Name = "phone2")]
		public string Phone2Error { get; set; }

		[DataMember(Name = "phone3")]
		public string Phone3Error { get; set; }

		public override string ToString()
		{
			string errorString = string.Empty;

			if (!string.IsNullOrEmpty(EmailError))
			{
				errorString += EmailError;
			}
			if (!string.IsNullOrEmpty(Phone1Error))
			{
				errorString += Phone1Error;
			}
			if (!string.IsNullOrEmpty(Phone2Error))
			{
				errorString += Phone2Error;
			}
			if (!string.IsNullOrEmpty(Phone3Error))
			{
				errorString += Phone3Error;
			}

			return errorString;
		}
	}
}