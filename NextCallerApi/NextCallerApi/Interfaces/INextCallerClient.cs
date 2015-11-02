using System.Collections.Generic;
using NextCallerApi.Entities.Common;


namespace NextCallerApi.Interfaces
{
    public interface INextCallerClient
    {
        /// <summary>
        /// Gets list of profiles, associated with the particular phone number.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/get-profile-phone/curl.
        /// </summary>
        /// <param name="phone">Phone number.</param>
        /// <returns>Profiles, associated with the particular phone number.</returns>
        IList<Profile> GetByPhone(string phone);

        /// <summary>
        /// Gets list of profiles, associated with the particular email.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/retrieve-profile-email/curl.
        /// </summary>
        /// <param name="email">Email to search by.</param>
        /// <returns>Profiles, associated with the particular email.</returns>
        IList<Profile> GetByEmail(string email);

        /// <summary>
        /// Gets list of profiles, associated with the particular name-address or name-zip pair.
        /// Throws an exception if response status is 404.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/retreive-profile-name-address/curl.
        /// </summary>
        /// <param name="nameAddressData">Pair of name and address or name and zip code.</param>
        /// <returns>Profiles, associated with the particular name-address or name-zip pair.</returns>
        IList<Profile> GetByNameAddress(NameAddress nameAddressData);

        /// <summary>
        /// Gets profile, associated with the particular ID.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/get-profile-id/curl.
        /// </summary>
        /// <param name="profileId">Profile id.</param>
        /// <returns>Profile, associated with the particular ID.</returns>
        Profile GetByProfileId(string profileId);

        /// <summary>
        /// Updates profile with given id.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/profiles/post-profile/curl
        /// </summary>
        /// <param name="profileData">Profile data to be updated.</param>
        /// <param name="profileId">Id of the updated profile.</param>
        void UpdateByProfileId(string profileId, ProfileToPost profileData);

        /// <summary>
        /// Gets fraud level for given phone number.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/fraud-levels/curl.
        /// </summary>
        /// <param name="phone">Phone number.</param>
        /// <returns>Fraud level for given phone number.</returns>
        FraudLevel GetFraudLevel(string phone);

        /// <summary>
        /// Retrives fraud level for given call data.
        /// More information at: https://nextcaller.com/documentation/v2.1/#/fraud-levels/curl.
        /// </summary>
        /// <param name="callData">Call data to be posted.</param>
        /// <returns>Fraud level for given call data.</returns>
        FraudLevel AnalyzeCall(AnalyzeCallData callData);
    }
}
