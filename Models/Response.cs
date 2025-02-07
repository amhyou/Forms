using System.ComponentModel.DataAnnotations;
using forms.Data;

namespace forms.Models;
public class Response
{
    [Key]
    public int Id { get; set; }

    public int FormId { get; set; }
    public Form? Form { get; set; }

    public string? AuthorId { get; set; }  // User who answered
    public ApplicationUser? Author { get; set; }

    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    public List<ResponseAnswer> Answers { get; set; } = new();
}


public class ResponseAnswer
{
    [Key]
    public int Id { get; set; }

    public int ResponseId { get; set; }
    public Response? Response { get; set; }

    public int QuestionId { get; set; }
    public Question? Question { get; set; }

    [Required]
    [MaxLength(1000)]
    public string? Answer { get; set; }
}
