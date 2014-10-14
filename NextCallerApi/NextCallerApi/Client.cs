using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using UrlParameter = System.Collections.Generic.KeyValuePair<string, string>;

using NextCallerApi.Entities;
using NextCallerApi.Http;
using NextCallerApi.Interfaces;
using NextCallerApi.Serialization;


namespace NextCallerApi
{
	/// <summary>
	/// Contains access methods to Next Caller service.
	/// </summary>
	public class Client
	{

		private readonly string usersUrl;
		private readonly string phoneUrl;

		private readonly string formatParameterName;
		private readonly string phoneParameterName;

		private const ContentType DefaultResponseType = ContentType.Json;
		private const ContentType PostContentType = ContentType.Json;

		private readonly IHttpTransport httpTransport;

		internal Client(IHttpTransport httpTransport): this()
		{
			this.httpTransport = httpTransport;
		}

		/// <summary>
		/// Initializes NextCaller client instance.
		/// </summary>
		/// <param name="consumerKey"> Consumer key for authorization.</param>
		/// <param name="consumerSecret"> Consumer secret for authorization.</param>
		public Client(string consumerKey, string consumerSecret): this()
		{
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(consumerKey), "consumerKey");
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(consumerSecret), "consumerSecret");

			httpTransport = new HttpTransport(consumerKey, consumerSecret);
		}

		private Client()
		{
			usersUrl = Properties.Resources.UsersUrl;
			phoneUrl = Properties.Resources.PhoneUrl;

			formatParameterName = Properties.Resources.FormatUrlParameterName;
			phoneParameterName = Properties.Resources.PhoneUrlParameterName;
		}

		#region Api

		/// <summary>
		/// Gets list of profiles, associated with a particular phone number.
		/// More information at: https://dev.nextcaller.com/documentation/get-profile/.
		/// </summary>
		/// <param name="phone">Phone number.</param>
		/// <returns>Profiles, associated with a particular phone number.</returns>
		public IList<Profile> GetProfilesByPhone(string phone)
		{
			string content = GetProfilesByPhoneJson(phone);

			return JsonSerializer.ParseProfileList(content);
		}

		/// <summary>
		/// Gets profile, associated with a particular id.
		/// More information at: https://dev.nextcaller.com/documentation/get-profile/.
		/// </summary>
		/// <param name="id">Profile id.</param>
		/// <returns>Profile, associated with a particular id.</returns>
		public Profile GetProfileById(string id)
		{
			string content = GetProfileByIdJson(id);

			return JsonSerializer.ParseProfile(content);
		}

		/// <summary>
		/// Updates profile with given id.
		/// More information at: https://dev.nextcaller.com/documentation/post-profile/.
		/// </summary>
		/// <param name="profile">Profile data to be updated.</param>
		/// <param name="id">Id of the updated profile.</param>
		public void PostProfile(ProfilePostRequest profile, string id)
		{

			Utility.EnsureParameterValid(!profile.Equals(null), "profile");
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(id), "id");

			string data = JsonSerializer.Serialize(profile);

			PostProfile(data, id);

		}

		#endregion Api

		#region RawApi

		/// <summary>
		/// Gets profile, associated with a particular id, in json format.
		/// More information at: https://dev.nextcaller.com/documentation/get-profile/.
		/// </summary>
		/// <param name="id">Profile id.</param>
		/// <returns>Profile in Json format.</returns>
		public  string GetProfileByIdJson(string id)
		{
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(id), "id");

			string url = GetUrl(usersUrl + id, new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

			return httpTransport.Request(url, DefaultResponseType);
		}

		/// <summary>
		/// Gets profile, associated with a particular phone, in json format.
		/// More information at: https://dev.nextcaller.com/documentation/get-profile/.
		/// </summary>
		/// <param name="phone">Phone number.</param>
		/// <returns>Profiles in json format.</returns>
		public string GetProfilesByPhoneJson(string phone)
		{

			Utility.EnsureParameterValid(!string.IsNullOrEmpty(phone), "phone");

			ValidationResult phoneValidationMessage = Phone.IsNumberValid(phone);
			Utility.EnsureParameterValid(phoneValidationMessage.IsValid, "phone", phoneValidationMessage.Message);

			string url = GetUrl(phoneUrl, new UrlParameter(phoneParameterName, phone), 
										  new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

			return httpTransport.Request(url, DefaultResponseType);
		}

		/// <summary>
		/// Updates profile with given id.
		/// More information at: https://dev.nextcaller.com/documentation/post-profile/.
		/// </summary>
		/// <param name="profileInJson">Profile data to be updated in Json.</param>
		/// <param name="id">Id of the updated profile.</param>
		public void PostProfile(string profileInJson, string id)
		{

			Utility.EnsureParameterValid(!string.IsNullOrEmpty(profileInJson), "profileInJson");
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(id), "id");

			string url = GetUrl(usersUrl + id, new UrlParameter(formatParameterName, PostContentType.ToString()));

			httpTransport.Request(url, PostContentType, profileInJson);

		}

		#endregion RawApi

		#region Private

		private static string GetUrl(string url, params UrlParameter[] urlParams)
		{
			if (!url.EndsWith("/"))
			{
				url += "/";
			}

			UriBuilder uriBuilder = new UriBuilder(url);

			uriBuilder.Query = string.Join("&", urlParams.Select(urlParam => urlParam.Key.ToLower() + '=' + urlParam.Value.ToLower()).ToArray());

			return Uri.EscapeUriString(uriBuilder.Uri.ToString());
		}

		#endregion Private

	}

	/// <summary>
	/// Represents possible content types.
	/// </summary>
	internal enum ContentType
	{
		/// <summary>
		/// Represent json content type.
		/// </summary>
		[Description("application/json")]
		Json,
		/// <summary>
		/// Represents xml content type.
		/// </summary>
		[Description("application/xml")]
		Xml
	}
}
