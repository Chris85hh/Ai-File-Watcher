namespace Local_Llm;

internal sealed class CustomHttpMessageHandler : HttpClientHandler
{
    public string CustomLlmUrl { get; set; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string[] urls = { "api.openai.com", "openai.azure.com" };

        // validate if request.RequestUri is not null and request.RequestUri.Host is in urls
        if (request.RequestUri != null && urls.Contains(request.RequestUri.Host))
        {
            // set request.RequestUri to a new Uri with the LLMUrl and request.RequestUri.PathAndQuery
            request.RequestUri = new Uri($"{CustomLlmUrl}{request.RequestUri.PathAndQuery}");
        }

        return base.SendAsync(request, cancellationToken);
    }
}