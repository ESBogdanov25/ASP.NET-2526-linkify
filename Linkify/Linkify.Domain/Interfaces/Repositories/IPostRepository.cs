using Linkify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Domain.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(int id);
        Task<IEnumerable<Post>> GetUserPostsAsync(int userId);
        Task<IEnumerable<Post>> GetFeedPostsAsync(int userId); // Posts from followed users
        Task AddAsync(Post post);
        void Update(Post post);
        void Delete(Post post);
    }
}
