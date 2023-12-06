namespace NetHttp.Server;

public class Handler : DelegatingHandler
{
    private readonly GuidProvider _guidProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<Handler> _logger;

    public Handler(GuidProvider guidProvider, IHttpContextAccessor httpContextAccessor, ILogger<Handler> logger, HttpMessageHandler? innerHandler = null)
    {
        _guidProvider = guidProvider;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;

        if (innerHandler is not null)
        {
            InnerHandler = innerHandler;
        }
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(_guidProvider.GuidValue);

        return Task.FromResult(new HttpResponseMessage());
    }
}
