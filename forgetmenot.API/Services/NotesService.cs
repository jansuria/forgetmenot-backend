using Microsoft.EntityFrameworkCore;
using forgetmenot.API.Data;
using forgetmenot.API.DTOs;
using forgetmenot.API.Models;

namespace forgetmenot.API.Services;

public class NotesService
{
    private readonly AppDbContext _context;

    public NotesService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<NoteDto>> GetNotesAsync(string userId)
    {
        return await _context.Notes
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => new NoteDto
            {
                Id = n.Id,
                UserNote = n.UserNote,
                CreatedAt = n.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<NoteDto> CreateNoteAsync(string userId, CreateNoteDto dto)
    {
        var note = new Note
        {
            UserId = userId,
            UserNote = dto.UserNote,
            CreatedAt = DateTime.UtcNow
        };

        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        return new NoteDto
        {
            Id = note.Id,
            UserNote = note.UserNote,
            CreatedAt = note.CreatedAt
        };
    }

    public async Task<bool> DeleteNoteAsync(string userId, long noteId)
    {
        var note = await _context.Notes
            .FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId);

        if (note == null) return false;

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();
        return true;
    }
}