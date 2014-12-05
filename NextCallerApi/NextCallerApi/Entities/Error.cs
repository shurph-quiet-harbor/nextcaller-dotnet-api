using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;


namespace NextCallerApi.Entities
{
	/// <summary>
	/// Represents an error response, if a Next Caller API endpoint was provided with invalid or nonsensical parameters.
	/// </summary>
	[DataContract(Name = "error")]
	public class Error
	{
		[DataMember(Name = "message")]
		public string Message { get; set; }

		[DataMember(Name = "code")]
		public string Code { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; }

		[DataMember(Name = "description")]
		public Dictionary<string, string[]> Description { get; set; }

		public override string ToString()
		{

			string message = string.Format("Message: {0}", Message ?? string.Empty);
			string code = string.Format("Code: {0}", Code ?? string.Empty);
			string type = string.Format("Type: {0}", Type ?? string.Empty);

			string description = string.Empty;

			if (Description != null && Description.Count > 0)
			{

				Func<string, string[], string> descriptionStringFormatter = (propertyName, messages) =>
																			{
																				string joinedMessages = string.Join(" ;", messages);
																				return propertyName + " : " + joinedMessages;
																			};

				description += string.Join(Environment.NewLine,
					Description.Select(pair => descriptionStringFormatter(pair.Key, pair.Value)).ToArray());
			}


			return string.Join(Environment.NewLine, new[]
			{
				message, code, type, description
			});
		}
	}
}