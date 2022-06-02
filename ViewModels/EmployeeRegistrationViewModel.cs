using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Models.UserModels;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Xaml.Behaviors.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class EmployeeRegistrationViewModel : ViewModelBase
    {
        public RegistrationUser User { get; set; }
        public EmployeeRegistrationViewModel()
        {
            Title = "Регистрация как персонал";
            User = new RegistrationUser();
            User.PropertyChanged += (_, __) =>
            {
                OnPropertyChanged(nameof(IsEmployeeValid));
            };
            LoadRolesAsync();
        }

        private async void LoadRolesAsync()
        {
            IRepository<UserRole> roleRepository = Ioc.Default.GetService<IRepository<UserRole>>();
            IEnumerable<UserRole> currentRoles = await roleRepository.GetAllAsync();
            currentRoles = currentRoles.Where(r => r.Title != "Клиент");
            Roles = new ObservableCollection<UserRole>(currentRoles);
        }

        public ObservableCollection<UserRole> Roles { get; set; }

        private ActionCommand registerCommand;

        public ICommand RegisterCommand
        {
            get
            {
                if (registerCommand == null)
                    registerCommand = new ActionCommand(RegisterAsync);

                return registerCommand;
            }
        }

        private async void RegisterAsync()
        {
            if (await Ioc.Default
                    .GetService<IRepository<RegistrationUser>>()
                    .CreateAsync(User))
            {
                Navigator.Go<EmployeeLoginViewModel>();
            }
        }

        public bool IsEmployeeValid
        {
            get
            {
                return User.UserRole != null
                       && !string.IsNullOrWhiteSpace(User.Login)
                       && !string.IsNullOrWhiteSpace(User.PlainPassword);
            }
        }
    }
}