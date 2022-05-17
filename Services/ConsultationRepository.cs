using LawOfficeDesktopApp.Models.Entities;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LawOfficeDesktopApp.Services
{
    public class ConsultationRepository : IRepository<Consultation>
    {
        public async Task<bool> CreateAsync(Consultation item)
        {
            return await Task.Run(() =>
            {
                using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
                {
                    item.ServiceId = item.Service.Id;
                    item.Service = null;
                    item.UserId = item.User.Id;
                    item.User = null;
                    entities.Consultations.Add(item);
                    try
                    {
                        entities.SaveChanges();
                        Ioc.Default
                            .GetService<INotificationService>()
                            .NotifyAsync("Вы записали клиента на консультацию");
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
                        Consultation consultation = entities.Consultations
                            .First(c =>
                                c.Id.ToString()
                                == idAsString);
                        entities.Consultations.Remove(consultation);
                        entities.SaveChanges();
                        Ioc.Default
                            .GetService<INotificationService>()
                            .NotifyAsync("Консультация удалена");
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
