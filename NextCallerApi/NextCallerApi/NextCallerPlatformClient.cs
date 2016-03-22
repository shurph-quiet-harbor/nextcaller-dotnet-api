using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        private readonly string accountIdHeaderName;
	    private readonly string accountIdParameterName;
		protected readonly string pageParameterName;

		internal NextCallerPlatformClient(IHttpTransport httpTransport) : base(httpTransport)
		{
			platformUrl = baseUrl + Properties.Resources.PlatformPath;
			accountIdHeaderName = Properties.Resources.AccountIdHeaderName;
            accountIdParameterName = Properties.Resources.AccountIdParameterName;
			pageParameterName = Properties.Resources.PageParameterName;
		}

	    private IEnumerable<Header> PrepareAccountIdHeader(string accountId)
	    {
            if (String.IsNullOrEmpty(accountId))
	        {
	            return null;
	        }
	        return new List<Header>
	        {
	            new Header(accountIdHeaderName, accountId)
	        };
	    }

		/// <summary>
		/// Initializes NextCaller platform client instance.
		/// </summary>
		/// <param name="username"> Username for authorization.</param>
		/// <param name="password"> Password for authorization.</param>
		/// <param name="sandbox"> Sandbox mode flag.</param>
        /// <param name="version">API version number.</param>
		public NextCallerPlatformClient(string username, string password, bool sandbox = false, string version = "")
            : base(username, password, sandbox)
		{
			platformUrl = baseUrl + Properties.Resources.PlatformPath;
            accountIdHeaderName = Properties.Resources.AccountIdHeaderName;
		    accountIdParameterName = Properties.Resources.AccountIdParameterName;
			pageParameterName = Properties.Resources.PageParameterName;
		}

        #region Api

        /// <summary>
        /// Gets detailed information about a specific platform account.
        /// More information at: https://nextcaller.com/platform/documentation/v2.1/#/accounts/get-platform-account/curl.
        /// </summary>
        /// <param name="accountId">ID of the platform account to get info about.</param>
        /// <returns>Platform account, associated with the particular ID.</returns>
        public PlatformAccount GetPlatformAccount(string accountId)
		{
            Utility.EnsureParameterValid(!(accountId == null), "accountId");

            string response = GetPlatformAccountJson(accountId);

            return JsonSerializer.Deserialize<PlatformAccount>(response);
		}

		/// <summary>
		/// Gets a summary of all API calls made and all users
		/// More information at: https://nextcaller.com/platform/documentation/v2.1/#/accounts/get-summary/curl.
		/// </summary>
		/// <returns>Platform statistics.</returns>
		public PlatformStatistics GetPlatformStatistics(int? page = null)
		{
            Utility.EnsureParameterValid(page >= 1, "page");

            string response = GetPlatformStatisticsJson(page);

            return JsonSerializer.Deserialize<PlatformStatistics>(response);
		}

		/// <summary>
		/// Gets the profiles information Next Caller has for a number.
		/// More information at: https://nextcaller.com/platform/documentation/v2.1/#/profiles/get-phone/curl.
		/// </summary>
		/// <param name="phone">Phone number.</param>
		/// <param name="accountId">Platform account ID.</param>
		/// <returns>List of profiles, associated with the given phone number.</returns>
		public IList<Profile> GetByPhone(string phone, string accountId = null)
		{
            Utility.EnsureParameterValid(!(phone == null), "phone");

            string response = GetByPhoneJson(phone, accountId);

            return JsonSerializer.ParseProfileList(response);
		}

		/// <summary>
		/// Get the profile associated with the particular profile ID.
		/// More information at: https://nextcaller.com/platform/documentation/v2.1/#/profiles/get-profile-id/curl.
		/// </summary>
		/// <param name="profileId">Profile ID.</param>
        /// <param name="accountId">Platform account ID.</param>
		/// <returns>Profile, associated with the given profile ID.</returns>
		public Profile GetByProfileId(string profileId, string accountId = null)
		{
            Utility.EnsureParameterValid(!(profileId == null), "profileId");

            string response = GetByProfileIdJson(profileId, accountId);

            return JsonSerializer.Deserialize<Profile>(response);
		}

        /// <summary>
        /// Gets list of profiles, associated with the particular email.
        /// More information at: https://nextcaller.com/platform/documentation/v2.1/#/profiles/get-profile-email/curl.
        /// </summary>
        /// <param name="email">Email to search by.</param>
        /// <param name="accountId">Platform account ID.</param>
        /// <returns>Profiles, associated with the given email.</returns>
        public IList<Profile> GetByEmail(string email, string accountId = null)
        {
            Utility.EnsureParameterValid(!(email == null), "email");

            string response = GetByEmailJson(email, accountId);

            return JsonSerializer.ParseProfileList(response);
        }

        /// <summary>
        /// Gets list of profiles, associated with the particular name-address or name-zip pair.
        /// Throws an exception if response status is 404.
        /// More info at: https://nextcaller.com/platform/documentation/v2.1/#/profiles/get-profile-name-and-address/curl.
        /// </summary>
        /// <param name="nameAddressData">Pair of name and address or name and zip code.</param>
        /// <param name="accountId">Platform account ID.</param>
        /// <returns>Profiles, associated with the particular name-address or name-zip pair.</returns>
        public IList<Profile> GetByNameAddress(NameAddress nameAddressData, string accountId = null)
        {
            Utility.EnsureParameterValid(!(nameAddressData == null), "nameAddressData");

            string response = GetByNameAddressJson(nameAddressData, accountId);

            return JsonSerializer.ParseProfileList(response);
        }

        /// <summary>
        /// Updates profile with given ID.
        /// More info at: https://nextcaller.com/platform/documentation/v2.1/#/profiles/post-profile/curl.
        /// </summary>
        /// <param name="profileData">Profile data to be updated.</param>
        /// <param name="profileId">ID of the profile to be updated.</param>
        /// <param name="accountId">Platform account ID.</param>
        public void UpdateByProfileId(string profileId, ProfileToPost profileData, string accountId = null)
		{
            Utility.EnsureParameterValid(!(profileId == null), "profileId");
            Utility.EnsureParameterValid(!(profileData == null), "profileData");

			string jsonData = JsonSerializer.Serialize(profileData);
            UpdateByProfileIdJson(profileId, jsonData, accountId);
		}

		/// <summary>
		/// Creates platform account.
		/// More info at:https://nextcaller.com/platform/documentation/v2.1/#/accounts/post-platform-account/curl.
		/// </summary>
		/// <param name="accountData">Plaftorm account information.</param>
        public void CreatePlatformAccount(PlatformAccountToPost accountData)
		{
			Utility.EnsureParameterValid(!(accountData == null), "accountData");

			string jsonData = JsonSerializer.Serialize(accountData);
            CreatePlatformAccountJson(jsonData);
		}

        /// <summary>
        /// Updates platform account.
        /// More info at:https://nextcaller.com/platform/documentation/v2.1/#/accounts/put-platform-account/curl.
        /// </summary>
        /// <param name="accountId">Platform account ID to be updated.</param>
        /// <param name="accountData">Plaftorm account information.</param>
        public void UpdatePlatformAccount(PlatformAccountToPost accountData, string accountId)
        {
            Utility.EnsureParameterValid(!(accountData == null), "accountData");
            Utility.EnsureParameterValid(!(accountId == null), "accountId");

            string jsonData = JsonSerializer.Serialize(accountData);
            UpdatePlatformAccountJson(jsonData, accountId);
        }

        #endregion Api

        #region RawApi

        /// <summary>
        /// Get the profile associated with the particular user ID.
        /// More information at: https://nextcaller.com/platform/documentation/v2.1/#/profiles/get-profile-id/curl.
        /// </summary>
        /// <param name="profileId">Profile ID.</param>
        /// <param name="accountId">Platform account ID.</param>
        /// <returns>Profile, associated with the given profile ID, in json.</returns>
        public string GetByProfileIdJson(string profileId, string accountId = null)
		{
            var headers = PrepareAccountIdHeader(accountId);

			string url = BuildUrl(usersUrl + profileId, new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

			return httpTransport.Request(url, DefaultResponseType, headers: headers);
		}

        /// <summary>
        /// Gets list of profiles, associated with the particular email.
        /// More information at: https://nextcaller.com/platform/documentation/v2.1/#/profiles/get-profile-email/curl.
        /// </summary>
        /// <param name="email">Email to search by.</param>
        /// <param name="accountId">Platform account ID.</param>
        /// <returns>Profiles, associated with the given email, in json.</returns>
        public string GetByEmailJson(string email, string accountId = null)
        {
            var headers = PrepareAccountIdHeader(accountId);

            string url = BuildUrl(phoneUrl, new UrlParameter(emailParameterName, email ?? ""), 
                new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

            return httpTransport.Request(url, DefaultResponseType, headers: headers);
        }

		/// <summary>
		/// Gets the profiles information Next Caller has for a number.
        /// More information at: https://nextcaller.com/platform/documentation/v2.1/#/profiles/get-profile-phone/curl.
		/// </summary>
		/// <param name="phone">Phone number.</param>
        /// <param name="accountId">Platform account ID.</param>
		/// <returns>List of profiles, associated with the given phone number, in json.</returns>
		public string GetByPhoneJson(string phone, string accountId = null)
		{
            var headers = PrepareAccountIdHeader(accountId);

			string url = BuildUrl(phoneUrl, new UrlParameter(phoneParameterName, phone),
                new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

			return httpTransport.Request(url, DefaultResponseType, headers: headers);
		}

        /// <summary>
        /// Gets list of profiles, associated with the particular name-address or name-zip pair.
        /// Throws an exception if response status is 404.
        /// More information at: https://nextcaller.com/platform/documentation/v2.1/#/profiles/get-profile-name-and-address/curl.
        /// </summary>
        /// <param name="nameAddressData">Pair of name and address or name and zip code.</param>
        /// <param name="accountId">Platform account ID.</param>
        /// <returns>Profiles, associated with the particular name-address or name-zip pair.</returns>
        public string GetByNameAddressJson(NameAddress nameAddressData, string accountId = null)
        {           
            var headers = PrepareAccountIdHeader(accountId);

            var parameters = new[]
            {
                new UrlParameter(nameAddressFirstNameParameterName, nameAddressData.FirstName ?? ""),
                new UrlParameter(nameAddressLastNameParameterName, nameAddressData.LastName ?? ""),
                new UrlParameter(nameAddressAddressParameterName, nameAddressData.AddressLine ?? ""),
                new UrlParameter(formatParameterName, DefaultResponseType.ToString())
            };
            var additional = nameAddressData.ZipCode != 0
                ? new[]
                {
                    new UrlParameter(nameAddressZipParameterName, nameAddressData.ZipCode.ToString(CultureInfo.InvariantCulture))
                }
                : new[]
                {
                    new UrlParameter(nameAddressCityParameterName, nameAddressData.City ?? ""),
                    new UrlParameter(nameAddressStateParameterName, nameAddressData.State ?? "")
                };

            string url = BuildUrl(phoneUrl, parameters.Concat(additional).ToArray());

            return httpTransport.Request(url, DefaultResponseType, headers: headers);
        }

        /// <summary>
        /// Gets a summary of all API calls made and all users
        /// More information at: https://nextcaller.com/platform/documentation/v2.1/#/accounts/get-summary/curl.
        /// </summary>
        /// <param name="page">Pagination page number.</param>
        /// <returns>Platform statistics in json.</returns>
        public string GetPlatformStatisticsJson(int? page = null)
		{
			var parameters = new List<UrlParameter> {new UrlParameter(formatParameterName, DefaultResponseType.ToString())};
			if (page != null)
			{
				parameters.Add(new UrlParameter(pageParameterName, page.ToString()));
			}
			string url = BuildUrl(platformUrl, parameters.ToArray());

			return httpTransport.Request(url, DefaultResponseType);
		}

		/// <summary>
		/// Gets detailed information about a specific platform account.
		/// More information at: https://nextcaller.com/platform/documentation/v2.1/#/accounts/get-platform-account/curl.
		/// </summary>
        /// <param name="accountId">ID of the platform account to get info about.</param>
		/// <returns>Platform account, associated with the particular account ID, in json.</returns>
		public string GetPlatformAccountJson(string accountId)
		{
            string url = BuildUrl(platformUrl + accountId,
                new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

			return httpTransport.Request(url, DefaultResponseType);
		}

		/// <summary>
		/// Creates platform account.
        /// More info at: https://nextcaller.com/platform/documentation/v2.1/#/accounts/post-platform-account/curl.
		/// </summary>
		/// <param name="accountData">Plaftorm account information in json.</param>
        public void CreatePlatformAccountJson(string accountData)
		{
            string url = BuildUrl(platformUrl, new UrlParameter(formatParameterName, PostContentType.ToString()));

			httpTransport.Request(url, PostContentType, "POST", accountData ?? "");
		}

        /// <summary>
        /// Updates platform account.
        /// More info at: https://nextcaller.com/platform/documentation/v2.1/#/accounts/put-platform-account/curl.
        /// </summary>
        /// <param name="accountId">Platform account ID to be updated.</param>
        /// <param name="accountData">Plaftorm account information in json.</param>
        public void UpdatePlatformAccountJson(string accountData, string accountId)
        {
            string url = BuildUrl(platformUrl + accountId, new UrlParameter(formatParameterName, PostContentType.ToString()));

            httpTransport.Request(url, PostContentType, "PUT", accountData ?? "");
        }

        /// <summary>
        /// Updates profile.
        /// More info at: https://nextcaller.com/platform/documentation/v2.1/#/post-profile/curl.
        /// </summary>
        /// <param name="profileId">ID of the profile to be updated.</param>
        /// <param name="accountId">Platform account ID.</param>
        /// <param name="profileData">Profile data to be updated in json.</param>
        public void UpdateByProfileIdJson(string profileId, string profileData, string accountId)
		{
            var headers = PrepareAccountIdHeader(accountId);

			string url = BuildUrl(usersUrl + profileId, new UrlParameter(formatParameterName, PostContentType.ToString()));

			httpTransport.Request(url, PostContentType, "POST", profileData ?? "", headers);

		}

		#endregion RawApi
	}
}
