using BimApp.API.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace BimApp.API.Data
{
    public interface IAuthRepository
    {
        Task<AppUser> Register(AppUser user, string password);

        Task<AppUser> Login(string username, string password);

        Task<bool> UserExists(string username);
    }
}
