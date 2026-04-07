namespace forgetmenot.API.DTOs;

public class CommandResultDto
{
    public string Function { get; set; } = string.Empty;
    public Dictionary<string, object> Parameters { get; set; } = new();
}