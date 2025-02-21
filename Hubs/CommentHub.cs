using Microsoft.AspNetCore.SignalR;
using forms.Services;
using forms.Models;

namespace forms.Hubs;

public class CommentHub : Hub
{
    private readonly CommentService _commentService;

    public CommentHub(CommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task SendComment(Comment newComment)
    {
        await _commentService.AddComment(newComment);
    }
}
