using Linkify.Domain.Interfaces;
using Linkify.Domain.Interfaces.Repositories;
using Linkify.Infrastructure.Data;
using Linkify.Infrastructure.Repositories;

namespace Linkify.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IUserRepository _users;
    private IPostRepository _posts;
    private ILikeRepository _likes;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IUserRepository Users => _users ??= new UserRepository(_context);
    public IPostRepository Posts => _posts ??= new PostRepository(_context);
    public ILikeRepository Likes => _likes ??= new LikeRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}