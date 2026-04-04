using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using forgetmenot.API.Data;

namespace forgetmenot.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly AppDbContext _context;

    public NotesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("health")]
    public async Task<IActionResult> Health()
    {
        var count = await _context.Notes.CountAsync();
        return Ok(new { status = "connected", noteCount = count });
    }
}