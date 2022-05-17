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
    public class LoginUserRepository : IRepository<LoginUser>
    {
        public async Task<bool> CreateAsync(LoginUser item)
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
                        if (currentUser.PlainPassword == item.PlainPassword)
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

        public Task<IEnumerable<LoginUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LoginUser> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(LoginUser item)
        {
            throw new NotImplementedException();
        }
    }
}
