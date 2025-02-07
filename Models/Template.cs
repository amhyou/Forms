using System.ComponentModel.DataAnnotations;
using forms.Data;

namespace forms.Models;
public class Template
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

    public string? CreatorId { get; set; }
    public ApplicationUser? Creator { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public TemplateType Type { get; set; }
    public List<AllowedUser> AllowedUsers { get; set; } = new();

    public List<TemplateTag> FormTags { get; set; } = new();
    public List<Question> Questions { get; set; } = new();
    public List<Form> Forms { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}


public enum TemplateType
{
    Public,
    Restricted
}