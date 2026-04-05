namespace forgetmenot.API.DTOs;

public class NoteDto
{
    public long Id { get; set; }
    public string UserNote { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}