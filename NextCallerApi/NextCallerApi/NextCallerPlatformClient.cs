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
		/// <param name="id">Profile ID.</param>
        /// <param name="accountId">Platform account ID.</param>
		/// <returns>Profile, associated with the given profile ID.</returns>
		public Profile GetByProfileId(string id, string accountId = null)
		{
            Utility.EnsureParameterValid(!(id == null), "id");

            string response = GetByProfileIdJson(id, accountId);

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
        /// <param name="pair">Pair of name and address or name and zip code.</param>
        /// <param name="accountId">Platform account ID.</param>
        /// <returns>Profiles, associated with the particular name-address or name-zip pair.</returns>
        public IList<Profile> GetByNameAddress(NameAddress pair, string accountId = null)
        {
            Utility.EnsureParameterValid(!(pair == null), "pair");

            string response = GetByNameAddressJson(pair, accountId);

            return JsonSerializer.ParseProfileList(response);
        }

		/// <summary>
		/// Gets fraud level for the particular phone number.
		/// More info at: https://nextcaller.com/platform/documentation/v2.1/#/fraud-levels/curl.
		/// </summary>
		/// <param name="phone">Phone number.</param>
        /// <param name="accountId">Platform account ID.</param>
		/// <returns>Fraud level, associated with the given phone number.</returns>
        public FraudLevel GetFraudLevel(string phone, string accountId = null)
		{
            Utility.EnsureParameterValid(!(phone == null), "phone");

            string response = GetFraudLevelJson(phone, accountId);

            return JsonSerializer.Deserialize<FraudLevel>(response);
		}

        /// <summary>
        /// Retrives fraud level for given call data.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/fraud-levels/curl.
        /// </summary>
        /// <param name="data">Call data to be posted.</param>
        /// <param name="accountId">Platform account ID.</param>
        /// <returns>Fraud level for given call data.</returns>
        public FraudLevel AnalyzeCall(AnalyzeCallData data, string accountId = null)
        {
            Utility.EnsureParameterValid(!(data == null), "data");

            string jsonData = JsonSerializer.Serialize(data);
            string content = AnalyzeCallJson(jsonData);

            return JsonSerializer.Deserialize<FraudLevel>(content);
        }

        /// <summary>
        /// Updates profile with given ID.
        /// More info at: https://nextcaller.com/platform/documentation/v2.1/#/profiles/post-profile/curl.
        /// </summary>
        /// <param name="data">Profile data to be updated.</param>
        /// <param name="id">ID of the profile to be updated.</param>
        /// <param name="accountId">Platform account ID.</param>
        public void UpdateByProfileId(string id, ProfileToPost data, string accountId = null)
		{
            Utility.EnsureParameterValid(!(id == null), "id");
            Utility.EnsureParameterValid(!(data == null), "data");

			string jsonData = JsonSerializer.Serialize(data);
            UpdateByProfileIdJson(id, jsonData, accountId);
		}

		/// <summary>
		/// Creates platform account.
		/// More info at:https://nextcaller.com/platform/documentation/v2.1/#/accounts/post-platform-account/curl.
		/// </summary>
		/// <param name="data">Plaftorm account information.</param>
        public void CreatePlatformAccount(PlatformAccountToPost data)
		{
			Utility.EnsureParameterValid(!(data == null), "data");

			string jsonData = JsonSerializer.Serialize(data);
            CreatePlatformAccountJson(jsonData);
		}

        /// <summary>
        /// Updates platform account.
        /// More info at:https://nextcaller.com/platform/documentation/v2.1/#/accounts/put-platform-account/curl.
        /// </summary>
        /// <param name="accountId">Platform account ID to be updated.</param>
        /// <param name="data">Plaftorm account information.</param>
        public void UpdatePlatformAccount(PlatformAccountToPost data, string accountId)
        {
            Utility.EnsureParameterValid(!(data == null), "data");
            Utility.EnsureParameterValid(!(accountId == null), "accountId");

            string jsonData = JsonSerializer.Serialize(data);
            UpdatePlatformAccountJson(jsonData, accountId);
        }

        #endregion Api

        #region RawApi

        /// <summary>
        /// Get the profile associated with the particular user ID.
        /// More information at: https://nextcaller.com/platform/documentation/v2.1/#/profiles/get-profile-id/curl.
        /// </summary>
        /// <param name="id">Profile ID.</param>
        /// <param name="accountId">Platform account ID.</param>
        /// <returns>Profile, associated with the given profile ID, in json.</returns>
        public string GetByProfileIdJson(string id, string accountId = null)
		{
            var headers = PrepareAccountIdHeader(accountId);

			string url = BuildUrl(usersUrl + id, new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

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
        /// <param name="pair">Pair of name and address or name and zip code.</param>
        /// <param name="accountId">Platform account ID.</param>
        /// <returns>Profiles, associated with the particular name-address or name-zip pair.</returns>
        public string GetByNameAddressJson(NameAddress pair, string accountId = null)
        {           
            var headers = PrepareAccountIdHeader(accountId);

            var parameters = new[]
            {
                new UrlParameter(nameAddressFirstNameParameterName, pair.FirstName ?? ""),
                new UrlParameter(nameAddressLastNameParameterName, pair.LastName ?? ""),
                new UrlParameter(nameAddressAddressParameterName, pair.AddressLine ?? ""),
                new UrlParameter(formatParameterName, DefaultResponseType.ToString())
            };
            var additional = pair.ZipCode != 0
                ? new[]
                {
                    new UrlParameter(nameAddressZipParameterName, pair.ZipCode.ToString(CultureInfo.InvariantCulture))
                }
                : new[]
                {
                    new UrlParameter(nameAddressCityParameterName, pair.City ?? ""),
                    new UrlParameter(nameAddressStateParameterName, pair.State ?? "")
                };

            string url = BuildUrl(phoneUrl, parameters.Concat(additional).ToArray());

            return httpTransport.Request(url, DefaultResponseType, headers: headers);
        }

		/// <summary>
		/// Gets fraud level for the particular phone number.
		/// More info at: https://nextcaller.com/platform/documentation/v2.1/#/fraud-levels/curl.
		/// </summary>
		/// <param name="phone">Phone number.</param>
        /// <param name="accountId">Platform account ID.</param>
		/// <returns>Fraud level, associated with the given phone number, in json.</returns>
        public string GetFraudLevelJson(string phone, string accountId = null)
		{
            var headers = PrepareAccountIdHeader(accountId);

            string url = BuildUrl(fraudUrl, new UrlParameter(phoneParameterName, phone),
										  new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

			return httpTransport.Request(url, DefaultResponseType, headers: headers);
		}

        /// <summary>
        /// Retrives fraud level for given call data in json format.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/fraud-levels/curl.
        /// </summary>
        /// <param name="data">Call data to be posted.</param>
        /// <param name="accountId">Platform account ID.</param>
        /// <returns>Fraud level for given call data in json format.</returns>
        public string AnalyzeCallJson(string data, string accountId = null)
        {
            var headers = PrepareAccountIdHeader(accountId);

            string url = BuildUrl(analyzeUrl, new UrlParameter(formatParameterName, PostContentType.ToString()));

            return httpTransport.Request(url, PostContentType, "POST", data ?? "", headers);
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
		/// <param name="data">Plaftorm account information in json.</param>
        public void CreatePlatformAccountJson(string data)
		{
            string url = BuildUrl(platformUrl, new UrlParameter(formatParameterName, PostContentType.ToString()));

			httpTransport.Request(url, PostContentType, "POST", data ?? "");
		}

        /// <summary>
        /// Updates platform account.
        /// More info at: https://nextcaller.com/platform/documentation/v2.1/#/accounts/put-platform-account/curl.
        /// </summary>
        /// <param name="accountId">Platform account ID to be updated.</param>
        /// <param name="data">Plaftorm account information in json.</param>
        public void UpdatePlatformAccountJson(string data, string accountId)
        {
            string url = BuildUrl(platformUrl + accountId, new UrlParameter(formatParameterName, PostContentType.ToString()));

            httpTransport.Request(url, PostContentType, "PUT", data ?? "");
        }

        /// <summary>
        /// Updates profile.
        /// More info at: https://nextcaller.com/platform/documentation/v2.1/#/post-profile/curl.
        /// </summary>
        /// <param name="id">ID of the profile to be updated.</param>
        /// <param name="accountId">Platform account ID.</param>
        /// <param name="data">Profile data to be updated in json.</param>
        public void UpdateByProfileIdJson(string id, string data, string accountId)
		{
            var headers = PrepareAccountIdHeader(accountId);

			string url = BuildUrl(usersUrl + id, new UrlParameter(formatParameterName, PostContentType.ToString()));

			httpTransport.Request(url, PostContentType, "POST", data ?? "", headers);

		}

		#endregion RawApi
	}
}
