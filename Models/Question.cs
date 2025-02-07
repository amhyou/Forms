using System.ComponentModel.DataAnnotations;
namespace forms.Models;

public class Question
{
    [Key]
    public int Id { get; set; }
    public int TemplateId { get; set; }
    public Template? Template { get; set; }

    [Required]
    [MaxLength(255)]
    public string Text { get; set; } = string.Empty;

    public QuestionType Type { get; set; }

    public List<QuestionOption> Options { get; set; } = new();
}

public enum QuestionType
{
    ShortAnswer,    // One-line response
    Paragraph,      // Multi-line response
    Integer,        // Positive integer
    Checkbox,       // One answer from options
}

public class QuestionOption
{
    [Key]
    public int Id { get; set; }

    public int QuestionId { get; set; }
    public Question? Question { get; set; }

    [Required]
    [MaxLength(100)]
    public string Option { get; set; } = string.Empty;
}