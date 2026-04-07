using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using forgetmenot.API.Services;
using forgetmenot.API.DTOs;

namespace forgetmenot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CommandsController : ControllerBase
{
    private readonly CommandDispatcherService _dispatcher;
    private readonly NotesService _notesService;

    public CommandsController(CommandDispatcherService dispatcher, NotesService notesService)
    {
        _dispatcher = dispatcher;
        _notesService = notesService;
    }

    [HttpPost]
    public async Task<IActionResult> Dispatch([FromBody] DispatchRequestDto request)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        var command = await _dispatcher.DispatchAsync(request.Input);

        return command.Function switch
        {
            "createNote" => Ok(await _notesService.CreateNoteAsync(
                userId,
                new CreateNoteDto { UserNote = command.Parameters["userNote"].ToString()! }
            )),
            "getNotes" => Ok(await _notesService.GetNotesAsync(userId)),
            "deleteNote" => Ok(await _notesService.DeleteNoteAsync(
                userId,
                long.Parse(command.Parameters["id"].ToString()!)
            )),
            _ => BadRequest(new { message = "Could not understand command" })
        };
    }
}