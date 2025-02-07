using forms.Data;

namespace forms.Models;

public class AllowedUser
{
    public int TemplateId { get; set; }
    public Template? Template { get; set; }

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
}