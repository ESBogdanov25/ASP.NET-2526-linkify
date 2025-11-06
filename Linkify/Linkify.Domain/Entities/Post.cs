using Linkify.Domain.Common;

namespace Linkify.Domain.Entities;

public class Post : BaseEntity
{
    public string Content { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }

    // Foreign key
    public int UserId { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
}