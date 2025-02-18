using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace forms.Services;

public class CommentService
{
    private readonly IHubContext<CommentHub> _hubContext;
    private static readonly ConcurrentDictionary<string, List<string>> _comments = new();

    public event Action<string>? OnCommentAdded;

    public CommentService(IHubContext<CommentHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task AddComment(string postId, string user, string message)
    {
        if (!_comments.ContainsKey(postId))
            _comments[postId] = new List<string>();

        _comments[postId].Add($"{user}: {message}");

        // Notify all connected clients in this post group
        await _hubContext.Clients.Group(postId).SendAsync("ReceiveComment", user, message);

        OnCommentAdded?.Invoke(postId);
    }

    public List<string> GetComments(string postId) => _comments.GetValueOrDefault(postId) ?? new();
}
