namespace NetHttp;

public class GuidHandler : DelegatingHandler
{
    private readonly GuidProvider _guidProvider;

    public GuidHandler(GuidProvider guidProvider, HttpMessageHandler? innerHandler = null)
    {
        _guidProvider = guidProvider;

        if (innerHandler is not null)
        {
            InnerHandler = innerHandler;
        }
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine(_guidProvider.GuidValue);

        return base.SendAsync(request, cancellationToken);
    }
}
