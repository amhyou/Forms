using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace forms.Models;


[Index(nameof(Name), IsUnique = true)]
public class Tag
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public List<TemplateTag> TemplateTags { get; set; } = new();
}

public class TemplateTag
{
    public int TemplateId { get; set; }
    public Template? Template { get; set; }

    public int TagId { get; set; }
    public Tag? Tag { get; set; }
}