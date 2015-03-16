using System;
using System.Globalization;

namespace NextCallerApi.Entities.Common
{
    /// <summary>
    /// Represents a name-address or name-zip pair.
    /// </summary>
    public class NameAddress
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public int ZipCode { get; set; }

        public string AddressLine { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public ValidationResult IsValid()
        {
            return IsNameAddressValid(this);
        }

        public static ValidationResult IsNameAddressValid(NameAddress pair)
        {
            var message = "";
            const string invalidNameText = "Invalid first or last name";
            const string invalidAddressText = "Invalid address";
            var zipOrAddress = pair.ZipCode.ToString(CultureInfo.InvariantCulture).Length == 5 ||
                               (!String.IsNullOrEmpty(pair.City) && !String.IsNullOrEmpty(pair.State));
            if (!zipOrAddress)
            {
                message += "Invalid zip code or empty city and state";
            }
            var validName = !String.IsNullOrEmpty(pair.FirstName) && !String.IsNullOrEmpty(pair.LastName);
            var validAddress = !String.IsNullOrEmpty(pair.AddressLine);
            if (!validName)
            {
                message += String.IsNullOrEmpty(message) ? ", " + invalidNameText : invalidNameText;
            }
            if (validAddress) return new ValidationResult(zipOrAddress, message);
            message += String.IsNullOrEmpty(message) ? ", " + invalidAddressText : invalidAddressText;
            return new ValidationResult(false, message);
        }
    }
}
