using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace forgetmenot.API.Models;

[Table("notes")]
public class Note
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    [Column("note")]
    public string UserNote { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}