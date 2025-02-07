using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace forms.Models;

[Index(nameof(Name), IsUnique = true)]
public class Topic
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
}