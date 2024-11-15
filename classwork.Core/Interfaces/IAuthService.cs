using classwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classwork.Core.Interfaces
{
    public interface IAuthService
    {
        Task Register(string email, string password);
        Task<(User?, string)> Login(string email, string password);
    }
}
