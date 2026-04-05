using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using forgetmenot.API.DTOs;
using forgetmenot.API.Services;


namespace forgetmenot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotesController : ControllerBase
{
    private readonly NotesService _notesService;

    public NotesController(NotesService notesService)
    {
        _notesService = notesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetNotes()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        var notes = await _notesService.GetNotesAsync(userId);
        return Ok(notes);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNote([FromBody] CreateNoteDto dto)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        var note = await _notesService.CreateNoteAsync(userId, dto);
        return Ok(note);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(long id)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        var deleted = await _notesService.DeleteNoteAsync(userId, id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}