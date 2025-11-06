using Linkify.Domain.Entities;

namespace Linkify.Domain.Interfaces.Repositories;

public interface IFollowRepository
{
    Task<Follow?> GetFollowAsync(int followerId, int followedId);
    Task<bool> IsFollowingAsync(int followerId, int followedId);
    Task<int> GetFollowerCountAsync(int userId);
    Task<int> GetFollowingCountAsync(int userId);
    Task<IEnumerable<User>> GetFollowersAsync(int userId);
    Task<IEnumerable<User>> GetFollowingAsync(int userId);
    Task AddAsync(Follow follow);
    void Remove(Follow follow);
}