using Linkify.Domain.Entities;

namespace Linkify.Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<Comment?> GetByIdAsync(int id);
    Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
    Task<int> GetCommentCountAsync(int postId);
    Task AddAsync(Comment comment);
    void Update(Comment comment);
    void Delete(Comment comment);
}