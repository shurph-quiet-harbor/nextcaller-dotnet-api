namespace NextCallerApi.Interfaces
{
	internal interface IHttpTransport
	{
		string Request(string url, ContentType contentType, string data = null);
	}
}
