using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Models.UserModels;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawOfficeDesktopApp.Services
{
    public class CustomerRegistrationUserRepository : IRepository<CustomerRegistrationUser>
    {
        public async Task<bool> CreateAsync(CustomerRegistrationUser item)
        {
            return await Task.Run(() =>
            {
                using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
                {
                    if (entities.Users.Any(u =>
                                  u.Login.Equals(item.Login, StringComparison.OrdinalIgnoreCase)))
                    {
                        Ioc.Default
                           .GetService<INotificationService>()
                           .NotifyErrorAsync("Такой логин уже есть");
                        return false;
                    }
                    User user = new User
                    {
                        Login = item.Login,
                        RoleId = 1,
                        PasswordHash = item.PasswordHash,
                    };
                    entities.Users.Add(user);
                    try
                    {
                        entities.SaveChanges();
                        Ioc.Default
                            .GetService<INotificationService>()
                            .NotifyAsync("Вы зарегистрированы");
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

        public Task<IEnumerable<CustomerRegistrationUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerRegistrationUser> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CustomerRegistrationUser item)
        {
            throw new NotImplementedException();
        }
    }
}
