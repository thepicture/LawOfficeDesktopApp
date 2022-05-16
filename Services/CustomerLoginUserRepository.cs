using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Models.UserModels;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace LawOfficeDesktopApp.Services
{
    public class CustomerLoginUserRepository : IRepository<CustomerLoginUser>
    {
        public async Task<bool> CreateAsync(CustomerLoginUser item)
        {
            return await Task.Run(() =>
            {
                using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
                {
                    if (entities.Users
                            .Include(u => u.UserRole)
                            .AsNoTracking()
                            .FirstOrDefault(u => u.Login == item.Login)
                                is User currentUser)
                    {
                        if (Enumerable.SequenceEqual(currentUser.PasswordHash, item.PasswordHash))
                        {
                            App.CurrentUser = currentUser;
                            Ioc.Default
                                .GetService<INotificationService>()
                                .NotifyAsync("Вы авторизованы");
                            return true;
                        }
                    }
                }
                Ioc.Default
                    .GetService<INotificationService>()
                    .NotifyErrorAsync("Неверный логин или пароль");
                return false;
            });
        }

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerLoginUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerLoginUser> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CustomerLoginUser item)
        {
            throw new NotImplementedException();
        }
    }
}
