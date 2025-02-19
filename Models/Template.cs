using System.ComponentModel.DataAnnotations;
using forms.Data;

namespace forms.Models;
public class Template
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(3000)]
    public string Description { get; set; } = string.Empty;

    public int TopicId { get; set; }
    public Topic? Topic { get; set; }

    [MaxLength(255)]
    public string? ImageUrl { get; set; }

    public string? CreatorId { get; set; }
    public ApplicationUser? Creator { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsPublic { get; set; } = true;
    public List<ApplicationUser> AllowedUsers { get; set; } = new();

    public List<Tag> Tags { get; set; } = new();
    public List<Question> Questions { get; set; } = new();
    public List<Form> Forms { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    public List<ApplicationUser> Likes { get; set; } = new();
}