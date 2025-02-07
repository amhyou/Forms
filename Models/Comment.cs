using System.ComponentModel.DataAnnotations;
using forms.Data;

namespace forms.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }

    public int TemplateId { get; set; }
    public Template? Template { get; set; }

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }

    [Required]
    [MaxLength(500)]
    public string Text { get; set; } = string.Empty;
}
