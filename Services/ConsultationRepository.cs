using LawOfficeDesktopApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LawOfficeDesktopApp.Services
{
    public class ConsultationRepository : IRepository<Consultation>
    {
        public Task<bool> CreateAsync(Consultation item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Consultation>> GetAllAsync()
        {
            using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
            {
                return await entities.Consultations
                    .Include(c => c.User)
                    .Include(c => c.Service)
                    .ToListAsync();
            }
        }

        public Task<Consultation> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Consultation item)
        {
            throw new NotImplementedException();
        }
    }
}
