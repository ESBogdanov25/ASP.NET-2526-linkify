using Linkify.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Domain.Entities
{
    public class Follow : BaseEntity
    {
        // Foreign keys
        public int FollowerId { get; set; }  // The one who is following
        public int FollowedId { get; set; }  // The one being followed

        // Navigation properties
        public virtual User Follower { get; set; } = null!;
        public virtual User Followed { get; set; } = null!;
    }
}
