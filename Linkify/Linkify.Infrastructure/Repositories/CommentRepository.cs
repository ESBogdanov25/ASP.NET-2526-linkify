using Microsoft.EntityFrameworkCore;
using Linkify.Domain.Entities;
using Linkify.Domain.Interfaces.Repositories;
using Linkify.Infrastructure.Data;

namespace Linkify.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        return await _context.Comments
            .Include(c => c.User)
            .Where(c => c.PostId == postId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<int> GetCommentCountAsync(int postId)
    {
        return await _context.Comments
            .CountAsync(c => c.PostId == postId);
    }

    public async Task AddAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
    }

    public void Update(Comment comment)
    {
        _context.Comments.Update(comment);
    }

    public void Delete(Comment comment)
    {
        _context.Comments.Remove(comment);
    }
}