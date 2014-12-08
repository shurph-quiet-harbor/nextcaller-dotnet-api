using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using NextCallerApi.Entities.Common;

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
	public class NextCallerClient
	{

		protected readonly string baseUrl;
		protected readonly string usersUrl;
		protected readonly string phoneUrl;
		protected readonly string fraudUrl;

		protected readonly string formatParameterName;
		protected readonly string phoneParameterName;

		protected readonly bool isSandboxOn;

		protected const ContentType DefaultResponseType = ContentType.Json;
		protected const ContentType PostContentType = ContentType.Json;

		protected readonly IHttpTransport httpTransport;

		internal NextCallerClient(IHttpTransport httpTransport): this()
		{
			this.httpTransport = httpTransport;
		}

		/// <summary>
		/// Initializes NextCaller client instance.
		/// </summary>
		/// <param name="username"> Username for authorization.</param>
		/// <param name="password"> Password for authorization.</param>
		/// <param name="sandbox"> Sandbox mode flag.</param>
		public NextCallerClient(string username, string password, bool sandbox = false)
			: this(sandbox)
		{
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(username), "username");
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(password), "password");

			httpTransport = new HttpTransport(username, password);
		}

		/// <summary>
		/// Initializes NextCaller client private fields.
		/// </summary>
		/// <param name="sandbox">Sandbox mode flag</param>
		protected NextCallerClient(bool sandbox = false)
		{
			isSandboxOn = sandbox;

			baseUrl = isSandboxOn ? Properties.Resources.SandboxUrl 
								  : Properties.Resources.WorkingUrl;

			usersUrl = baseUrl + Properties.Resources.UsersPath;
			phoneUrl = baseUrl + Properties.Resources.PhonePath;
			fraudUrl = baseUrl + Properties.Resources.FraudPath;

			formatParameterName = Properties.Resources.FormatUrlParameterName;
			phoneParameterName = Properties.Resources.PhoneUrlParameterName;
		}

		#region Api

		/// <summary>
		/// Gets list of profiles, associated with a particular phone number.
		/// More information at: https://nextcaller.com/documentation/#/get-profile/curl.
		/// </summary>
		/// <param name="phone">Phone number.</param>
		/// <returns>Profiles, associated with a particular phone number.</returns>
		public IList<Profile> GetByPhone(string phone)
		{
			string content = GetByPhoneJson(phone);

			return JsonSerializer.ParseProfileList(content);
		}

		/// <summary>
		/// Gets profile, associated with a particular id.
		/// More information at: https://nextcaller.com/documentation/#/get-profile/curl.
		/// </summary>
		/// <param name="id">Profile id.</param>
		/// <returns>Profile, associated with a particular id.</returns>
		public Profile GetByProfileId(string id)
		{
			string content = GetByProfileIdJson(id);

			return JsonSerializer.Deserialize<Profile>(content);
		}

		/// <summary>
		/// Gets fraud level for given phone number.
		/// More information at: https://nextcaller.com/documentation/#/get-fraud-level/curl.
		/// </summary>
		/// <param name="phone">Phone number.</param>
		/// <returns>Fraud level for given phone number.</returns>
		public FraudLevel GetFraudLevel(string phone)
		{
			string content = GetFraudLevelJson(phone);

			return JsonSerializer.Deserialize<FraudLevel>(content);
		}

		/// <summary>
		/// Updates profile with given id.
		/// More information at: https://nextcaller.com/documentation/#/post-profile/curl
		/// </summary>
		/// <param name="data">Profile data to be updated.</param>
		/// <param name="id">Id of the updated profile.</param>
		public void UpdateByProfileId(string id, ProfileToPost data)
		{

			Utility.EnsureParameterValid(!data.Equals(null), "data");
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(id), "id");

			string jsonData = JsonSerializer.Serialize(data);

			UpdateByProfileId(id ,jsonData);

		}

		#endregion Api

		#region RawApi

		/// <summary>
		/// Gets profile, associated with a particular id, in json format.
		/// More information at: https://nextcaller.com/documentation/#/get-profile/curl.
		/// </summary>
		/// <param name="id">Profile id.</param>
		/// <returns>Profile in Json format.</returns>
		public  string GetByProfileIdJson(string id)
		{
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(id), "id");

			string url = BuildUrl(usersUrl + id, new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

			return httpTransport.Request(url, DefaultResponseType);
		}

		/// <summary>
		/// Gets profile, associated with a particular phone, in json format.
		/// More information at: https://nextcaller.com/documentation/#/get-profile/curl.
		/// </summary>
		/// <param name="phone">Phone number.</param>
		/// <returns>Profiles in json format.</returns>
		public string GetByPhoneJson(string phone)
		{
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(phone), "phone");

			ValidationResult phoneValidationMessage = Phone.IsNumberValid(phone);
			Utility.EnsureParameterValid(phoneValidationMessage.IsValid, "phone", phoneValidationMessage.Message);

			string url = BuildUrl(phoneUrl, new UrlParameter(phoneParameterName, phone), 
										  new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

			return httpTransport.Request(url, DefaultResponseType);
		}

		/// <summary>
		/// Gets fraud level for given phone number in json format.
		/// More information at: https://nextcaller.com/documentation/#/get-fraud-level/curl.
		/// </summary>
		/// <param name="phone">Phone number.</param>
		/// <returns>Fraud level for given phone number in json format.</returns>
		public string GetFraudLevelJson(string phone)
		{
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(phone), "phone");

			string url = BuildUrl(fraudUrl , new UrlParameter(phoneParameterName, phone), 
										   new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

			return httpTransport.Request(url, DefaultResponseType);
		}

		/// <summary>
		/// Updates profile with given id.
		/// More information at: https://nextcaller.com/documentation/#/post-profile/curl.
		/// </summary>
		/// <param name="data">Profile data to be updated in Json.</param>
		/// <param name="id">Id of the updated profile.</param>
		public void UpdateByProfileId(string id, string data)
		{

			Utility.EnsureParameterValid(!string.IsNullOrEmpty(data), "data");
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(id), "id");

			string url = BuildUrl(usersUrl + id, new UrlParameter(formatParameterName, PostContentType.ToString()));

			httpTransport.Request(url, PostContentType, data);

		}

		#endregion RawApi

		#region Protected

		/// <summary>
		/// Joins url parameters and appends the result to the given url.
		/// </summary>
		/// <param name="url">Url to concatenate with parameters</param>
		/// <param name="urlParams">Array of string - string pairs</param>
		/// <returns></returns>
		protected static string BuildUrl(string url, params UrlParameter[] urlParams)
		{
			if (!url.EndsWith("/"))
			{
				url += "/";
			}

			UriBuilder uriBuilder = new UriBuilder(url);

			uriBuilder.Query = string.Join("&", urlParams.Select(urlParam => urlParam.Key.ToLower() + '=' + urlParam.Value.ToLower()).ToArray());

			return Uri.EscapeUriString(uriBuilder.Uri.ToString());
		}

		#endregion Protected

	}

	/// <summary>
	/// Represents possible content types.
	/// </summary>
	public enum ContentType
	{
		/// <summary>
		/// Represent json content type.
		/// </summary>
		[Description("application/json")]
		Json
	}
}
