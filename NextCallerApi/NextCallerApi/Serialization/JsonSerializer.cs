using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using NextCallerApi.Entities;


namespace NextCallerApi.Serialization
{
	internal static class JsonSerializer
	{

		public static IList<Profile> ParseProfileList(string response)
		{
			JObject json = JObject.Parse(response);

			IList<JToken> profilesListJson = json["records"].Children().ToList();

			IList<Profile> profiles = new List<Profile>();

			foreach (JToken profile in profilesListJson)
			{
				Profile deserializedProfile = ParseProfile(profile.ToString());
				profiles.Add(deserializedProfile);
			}

			return profiles;
		}

		public static Profile ParseProfile(string response)
		{
			return Deserialize<Profile>(response);
		}

		public static ErrorResponse ParseError(string response)
		{
			JObject json = JObject.Parse(response);

			JToken errorJson = json["error"];

			if (errorJson == null)
			{
				throw new JsonException();
			}

			ErrorResponse error = Deserialize<ErrorResponse>(errorJson.ToString());

			return error;
		}

		public static PostErrorResponse ParsePostError(string response)
		{
			JObject json = JObject.Parse(response);

			JToken errorJson = json["users"];

			if (errorJson == null)
			{
				throw new JsonException();
			}

			PostErrorResponse error = Deserialize<PostErrorResponse>(errorJson.ToString());

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
