using System.Collections.Generic;
using System.Runtime.Serialization;


namespace NextCallerApi.Entities.Common
{
    /// <summary>
    /// Represents call data object that might be posted to the Next Caller Call Analyzer API.
    /// </summary>
    [DataContract]
    public class AnalyzeCallData
    {
        [DataMember(Name = "ani")]
        public string Ani { get; set; }

        [DataMember(Name = "dnis")]
        public string Dnis { get; set; }

        [DataMember(Name = "headers")]
        public Dictionary<string, object> Headers { get; set; }

        [DataMember(Name = "meta")]
        public Dictionary<string, string> Meta { get; set; }
    }
}
