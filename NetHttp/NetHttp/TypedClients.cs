namespace NetHttp;

public class TypedClientOne(HttpClient httpClient)
{
    public string BaseUrl { get; } = httpClient.BaseAddress!.ToString();
}

public class TypedClientTwo(HttpClient httpClient)
{
    public string BaseUrl { get; } = httpClient.BaseAddress!.ToString();
}
