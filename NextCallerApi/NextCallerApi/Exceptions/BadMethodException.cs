using System;
using System.Net;

using NextCallerApi.Entities;

namespace NextCallerApi.Exceptions
{
    /// <summary>
    /// Thrown in case of bad method of the HTTP request.
    /// </summary>
    public class BadMethodException : Exception
    {

        /// <summary>
        /// Failed method name.
        /// </summary>
        public string Method
        {
            get;
            private set;
        }

        public override string Message
        {
            get
            {
                return ToString();
            }
        }

        /// <summary>
        /// Initializes BadMethodException instance.
        /// </summary>
        /// <param name="method">Failed method name.</param>
        public BadMethodException(string method)
        {
            Method = method;
        }

        public override string ToString()
        {
            string template = "Bad Method Exception: method name was '{0}'." + Environment.NewLine;
            return string.Format(template, Method);
        }
    }
}
