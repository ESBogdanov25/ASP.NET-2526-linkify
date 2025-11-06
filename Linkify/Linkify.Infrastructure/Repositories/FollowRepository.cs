using Microsoft.EntityFrameworkCore;
using Linkify.Domain.Entities;
using Linkify.Domain.Interfaces.Repositories;
using Linkify.Infrastructure.Data;

namespace Linkify.Infrastructure.Repositories;

public class FollowRepository : IFollowRepository
{
    private readonly ApplicationDbContext _context;

    public FollowRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Follow?> GetFollowAsync(int followerId, int followedId)
    {
        return await _context.Follows
            .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FollowedId == followedId);
    }

    public async Task<bool> IsFollowingAsync(int followerId, int followedId)
    {
        return await _context.Follows
            .AnyAsync(f => f.FollowerId == followerId && f.FollowedId == followedId);
    }

    public async Task<int> GetFollowerCountAsync(int userId)
    {
        return await _context.Follows
            .CountAsync(f => f.FollowedId == userId);
    }

    public async Task<int> GetFollowingCountAsync(int userId)
    {
        return await _context.Follows
            .CountAsync(f => f.FollowerId == userId);
    }

    public async Task<IEnumerable<User>> GetFollowersAsync(int userId)
    {
        return await _context.Follows
            .Where(f => f.FollowedId == userId)
            .Include(f => f.Follower)
            .Select(f => f.Follower)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetFollowingAsync(int userId)
    {
        return await _context.Follows
            .Where(f => f.FollowerId == userId)
            .Include(f => f.Followed)
            .Select(f => f.Followed)
            .ToListAsync();
    }

    public async Task AddAsync(Follow follow)
    {
        await _context.Follows.AddAsync(follow);
    }

    public void Remove(Follow follow)
    {
        _context.Follows.Remove(follow);
    }
}