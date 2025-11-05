using Linkify.Domain.Entities;
using Linkify.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkify.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
