using System.ComponentModel.DataAnnotations;
using forms.Data;

namespace forms.Models;
public class Form
{
    [Key]
    public int Id { get; set; }

    public int TemplateId { get; set; }
    public Template? Template { get; set; }

    public string? AuthorId { get; set; }  // User who answered
    public ApplicationUser? Author { get; set; }

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    public List<Response> Responses { get; set; } = new();
}


public class Response
{
    [Key]
    public int Id { get; set; }

    public int FormId { get; set; }
    public Form? Form { get; set; }

    public int QuestionId { get; set; }
    public Question? Question { get; set; }

    [Required]
    [MaxLength(1000)]
    public string? Answer { get; set; }
}
