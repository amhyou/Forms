using System.ComponentModel.DataAnnotations;
using forms.Data;

namespace forms.Models;
public class Form
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public int TopicId { get; set; }
    public Topic? Topic { get; set; }

    [MaxLength(255)]
    public string? ImageUrl { get; set; }

    public string? AuthorId { get; set; }
    public ApplicationUser? Author { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public FormType Type { get; set; }
    public List<AllowedUser> AllowedUsers { get; set; } = new();

    public List<FormTag> FormTags { get; set; } = new();

    public List<Question> Questions { get; set; } = new();

    public List<Response> Responses { get; set; } = new();

    public List<Comment> Comments { get; set; } = new();
}


public enum FormType
{
    Public,
    Restricted
}