using forms.Data;

namespace forms.Models;

public class AllowedUser
{
    public int FormId { get; set; }
    public Form? Form { get; set; }

    public string? UserId { get; set; }
    public ApplicationUser? User { get; set; }
}