using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using NextCallerApi.Entities.Common;

using UrlParameter = System.Collections.Generic.KeyValuePair<string, string>;

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

		protected string baseUrl;
		protected readonly string usersUrl;
		protected readonly string phoneUrl;
		protected readonly string fraudUrl;
        protected readonly string analyzeUrl;
        protected readonly string versionNumber;

		protected readonly string formatParameterName;
		protected readonly string phoneParameterName;
        protected readonly string emailParameterName;

        protected readonly string nameAddressFirstNameParameterName;
        protected readonly string nameAddressLastNameParameterName;
        protected readonly string nameAddressAddressParameterName;
        protected readonly string nameAddressZipParameterName;
        protected readonly string nameAddressCityParameterName;
        protected readonly string nameAddressStateParameterName;

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
			Utility.EnsureParameterValid(!(username == null), "username");
			Utility.EnsureParameterValid(!(password == null), "password");

			httpTransport = new HttpTransport(username, password);
		}

		/// <summary>
		/// Initializes NextCaller client private fields.
		/// </summary>
		/// <param name="sandbox">Sandbox mode flag</param>
        protected NextCallerClient(bool sandbox = false)
		{
			isSandboxOn = sandbox;
		    versionNumber = Properties.Resources.DefaultAPIVersion;

			baseUrl = isSandboxOn ? Properties.Resources.SandboxUrl  : Properties.Resources.WorkingUrl;
		    baseUrl += ("v" + versionNumber + "/");

			usersUrl = baseUrl + Properties.Resources.UsersPath;
			phoneUrl = baseUrl + Properties.Resources.PhonePath;
			fraudUrl = baseUrl + Properties.Resources.FraudPath;
            analyzeUrl = baseUrl + Properties.Resources.AnalyzePath;

			formatParameterName = Properties.Resources.FormatUrlParameterName;
			phoneParameterName = Properties.Resources.PhoneUrlParameterName;
            emailParameterName = Properties.Resources.EmailUrlParameterName;
		    nameAddressFirstNameParameterName = Properties.Resources.NameAddressFirstNameUrlParameterName;
            nameAddressLastNameParameterName = Properties.Resources.NameAddressLastNameUrlParameterName;
            nameAddressAddressParameterName = Properties.Resources.NameAddressAddressUrlParameterName;
            nameAddressZipParameterName = Properties.Resources.NameAddressZipUrlParameterName;
            nameAddressCityParameterName = Properties.Resources.NameAddressCityUrlParameterName;
            nameAddressStateParameterName = Properties.Resources.NameAddressStateUrlParameterName;
		}

		#region Api

		/// <summary>
		/// Gets list of profiles, associated with the particular phone number.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/get-profile-phone/curl.
		/// </summary>
		/// <param name="phone">Phone number.</param>
		/// <returns>Profiles, associated with the particular phone number.</returns>
		public IList<Profile> GetByPhone(string phone)
		{
            Utility.EnsureParameterValid(!(phone == null), "phone");

            string content = GetByPhoneJson(phone);

            return JsonSerializer.ParseProfileList(content);
		}

        /// <summary>
        /// Gets list of profiles associated with the particular email.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/retrieve-profile-email/curl.
        /// </summary>
        /// <param name="email">Email to search by.</param>
        /// <returns>Profiles, associated with the particular email.</returns>
        public IList<Profile> GetByEmail(string email)
        {
            Utility.EnsureParameterValid(!(email == null), "email");

            string content = GetByEmailJson(email);

            return JsonSerializer.ParseProfileList(content);
        }

		/// <summary>
		/// Gets profile, associated with the particular ID.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/get-profile-id/curl.
		/// </summary>
		/// <param name="id">Profile id.</param>
		/// <returns>Profile, associated with the particular ID.</returns>
		public Profile GetByProfileId(string id)
		{
            Utility.EnsureParameterValid(!(id == null), "id");

            string content = GetByProfileIdJson(id);

            return JsonSerializer.Deserialize<Profile>(content);
		}

        /// <summary>
        /// Gets list of profiles, associated with the particular name-address or name-zip pair.
        /// Throws an exception if response status is 404.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/retreive-profile-name-address/curl.
        /// </summary>
        /// <param name="pair">Pair of name and address or name and zip code.</param>
        /// <returns>Profiles, associated with the particular name-address or name-zip pair.</returns>
        public IList<Profile> GetByNameAddress(NameAddress pair)
        {
            Utility.EnsureParameterValid(!(pair == null), "pair");

            string content = GetByNameAddressJson(pair);

            return JsonSerializer.ParseProfileList(content);
        }

		/// <summary>
		/// Gets fraud level for given phone number.
		/// More information at: https://nextcaller.com/documentation/v2.1/#/fraud-levels/curl.
		/// </summary>
		/// <param name="phone">Phone number.</param>
		/// <returns>Fraud level for given phone number.</returns>
		public FraudLevel GetFraudLevel(string phone)
		{
            Utility.EnsureParameterValid(!(phone == null), "phone");

            string content = GetFraudLevelJson(phone);

            return JsonSerializer.Deserialize<FraudLevel>(content);
		}

        /// <summary>
        /// Retrives fraud level for given call data.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/fraud-levels/curl.
        /// </summary>
        /// <param name="data">Call data to be posted.</param>
        /// <returns>Fraud level for given call data.</returns>
        public FraudLevel AnalyzeCall(AnalyzeCallData data)
        {
            Utility.EnsureParameterValid(!(data == null), "data");

            string jsonData = JsonSerializer.Serialize(data);
            string content = AnalyzeCallJson(jsonData);

            return JsonSerializer.Deserialize<FraudLevel>(content);
        }

        /// <summary>
        /// Updates profile with given id.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/post-profile/curl
        /// </summary>
        /// <param name="data">Profile data to be updated.</param>
        /// <param name="id">Id of the updated profile.</param>
        public void UpdateByProfileId(string id, ProfileToPost data)
		{
            Utility.EnsureParameterValid(!(id == null), "id");
            Utility.EnsureParameterValid(!(data == null), "data");

            string jsonData = JsonSerializer.Serialize(data);
			UpdateByProfileIdJson(id, jsonData);

		}

        #endregion Api

        #region RawApi

        /// <summary>
        /// Gets profile, associated with the particular ID, in json format.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/get-profile-id/curl.
        /// </summary>
        /// <param name="id">Profile id.</param>
        /// <returns>Profile, associated with the particular ID.</returns>
        public string GetByProfileIdJson(string id)
		{
			string url = BuildUrl(usersUrl + id, new UrlParameter(formatParameterName, DefaultResponseType.ToString()));
			return httpTransport.Request(url, DefaultResponseType);
		}

        /// <summary>
        /// Gets list of profiles, associated with the particular email, in json format.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/retrieve-profile-email/curl.
        /// </summary>
        /// <param name="email">Email to search by.</param>
        /// <returns>Profiles, associated with the particular email.</returns>
        public string GetByEmailJson(string email)
        {
            string url = BuildUrl(phoneUrl, new UrlParameter(emailParameterName, email ?? ""),
                new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

            return httpTransport.Request(url, DefaultResponseType);
        }

        /// <summary>
        /// Gets list of profiles, associated with the particular phone, in json format.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/get-profile-phone/curl.
        /// </summary>
        /// <param name="phone">Phone number.</param>
        /// <returns>Profiles, associated with the particular phone.</returns>
        public string GetByPhoneJson(string phone)
		{
			string url = BuildUrl(phoneUrl, new UrlParameter(phoneParameterName, phone), 
                new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

            return httpTransport.Request(url, DefaultResponseType);
		}

        /// <summary>
        /// Gets list of profiles, associated with the particular name-address or name-zip pair.
        /// Throws an exception if response status is 404.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/retrieve-profile-name-address/curl.
        /// </summary>
        /// <param name="pair">Pair of name and address or name and zip code.</param>
        /// <returns>Profiles, associated with the particular name-address or name-zip pair.</returns>
        public string GetByNameAddressJson(NameAddress pair)
        {
            var parameters = new []
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

            return httpTransport.Request(url, DefaultResponseType);
        }

		/// <summary>
		/// Gets fraud level for given phone number in json format.
		/// More information at: https://nextcaller.com/documentation/v2.1/#/fraud-levels/curl.
		/// </summary>
		/// <param name="phone">Phone number.</param>
		/// <returns>Fraud level for given phone number in json format.</returns>
		public string GetFraudLevelJson(string phone)
		{
			string url = BuildUrl(fraudUrl , new UrlParameter(phoneParameterName, phone), 
                new UrlParameter(formatParameterName, DefaultResponseType.ToString()));

			return httpTransport.Request(url, DefaultResponseType);
		}

        /// <summary>
        /// Retrives fraud level for given call data in json format.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/fraud-levels/curl.
        /// </summary>
        /// <param name="data">Call data to be posted.</param>
        /// <returns>Fraud level for given call data in json format.</returns>
        public string AnalyzeCallJson(string data)
        {
            string url = BuildUrl(analyzeUrl, new UrlParameter(formatParameterName, PostContentType.ToString()));
            return httpTransport.Request(url, PostContentType, "POST", data ?? "");
        }

        /// <summary>
        /// Updates profile with given id.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/post-profile/curl.
        /// </summary>
        /// <param name="data">Profile data to be updated in Json.</param>
        /// <param name="id">Id of the updated profile.</param>
        public void UpdateByProfileIdJson(string id, string data)
		{
            string url = BuildUrl(usersUrl + id, new UrlParameter(formatParameterName, PostContentType.ToString()));
			httpTransport.Request(url, PostContentType, "POST", data ?? "");

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
