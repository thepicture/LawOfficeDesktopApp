using LawOfficeDesktopApp.Models.Entities;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LawOfficeDesktopApp.Services
{
    public class ServiceRepository : IRepository<Service>
    {
        public async Task<bool> CreateAsync(Service item)
        {
            return await Task.Run(() =>
            {
                using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
                {
                    if (!item.IsHasMaximumPrice)
                    {
                        item.MaximumPrice = null;
                    }
                    try
                    {
                        if (item.Id == 0)
                        {
                            entities.Services.Add(item);
                        }
                        else
                        {
                            entities
                                .Entry(
                                    entities.Services.First(s => s.Id == item.Id)).CurrentValues
                                .SetValues(item);
                        }
                        entities.SaveChanges();
                        Ioc.Default
                            .GetService<INotificationService>()
                            .NotifyAsync("Данные сохранены");
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
                        Service service = entities.Services
                            .First(s =>
                                s.Id.ToString()
                                == idAsString);
                        entities.Services.Remove(service);
                        entities.SaveChanges();
                        Ioc.Default
                            .GetService<INotificationService>()
                            .NotifyAsync("Услуга удалена");
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
