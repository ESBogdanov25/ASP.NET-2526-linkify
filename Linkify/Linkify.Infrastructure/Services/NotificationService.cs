using Linkify.Application.Interfaces;
using Linkify.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Linkify.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyNewLikeAsync(int userId, int postId, int likedByUserId)
    {
        await _hubContext.Clients.Group($"user-{userId}")
            .SendAsync("ReceiveLikeNotification", new
            {
                PostId = postId,
                LikedByUserId = likedByUserId,
                Message = "Someone liked your post!",
                Timestamp = DateTime.UtcNow
            });
    }

    public async Task NotifyNewCommentAsync(int userId, int postId, int commentedByUserId)
    {
        await _hubContext.Clients.Group($"user-{userId}")
            .SendAsync("ReceiveCommentNotification", new
            {
                PostId = postId,
                CommentedByUserId = commentedByUserId,
                Message = "Someone commented on your post!",
                Timestamp = DateTime.UtcNow
            });
    }

    public async Task NotifyNewFollowerAsync(int userId, int followerId)
    {
        await _hubContext.Clients.Group($"user-{userId}")
            .SendAsync("ReceiveFollowNotification", new
            {
                FollowerId = followerId,
                Message = "You have a new follower!",
                Timestamp = DateTime.UtcNow
            });
    }
}