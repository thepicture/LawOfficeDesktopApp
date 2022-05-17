using LawOfficeDesktopApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LawOfficeDesktopApp.Services
{
    public class ContactsRepository : IRepository<Contact>
    {
        public Task<bool> CreateAsync(Contact item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
            {
                return await entities.Contacts.ToListAsync();
            }
        }

        public Task<Contact> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Contact item)
        {
            throw new NotImplementedException();
        }
    }
}
