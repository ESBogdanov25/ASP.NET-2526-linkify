using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto User { get; set; } = null!;
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
    }

    public class CreatePostDto
    {
        public string Content { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }

    public class UpdatePostDto
    {
        public string Content { get; set; } = string.Empty;
    }
}
