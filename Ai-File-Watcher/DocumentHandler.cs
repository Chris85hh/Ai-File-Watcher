using System.Text.Json;
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;

namespace Ai_File_Watcher;

public sealed class DocumentHandler
{
   public async Task<Document> GetDocumentFromAi(string fileText)
    {
        var builder = new ConfigurationBuilder()  
            .AddUserSecrets<DocumentHandler>();  
        var configuration = builder.Build();
        var openAiEndpoint = configuration["OpenAiEndpoint"];
        var openAiKey = configuration["OpenAiKey"];
        var deploymentName = configuration["DeploymentName"];
        
        if (openAiKey == null || openAiEndpoint == null || deploymentName == null)
        {
            Console.WriteLine("OpenAiKey oder OpenAiEndpoint nicht gefunden");
            return new Document();
        }

        var prompt =
            "Dein Job ist es meinen Inhalt zu Kategorisieren.\ndie Kategorien sind:\n-Rechnung\n-Angebot\n-Sonstiges\n\nDer Eigene Firmenname ist \"Fantasie Webdesign GmbH\", Ermittle den Firmenname vom Kunden.\n\nDu Antwortest ausschließlich nur mit einem JSON-Datensatz:\n{\n \"category\":\"Rechnung\",\n \"company\":\"Test Gmbh\"\n}\n###\n";
        OpenAIClient client = new OpenAIClient(
            new Uri(openAiEndpoint),
           new AzureKeyCredential(openAiKey));
        
        ChatCompletionsOptions completionsOptions = new()
        {
            DeploymentName = deploymentName,
            Messages =
            {
                new ChatRequestSystemMessage( prompt+fileText+"\n"),
            },
            Temperature = (float)0.7,
            MaxTokens = 800,
            NucleusSamplingFactor = (float)0.95,
            FrequencyPenalty = 0,
            PresencePenalty = 0,
        };

        Response<ChatCompletions> completionsResponse = await client.GetChatCompletionsAsync(completionsOptions);
        var completion = completionsResponse.Value.Choices[0].Message.Content;

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        Console.WriteLine(completion);
        var document = JsonSerializer.Deserialize<Document>(completion.TrimStart().TrimEnd(), options);
        
        return document ?? new Document();
        
    }
}