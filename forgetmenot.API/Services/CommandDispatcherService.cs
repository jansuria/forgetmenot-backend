using Anthropic.SDK;
using Anthropic.SDK.Messaging;
using forgetmenot.API.DTOs;


namespace forgetmenot.API.Services;

public class CommandDispatcherService
{
    private readonly AnthropicClient _anthropic;

    public CommandDispatcherService(AnthropicClient anthropic)
    {
        _anthropic = anthropic;
    }

public async Task<CommandResultDto> DispatchAsync(string userInput)
{
    var systemPrompt = """
        You are a command dispatcher. Map user input to exactly one function.
        
        Functions:
        - createNote: saves a note. params: { "userNote": string }
        - getNotes: retrieves all notes. params: {}
        - deleteNote: deletes a note. params: { "id": number }
        
        Respond ONLY with JSON, no other text:
        {"function":"functionName","parameters":{}}
        
        If no match: {"function":"unknown","parameters":{}}
        """;

    var response = await _anthropic.Messages.GetClaudeMessageAsync(new MessageParameters
    {
        Model = "claude-sonnet-4-20250514",
        MaxTokens = 100,
        System = [new SystemMessage(systemPrompt)],
        Messages = [new Message(RoleType.User, userInput)]
    });

    var raw = response.Content.OfType<TextContent>().First().Text;
    var result = System.Text.Json.JsonSerializer.Deserialize<CommandResultDto>(raw,
        new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    return result ?? new CommandResultDto { Function = "unknown" };
}
}