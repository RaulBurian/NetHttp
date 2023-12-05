using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace NetHttp;

public static class SocketHttp
{
    private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public static async Task<T> GetRequest<T>(Uri uri, CancellationToken cancellationToken = default)
    {
        var requestBody = $"""
                           GET {uri.AbsolutePath} HTTP/1.1
                           Host: {uri.Host}
                           Connection: Close


                           """;

        using var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        await socket.ConnectAsync(uri.Host, uri.Port, cancellationToken);

        var endHeadersSearchBytes = "\r\n\r\n"u8.ToArray();
        var requestBytes = Encoding.UTF8.GetBytes(requestBody);
        var bytesSent = 0;

        while (bytesSent < requestBytes.Length)
        {
            bytesSent += await socket.SendAsync(requestBytes.AsMemory(bytesSent), SocketFlags.None, cancellationToken);
        }

        var responseBytes = new byte[256];

        var responseStream = new MemoryStream();

        while (true)
        {
            var bytesReceived = await socket.ReceiveAsync(responseBytes, SocketFlags.None, cancellationToken);

            if (bytesReceived == 0)
            {
                break;
            }

            await responseStream.WriteAsync(responseBytes.AsMemory(0, bytesReceived), cancellationToken);
        }

        var responseBuffer = responseStream.GetBuffer();
        var indexOfBodyStart = responseBuffer.AsSpan().IndexOf(endHeadersSearchBytes) + endHeadersSearchBytes.Length;

        return JsonSerializer.Deserialize<T>(responseBuffer.AsSpan()[indexOfBodyStart..(int)responseStream.Length], SerializerOptions)!;
    }

    public static async Task<T> GetRequestSsl<T>(Uri uri, CancellationToken cancellationToken = default)
    {
        var requestBody = $"""
                           GET {uri.AbsolutePath} HTTP/1.1
                           Host: {uri.Host}
                           Connection: Close


                           """;

        using var tcpClient = new TcpClient(uri.Host, uri.Port);
        await using var sslStream = new SslStream(tcpClient.GetStream());

        await sslStream.AuthenticateAsClientAsync(uri.Host);

        var endHeadersSearchBytes = "\r\n\r\n"u8.ToArray();
        var requestBytes = Encoding.UTF8.GetBytes(requestBody);

        await sslStream.WriteAsync(requestBytes, cancellationToken);

        var responseBytes = new byte[tcpClient.ReceiveBufferSize];
        var responseStream = new MemoryStream();

        while (true)
        {
            var bytesReceived = await sslStream.ReadAsync(responseBytes, cancellationToken);

            if (bytesReceived == 0)
            {
                break;
            }

            await responseStream.WriteAsync(responseBytes.AsMemory(0, bytesReceived), cancellationToken);
        }

        var responseBuffer = responseStream.GetBuffer();
        var indexOfBodyStart = responseBuffer.AsSpan().IndexOf(endHeadersSearchBytes) + endHeadersSearchBytes.Length;

        return JsonSerializer.Deserialize<T>(responseBuffer.AsSpan()[indexOfBodyStart..(int)responseStream.Length], SerializerOptions)!;
    }

    public static async Task Examples()
    {
        var indented = new JsonSerializerOptions { WriteIndented = true };

        var result = await GetRequest<WeatherForecast[]>(new Uri("http://localhost:5099/weatherforecast"));
        var resultSsl = await GetRequestSsl<WeatherForecast[]>(new Uri("https://localhost:7036/weatherforecast"));

        Console.WriteLine(JsonSerializer.Serialize(result, indented));
        Console.WriteLine(JsonSerializer.Serialize(resultSsl , indented));
    }
}
