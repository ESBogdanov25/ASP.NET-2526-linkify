using Linkify.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Domain.Entities
{
    public class Like : BaseEntity
    {
        // Foreign keys
        public int UserId { get; set; }
        public int PostId { get; set; }

        // Navigation properties
        public virtual User User { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
