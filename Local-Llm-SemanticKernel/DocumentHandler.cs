using System.Text.Json;
using Ai_File_Watcher;
using Microsoft.SemanticKernel;

namespace Local_Llm;

internal class DocumentHandler
{
    public async Task<Document> GetDocumentFromAi(string fileText)
    {
        var customHttpMessageHandler = new CustomHttpMessageHandler();
        customHttpMessageHandler.CustomLlmUrl = "http://localhost:1234";
        HttpClient client = new HttpClient(customHttpMessageHandler);

// create kernel
        var builder = Kernel.CreateBuilder();
        builder.AddOpenAIChatCompletion("llama2", "api-key", httpClient: client);
        var kernel = builder.Build();

// invoke a simple prompt to the chat service
        var prompt = PrompProvider.GetPrompt(fileText);
        var response = await kernel.InvokePromptAsync(prompt);
        Console.WriteLine(response.GetValue<string>());
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var document = JsonSerializer.Deserialize<Document>(response.GetValue<string>(), options);
        return document?? new Document();
    }

}