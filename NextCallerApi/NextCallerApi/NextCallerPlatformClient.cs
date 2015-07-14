using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using NextCallerApi.Entities;
using NextCallerApi.Entities.Common;
using NextCallerApi.Entities.Platform;
using NextCallerApi.Interfaces;
using NextCallerApi.Serialization;

using UrlParameter = System.Collections.Generic.KeyValuePair<string, string>;


namespace NextCallerApi
{
	/// <summary>
	/// Contains access methods to Next Caller platform service.
	/// </summary>
	public class NextCallerPlatformClient: NextCallerClient
	{
		private readonly string platformUrl;
		private readonly string platformUsernameParameterName;
		protected readonly string pageParameterName;

		internal NextCallerPlatformClient(IHttpTransport httpTransport) : base(httpTransport)
		{
			platformUrl = baseUrl + Properties.Resources.PlatformPath;
			platformUsernameParameterName = Properties.Resources.PlatformUsernameParameterName;
			pageParameterName = Properties.Resources.PageParameterName;
		}

		/// <summary>
		/// Initializes NextCaller platform client instance.
		/// </summary>
		/// <param name="username"> Username for authorization.</param>
		/// <param name="password"> Password for authorization.</param>
		/// <param name="sandbox"> Sandbox mode flag.</param>
        /// <param name="version">API version number</param>
		public NextCallerPlatformClient(string username, string password, bool sandbox = false, string version = "") : base(username, password, sandbox, version)
		{
			platformUrl = baseUrl + Properties.Resources.PlatformPath;
			platformUsernameParameterName = Properties.Resources.PlatformUsernameParameterName;
			pageParameterName = Properties.Resources.PageParameterName;
		}

		#region Api

		/// <summary>
		/// Gets detailed information about a specific user.
		/// More information at: https://nextcaller.com/platform/documentation/v2.1/#/get-platform-user/curl.
		/// </summary>
		/// <param name="platformUsername">Platform username to get info about.</param>
		/// <returns>Platform user, associated with a particular platform username.</returns>
		public PlatformUser GetPlatformUser(string platformUsername)
		{
			string response = GetPlatformUserJson(platformUsername);
			return JsonSerializer.Deserialize<PlatformUser>(response);
		}

		/// <summary>
		/// Gets a summary of all API calls made and all users
		/// More information at: https://nextcaller.com/platform/documentation/v2.1/#/get-summary/curl.
		/// </summary>
		/// <returns>Platform statistics</returns>
		public PlatformStatistics GetPlatformStatistics(int? page=null)
		{
			string response = GetPlatformStatisticsJson(page);
			return JsonSerializer.Deserialize<PlatformStatistics>(response);
		}

		/// <summary>
		/// Gets the profiles information Next Caller has for a number.
		/// More information at: https://nextcaller.com/platform/documentation/v2.1/#/get-profile/curl.
		/// </summary>
		/// <param name="phone">Phone number</param>
		/// <param name="platformUsername">Platfor username of the caller</param>
		/// <returns>List of profiles, associated with the given phone number</returns>
		public IList<Profile> GetByPhone(string phone, string platformUsername)
		{
			string response = GetByPhoneJson(phone, platformUsername);
			return JsonSerializer.ParseProfileList(response);
		}

		/// <summary>
		/// Get the profile associated with a particular user id.
		/// More information at: https://nextcaller.com/platform/documentation/v2.1/#/get-profile-id/curl.
		/// </summary>
		/// <param name="id">Profile id</param>
		/// <param name="platformUsername">Platform username of the caller</param>
		/// <returns>Profile, associated with the given profile id</returns>
		public Profile GetByProfileId(string id, string platformUsername)
		{
			string response = GetByProfileIdJson(id, platformUsername);
			return JsonSerializer.Deserialize<Profile>(response);
		}

        /// <summary>
        /// Gets list of profiles, associated with a particular name-address or name-zip pair.
        /// Throws an exception if response status is 404.
        /// </summary>
        /// <param name="pair">Pair of name and address or name and zip code.</param>
        /// <param name="platformUsername">Platform username of the caller</param>
        /// <returns>Profiles, associated with a particular name-address or name-zip pair.</returns>
        public IList<Profile> GetByNameAddress(NameAddress pair, string platformUsername)
        {
            string response = GetByNameAddressJson(pair, platformUsername);
            return JsonSerializer.ParseProfileList(response);
        }

		/// <summary>
		/// Gets fraud level for a particular phone number.
		/// More info at: https://nextcaller.com/platform/documentation/v2.1/#/get-fraud-level/curl.
		/// </summary>
		/// <param name="phone">Phone number</param>
		/// <param name="platformUsername">Platform username of the caller</param>
		/// <returns>Fraud level, associated with the given phone number</returns>
		public FraudLevel GetFraudLevel(string phone, string platformUsername)
		{
			string response = GetFraudLevelJson(phone, platformUsername);
			return JsonSerializer.Deserialize<FraudLevel>(response);
		}

		/// <summary>
		/// Updates profile with given id.
		/// More info at: https://nextcaller.com/platform/documentation/v2.1/#/post-profile/curl.
		/// </summary>
		/// <param name="data">Profile data to be updated.</param>
		/// <param name="id">Id of the updated profile.</param>
		/// <param name="platformUsername">Platform username of the caller</param>
		public void UpdateByProfileId(string id, ProfileToPost data, string platformUsername)
		{
			Utility.EnsureParameterValid(!data.Equals(null), "data");

			string jsonData = JsonSerializer.Serialize(data);
			UpdateByProfileId(id, jsonData, platformUsername);
		}

		/// <summary>
		/// Updates or creates platform user.
		/// More info at:https://nextcaller.com/platform/documentation/v2.1/#/post-platform-user/curl.
		/// </summary>
		/// <param name="platformUsername">Platform username</param>
		/// <param name="data">Plaftorm user information</param>
		public void UpdatePlatformUser(string platformUsername, PlatformUserToPost data)
		{
			Utility.EnsureParameterValid(!data.Equals(null), "data");

			string jsonData = JsonSerializer.Serialize(data);
			UpdatePlatformUser(platformUsername, jsonData);
		}

		#endregion Api

		#region RawApi

		/// <summary>
		/// Get the profile associated with a particular user id.
		/// More information at: https://nextcaller.com/platform/documentation/v2.1/#/get-profile/get-profile-id/curl.
		/// </summary>
		/// <param name="id">Profile id</param>
		/// <param name="platformUsername">Platform username of the caller</param>
		/// <returns>Profile, associated with the given profile id, in json</returns>
		public string GetByProfileIdJson(string id, string platformUsername)
		{
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(id), "id");
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(platformUsername), "platformUsername");

			string url = BuildUrl(usersUrl + id, new UrlParameter(platformUsernameParameterName, platformUsername),
											   new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

			return httpTransport.Request(url, DefaultResponseType);
		}

		/// <summary>
		/// Gets the profiles information Next Caller has for a number.
        /// More information at: https://nextcaller.com/platform/documentation/v2.1/#/get-profile/get-profile-phone/curl.
		/// </summary>
		/// <param name="phone">Phone number</param>
		/// <param name="platformUsername">Platfor username of the caller</param>
		/// <returns>List of profiles, associated with the given phone number, in json</returns>
		public string GetByPhoneJson(string phone, string platformUsername)
		{
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(phone), "phone");
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(platformUsername), "platformUsername");

			ValidationResult phoneValidationMessage = Phone.IsNumberValid(phone);
			Utility.EnsureParameterValid(phoneValidationMessage.IsValid, "phone", phoneValidationMessage.Message);

			string url = BuildUrl(phoneUrl, new UrlParameter(platformUsernameParameterName, platformUsername),
										  new UrlParameter(phoneParameterName, phone),
										  new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

			return httpTransport.Request(url, DefaultResponseType);
		}

        /// <summary>
        /// Gets list of profiles, associated with a particular name-address or name-zip pair.
        /// Throws an exception if response status is 404.
        /// </summary>
        /// <param name="pair">Pair of name and address or name and zip code.</param>
        /// <param name="platformUsername">Platfor username of the caller</param>
        /// <returns>Profiles, associated with a particular name-address or name-zip pair.</returns>
        public string GetByNameAddressJson(NameAddress pair, string platformUsername)
        {
            Utility.EnsureParameterValid(!string.IsNullOrEmpty(platformUsername), "platformUsername");
            ValidationResult nameAddressValidationResult = NameAddress.IsNameAddressValid(pair);
            Utility.EnsureParameterValid(nameAddressValidationResult.IsValid, "name and address", nameAddressValidationResult.Message);

            var parameters = new[]
            {
                new UrlParameter(nameAddressFirstNameParameterName, pair.FirstName),
                new UrlParameter(nameAddressLastNameParameterName, pair.LastName),
                new UrlParameter(nameAddressAddressParameterName, pair.AddressLine),
                new UrlParameter(formatParameterName, DefaultResponseType.ToString()),
                new UrlParameter(platformUsernameParameterName, platformUsername)
            };
            var additional = pair.ZipCode != 0
                ? new[]
                {
                    new UrlParameter(nameAddressZipParameterName, pair.ZipCode.ToString(CultureInfo.InvariantCulture))
                }
                : new[]
                {
                    new UrlParameter(nameAddressCityParameterName, pair.City),
                    new UrlParameter(nameAddressStateParameterName, pair.State)
                };

            string url = BuildUrl(phoneUrl, parameters.Concat(additional).ToArray());

            return httpTransport.Request(url, DefaultResponseType);
        }

		/// <summary>
		/// Gets fraud level for a particular phone number.
		/// More info at: https://nextcaller.com/platform/documentation/v2.1/#/get-fraud-level/curl.
		/// </summary>
		/// <param name="phone">Phone number</param>
		/// <param name="platformUsername">Platform username of the caller</param>
		/// <returns>Fraud level, associated with the given phone number, in json</returns>
		public string GetFraudLevelJson(string phone, string platformUsername)
		{
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(phone), "phone");
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(platformUsername), "platformUsername");

			string url = BuildUrl(fraudUrl, new UrlParameter(platformUsernameParameterName, platformUsername),
										  new UrlParameter(phoneParameterName, phone),
										  new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

			return httpTransport.Request(url, DefaultResponseType);
		}

		/// <summary>
		/// Gets a summary of all API calls made and all users
		/// More information at: https://nextcaller.com/platform/documentation/v2.1/#/get-summary/curl.
		/// </summary>
		/// <param name="page">Pagination page number.</param>
		/// <returns>Platform statistics in json.</returns>
		public string GetPlatformStatisticsJson(int? page = null)
		{
			var parameters = new List<UrlParameter> {new UrlParameter(formatParameterName, DefaultResponseType.ToString())};
			if (page != null)
			{
				Utility.EnsureParameterValid(page >= 1, "page");
				parameters.Add(new UrlParameter(pageParameterName, page.ToString()));
			}
			string url = BuildUrl(platformUrl, parameters.ToArray());

			return httpTransport.Request(url, DefaultResponseType);
		}

		/// <summary>
		/// Gets detailed information about a specific user.
		/// More information at: https://nextcaller.com/platform/documentation/v2.1/#/get-platform-user/curl.
		/// </summary>
		/// <param name="platformUsername">Platform username to get info about.</param>
		/// <returns>Platform user, associated with a particular platform username, in json.</returns>
		public string GetPlatformUserJson(string platformUsername)
		{
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(platformUsername), "platformUsername");

			string url = BuildUrl(platformUrl + platformUsername,
											 new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

			return httpTransport.Request(url, DefaultResponseType);
		}

		/// <summary>
		/// Updates or creates platform user.
		/// More info at: https://nextcaller.com/platform/documentation/v2.1/#/post-platform-user/curl.
		/// </summary>
		/// <param name="platformUsername">Platform username</param>
		/// <param name="data">Plaftorm user information in json</param>
		public void UpdatePlatformUser(string platformUsername, string data)
		{
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(data), "data");
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(platformUsername), "platformUsername");

			string url = BuildUrl(platformUrl + platformUsername, new UrlParameter(formatParameterName, PostContentType.ToString()));

			httpTransport.Request(url, PostContentType, "POST", data);
		}

		/// <summary>
		/// Updates profile.
		/// More info at: https://nextcaller.com/platform/documentation/v2.1/#/post-profile/curl.
		/// </summary>
		/// <param name="id">Profile id</param>
		/// <param name="platformUsername">Platform username of the caller</param>
		/// <param name="data">Plaftorm user information in json</param>
		public void UpdateByProfileId(string id, string data, string platformUsername)
		{

			Utility.EnsureParameterValid(!string.IsNullOrEmpty(data), "data");
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(id), "id");
			Utility.EnsureParameterValid(!string.IsNullOrEmpty(platformUsername), "platformUsername");

			string url = BuildUrl(usersUrl + id, new UrlParameter(platformUsernameParameterName, platformUsername),
											   new UrlParameter(formatParameterName, PostContentType.ToString()));

			httpTransport.Request(url, PostContentType, "POST", data);

		}

		#endregion RawApi
	}
}
