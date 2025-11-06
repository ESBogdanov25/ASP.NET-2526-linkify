using Microsoft.EntityFrameworkCore;
using Linkify.Domain.Entities;
using Linkify.Domain.Interfaces.Repositories;
using Linkify.Infrastructure.Data;

namespace Linkify.Infrastructure.Repositories;

public class LikeRepository : ILikeRepository
{
    private readonly ApplicationDbContext _context;

    public LikeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Like?> GetLikeAsync(int userId, int postId)
    {
        return await _context.Likes
            .FirstOrDefaultAsync(l => l.UserId == userId && l.PostId == postId);
    }

    public async Task<int> GetLikeCountAsync(int postId)
    {
        return await _context.Likes
            .CountAsync(l => l.PostId == postId);
    }

    public async Task<bool> UserLikedPostAsync(int userId, int postId)
    {
        return await _context.Likes
            .AnyAsync(l => l.UserId == userId && l.PostId == postId);
    }

    public async Task AddAsync(Like like)
    {
        await _context.Likes.AddAsync(like);
    }

    public void Remove(Like like)
    {
        _context.Likes.Remove(like);
    }
}