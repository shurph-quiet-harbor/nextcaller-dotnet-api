namespace NextCallerApi.Interfaces
{
	public interface IHttpTransport
	{
		string Request(string url, ContentType contentType, string data = null);
	}
}
