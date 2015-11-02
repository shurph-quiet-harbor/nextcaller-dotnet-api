using System;
using System.Net;

namespace NextCallerApi.Exceptions
{
    /// <summary>
	/// Thrown when "429 Too Many Requests" error occured.
	/// </summary>
    public class RateLimitExceededException : BaseException
    {
        public long? limitLimitValue;
        public long? limitRemainigValue;
        public TimeSpan? limitResetValue;

        public DateTime? limitResetUTCDate
        {
            get { return prepareLimitReset(); }
        }

        private DateTime? prepareLimitReset()
        {
            if (!limitResetValue.Value.Equals(null))
            {
                var unixTimeEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                return unixTimeEpoch.Add(limitResetValue.Value);
            }
            return null;
        }

        /// <summary>
        /// Initializes RateLimitExceededException
        /// </summary>
        /// <param name="request">Failed request.</param>
        /// <param name="response">Response.</param>
        /// <param name="content">Response content.</param>
        public RateLimitExceededException(HttpWebRequest request, HttpWebResponse response, string content)
            : base(request, response, content)
        {
            var limitReset = response.Headers["X-Rate-Limit-Reset"];
            var limitRemainig = response.Headers["X-Rate-Limit-Remaining"];
            var limitLimit = response.Headers["X-Rate-Limit-Limit"];
            limitLimitValue = null;
            limitRemainigValue = null;
            limitResetValue = null;
            if (limitLimit != null) limitLimitValue = long.Parse(limitLimit);
            if (limitReset != null) limitResetValue = TimeSpan.FromSeconds(long.Parse(limitReset));
            if (limitRemainig != null) limitRemainigValue = long.Parse(limitRemainig);
        }

        public override string Message
        {
            get
            {
                return ToString();
            }
        }

        public override string ToString()
        {
            string template = "Rate Limit Exceeded."
                + (limitLimitValue.HasValue ? " Current rate limit value is {0} per second;" : "")
                + (limitRemainigValue.HasValue ? " number of requests left: {1};" : "")
                + (limitResetUTCDate.HasValue ? " remaning window before rate limit resets: {2};" : "")
                + " {3}";

            return string.Format(template, limitLimitValue, limitRemainigValue, limitResetUTCDate, Content);
        }
    }
}
