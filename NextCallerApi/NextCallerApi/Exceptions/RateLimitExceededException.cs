using System;
using System.Net;

namespace NextCallerApi.Exceptions
{
    class RateLimitExceededException : BaseException
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
                return unixTimeEpoch.AddMilliseconds(limitResetValue.Value.TotalMilliseconds);
            }
            return null;
        }

        /// <summary>
        /// Initializes RateLimitExceededException
        /// </summary>
        /// <param name="request">Failed request</param>
        /// <param name="response">Response</param>
        /// <param name="content">Response content</param>
        public RateLimitExceededException(HttpWebRequest request, HttpWebResponse response, string content)
            : base(request, response, content)
        {
            var limitReset = response.Headers["X-RateLimit-Reset"];
            var limitRemainig = response.Headers["X-RateLimit-Remaining"];
            var limitLimit = response.Headers["X-RateLimit-Limit"];
            limitLimitValue = limitLimit == null ? limitLimitValue.GetValueOrDefault() : long.Parse(limitLimit);
            limitResetValue = limitReset == null ? limitResetValue.GetValueOrDefault() : TimeSpan.FromMilliseconds(long.Parse(limitReset));
            limitRemainigValue = limitRemainig == null ? limitRemainigValue.GetValueOrDefault() : long.Parse(limitRemainig);
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
                + (limitRemainigValue.HasValue ? " number of requests left: {1}" : "")
                + (limitResetUTCDate.HasValue ? "remaning window before rate limit resets: {2}" : "")
                + " {3}";

			return string.Format(template, limitLimitValue, limitRemainigValue, limitResetUTCDate, Content);
		}
    }
}
