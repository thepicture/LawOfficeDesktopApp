using LawOfficeDesktopApp.Models.Entities;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawOfficeDesktopApp.Services
{
    public class CustomerRequestRepository : IRepository<CustomerRequest>
    {
        public async Task<bool> CreateAsync(CustomerRequest item)
        {
            if (item.PhoneNumber == null)
            {
                item.PhoneNumber = item.User.PhoneNumber;
            }
            if (item.User != null)
            {
                item.CustomerId = item.User.Id;
                item.User = null;
            }
            if (item.Service != null)
            {
                item.ServiceId = item.Service.Id;
                item.Service = null;
            }
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

        public async Task<bool> DeleteAsync(object id)
        {
            return await Task.Run(() =>
            {
                using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
                {
                    try
                    {
                        string idAsString = id.ToString();
                        CustomerRequest request = entities.CustomerRequests
                            .First(r =>
                                r.Id.ToString()
                                == idAsString);
                        entities.CustomerRequests.Remove(request);
                        entities.SaveChanges();
                        Ioc.Default
                            .GetService<INotificationService>()
                            .NotifyAsync("Заявка удалена");
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

        public async Task<IEnumerable<CustomerRequest>> GetAllAsync()
        {
            using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
            {
                return await entities.CustomerRequests
                    .Include(r => r.Service)
                    .Include(r => r.User)
                    .ToListAsync();
            }
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
