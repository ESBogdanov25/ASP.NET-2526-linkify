using Microsoft.EntityFrameworkCore;
using Linkify.Domain.Entities;
using Linkify.Domain.Interfaces.Repositories;
using Linkify.Infrastructure.Data;

namespace Linkify.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _context;

    public PostRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _context.Posts
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Post>> GetUserPostsAsync(int userId)
    {
        return await _context.Posts
            .Include(p => p.User)
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Post>> GetFeedPostsAsync(int userId)
    {
        // Get posts from users that the current user follows
        var followedUserIds = await _context.Follows
            .Where(f => f.FollowerId == userId)
            .Select(f => f.FollowedId)
            .ToListAsync();

        // Include user's own posts in the feed
        followedUserIds.Add(userId);

        return await _context.Posts
            .Include(p => p.User)
            .Where(p => followedUserIds.Contains(p.UserId))
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
    }

    public void Update(Post post)
    {
        _context.Posts.Update(post);
    }

    public void Delete(Post post)
    {
        _context.Posts.Remove(post);
    }
}