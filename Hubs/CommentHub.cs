using Microsoft.AspNetCore.SignalR;
using forms.Services;

public class CommentHub : Hub
{
    private readonly CommentService _commentService;

    public CommentHub(CommentService commentService)
    {
        _commentService = commentService;
    }

    public async Task SendComment(string postId, string user, string message)
    {
        await _commentService.AddComment(postId, user, message);
    }
}
