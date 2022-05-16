using LawOfficeDesktopApp.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LawOfficeDesktopApp.Services
{
    public class ServiceRepository : IRepository<Service>
    {
        public Task<bool> CreateAsync(Service item)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
            {
                return await entities.Services.ToListAsync();
            }
        }

        public Task<Service> GetSingleAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(Service item)
        {
            throw new System.NotImplementedException();
        }
    }
}
