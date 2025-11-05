using Linkify.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        // Foreign key
        public int UserId { get; set; }

        // Navigation properties
        public virtual User User { get; set; } = null!;
    }
}
