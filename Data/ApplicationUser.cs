using Microsoft.AspNetCore.Identity;
using forms.Models;

namespace forms.Data;

public class ApplicationUser : IdentityUser
{
    public List<Template> Templates { get; set; } = new();
    public List<Template> AllowedTemplates { get; set; } = new();
    public List<Template> LikedTemplates { get; set; } = new();
}

