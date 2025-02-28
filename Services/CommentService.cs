using forms.Models;
using Microsoft.AspNetCore.SignalR;

namespace forms.Services;

public class CommentService
{
    private readonly IHubContext<Hub> _hubContext;
    public event Action<Comment>? OnCommentAdded;

    public CommentService(IHubContext<Hub> hubContext)
    {
        _hubContext = hubContext;
    }

    public void AddComment(Comment newComment)
    {
        _hubContext.Clients.Group(newComment.TemplateId.ToString());
        OnCommentAdded?.Invoke(newComment);
    }
}
