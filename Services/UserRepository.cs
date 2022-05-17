using LawOfficeDesktopApp.Models.Entities;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace LawOfficeDesktopApp.Services
{
    public class UserRepository : IRepository<User>
    {
        public Task<bool> CreateAsync(User item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
            {
                return await entities.Users.ToListAsync();
            }
        }

        public async Task<User> GetSingleAsync(object id)
        {
            using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
            {
                return await entities.Users
                    .FirstAsync(u =>
                        u.Id.ToString() == id.ToString());
            }
        }

        public async Task<bool> UpdateAsync(User item)
        {
            return await Task.Run(() =>
            {
                using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
                {
                    try
                    {
                        entities
                            .Entry(
                                entities.Users.Find(item.Id)).CurrentValues
                            .SetValues(item);
                        entities.SaveChanges();
                        App.CurrentUser = item;
                        Ioc.Default
                            .GetService<INotificationService>()
                            .NotifyAsync("Данные изменены");
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
    }
}
