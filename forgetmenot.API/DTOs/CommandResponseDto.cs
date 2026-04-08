namespace forgetmenot.API.DTOs;

public class CommandResponseDto
{
    public string Function { get; set; } = string.Empty;
    public NoteDto? Note { get; set; }
    public List<NoteDto>? Notes { get; set; }
}