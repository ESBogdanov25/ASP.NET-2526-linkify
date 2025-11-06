using Linkify.Domain.Interfaces.Repositories;

namespace Linkify.Domain.Interfaces;

public interface IUnitOfWork
{
    IUserRepository Users { get; }
    IPostRepository Posts { get; }
    ILikeRepository Likes { get; }
    Task<int> SaveChangesAsync();
}