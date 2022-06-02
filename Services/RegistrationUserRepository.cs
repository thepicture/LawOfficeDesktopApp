using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Models.UserModels;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawOfficeDesktopApp.Services
{
    public class RegistrationUserRepository : IRepository<RegistrationUser>
    {
        public async Task<bool> CreateAsync(RegistrationUser item)
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
                    if (item.UserRole != null)
                    {
                        item.RoleId = item.UserRole.Id;
                        item.UserRole = null;
                    }
                    User user = new User
                    {
                        Login = item.Login,
                        PhoneNumber = item.PhoneNumber,
                        RoleId = item.RoleId,
                        LastName = item.LastName,
                        FirstName = item.FirstName,
                        ExperienceInYears = item.ExperienceInYears,
                        PlainPassword = item.PlainPassword,
                    };
                    entities.Users.Add(user);
                    try
                    {
                        entities.SaveChanges();
                        if (App.IsAddingUser)
                        {
                            Ioc.Default
                                .GetService<INotificationService>()
                                .NotifyAsync("Данные добавлены");
                        }
                        else
                        {

                            Ioc.Default
                                .GetService<INotificationService>()
                                .NotifyAsync("Вы зарегистрированы");
                        }
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

        public Task<IEnumerable<RegistrationUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RegistrationUser> GetSingleAsync(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(RegistrationUser item)
        {
            return await Task.Run(() =>
            {
                using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
                {
                    var userFromDatabase = entities.Users
                        .First(u => u.Id == item.Id);
                    entities.Entry(userFromDatabase).CurrentValues.SetValues(item);
                    try
                    {
                        entities.SaveChanges();
                        if (App.IsAddingUser)
                        {
                            Ioc.Default
                                .GetService<INotificationService>()
                                .NotifyAsync("Данные изменены");
                        }
                        else
                        {

                            Ioc.Default
                                .GetService<INotificationService>()
                                .NotifyAsync("Вы зарегистрированы");
                        }
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
