using BimApp.API.Entities;
using BimApp.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BimApp.API.Dtos;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace BimApp.API.Data
{
    public class UserRepository : IUserRepository
    {
        private DataContext _dataContext;
        private IMapper _mapper;

        public UserRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<MemberDto> GetMemberAsync(string userName)
        {
            return await _dataContext.Users
                .Where(x => x.UserName == userName)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _dataContext.Users
                  .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                  .ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetUserAsync()
        {
            return await _dataContext.Users
                .Include(p => p.Photos)
                .ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return await _dataContext.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _dataContext.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(c => c.UserName == username);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _dataContext.Entry(user).State = EntityState.Modified;
        }
    }
}
