using System.Diagnostics.Eventing.Reader;
using NextCallerApi.Entities.Common;
using System.Collections.Generic;

namespace NextCallerApi.Interfaces
{
	public interface IHttpTransport
	{
        string Request(string url, ContentType contentType, string method = "GET", string data = null, IEnumerable<Header> headers = null);
	}
}
