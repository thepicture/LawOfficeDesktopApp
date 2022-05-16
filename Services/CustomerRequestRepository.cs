using LawOfficeDesktopApp.Models.Entities;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeDesktopApp.Services
{
    public class CustomerRequestRepository : IRepository<CustomerRequest>
    {
        public async Task<bool> CreateAsync(CustomerRequest item)
        {
            return await Task.Run(() =>
            {
                using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
                {
                    entities.CustomerRequests.Add(item);
                    try
                    {
                        entities.SaveChanges();
                        Ioc.Default
                            .GetService<INotificationService>()
                            .NotifyAsync("Заявка отправлена");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Ioc.Default
                            .GetService<INotificationService>()
                            .NotifyErrorAsync(ex);
                        return false;
                    }
                }
            });
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerRequest>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerRequest> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CustomerRequest item)
        {
            throw new NotImplementedException();
        }
    }
}
