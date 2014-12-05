using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using NextCallerApi.Entities;
using NextCallerApi.Entities.Common;


namespace NextCallerApi.Serialization
{
	public static class JsonSerializer
	{

		public static IList<Profile> ParseProfileList(string json)
		{
			JObject jsonObject = JObject.Parse(json);

			IList<JToken> profilesListJson = jsonObject["records"].Children().ToList();

			IList<Profile> profiles = new List<Profile>();

			foreach (JToken profile in profilesListJson)
			{
				Profile deserializedProfile = Deserialize<Profile>(profile.ToString());
				profiles.Add(deserializedProfile);
			}

			return profiles;
		}

		public static Error ParseError(string json)
		{
			JObject jsonObject = JObject.Parse(json);

			JToken errorJson = jsonObject["error"];

			if (errorJson == null)
			{
				throw new JsonException();
			}

			Error error = Deserialize<Error>(errorJson.ToString());

			return error;
		}

		public static string Serialize<T>(T objectToSerialize)
		{

			JsonSerializerSettings jsonSettings = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,

			};

			string json = JsonConvert.SerializeObject(objectToSerialize, jsonSettings);

			return json;
		}

		public static T Deserialize<T>(string serializedObject)
		{
			return JsonConvert.DeserializeObject<T>(serializedObject);
		}

	}
}
