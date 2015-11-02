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
    }
}
