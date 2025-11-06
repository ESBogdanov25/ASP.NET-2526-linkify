using Linkify.Domain.Entities;

namespace Linkify.Domain.Interfaces.Repositories;

public interface ILikeRepository
{
    Task<Like?> GetLikeAsync(int userId, int postId);
    Task<int> GetLikeCountAsync(int postId);
    Task<bool> UserLikedPostAsync(int userId, int postId);
    Task AddAsync(Like like);
    void Remove(Like like);
}