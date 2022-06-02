using LawOfficeDesktopApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LawOfficeDesktopApp.Services
{
    public class RoleRepository : IRepository<UserRole>
    {
        public Task<bool> CreateAsync(UserRole item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
            {
                return await entities.UserRoles.ToListAsync();
            }
        }

        public Task<UserRole> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(UserRole item)
        {
            throw new NotImplementedException();
        }
    }
}
