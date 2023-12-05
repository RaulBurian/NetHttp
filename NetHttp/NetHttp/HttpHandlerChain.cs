namespace NetHttp;

public class HttpHandlerFirst : DelegatingHandler
{
    public HttpHandlerFirst(HttpMessageHandler? innerHandler = null)
    {
        if (innerHandler is not null)
        {
            InnerHandler = innerHandler;
        }
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine("First");

        var response = await base.SendAsync(request, cancellationToken);

        Console.WriteLine("First after");

        return response;
    }
}

public class HttpHandlerSecond : DelegatingHandler
{
    public HttpHandlerSecond(HttpMessageHandler? innerHandler = null)
    {
        if (innerHandler is not null)
        {
            InnerHandler = innerHandler;
        }
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Second");

        var response = await base.SendAsync(request, cancellationToken);

        Console.WriteLine("Second after");

        return response;
    }
}

public class HttpHandlerThird : DelegatingHandler
{
    public HttpHandlerThird(HttpMessageHandler? innerHandler = null)
    {
        if (innerHandler is not null)
        {
            InnerHandler = innerHandler;
        }
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Third");

        var response = await base.SendAsync(request, cancellationToken);

        Console.WriteLine("Third after");

        return response;
    }
}
