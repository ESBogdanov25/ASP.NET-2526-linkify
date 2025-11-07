namespace Linkify.Application.Interfaces;

public interface INotificationService
{
    Task NotifyNewLikeAsync(int userId, int postId, int likedByUserId);
    Task NotifyNewCommentAsync(int userId, int postId, int commentedByUserId);
    Task NotifyNewFollowerAsync(int userId, int followerId);
}