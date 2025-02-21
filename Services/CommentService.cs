using forms.Models;
using Microsoft.AspNetCore.SignalR;
using forms.Hubs;

namespace forms.Services;

public class CommentService
{
    private readonly IHubContext<CommentHub> _hubContext;
    public event Action<Comment>? OnCommentAdded;

    public CommentService(IHubContext<CommentHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task AddComment(Comment newComment)
    {
        await _hubContext.Clients.Group(newComment.TemplateId.ToString()).SendAsync("SendComment", newComment);
        OnCommentAdded?.Invoke(newComment);
    }
}
