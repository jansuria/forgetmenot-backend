namespace forgetmenot.API.Middleware;

public class ApiKeyHttpHandler : HttpClientHandler
{
    private readonly string _apiKey;

    public ApiKeyHttpHandler(string apiKey)
    {
        _apiKey = apiKey;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("apikey", _apiKey);
        return base.SendAsync(request, cancellationToken);
    }
}