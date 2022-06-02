using LawOfficeDesktopApp.Models.UserModels;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace LawOfficeDesktopApp.ViewModels
{
    public class AddCustomerViewModel : ViewModelBase
    {
        public RegistrationUser User { get; set; }
        public AddCustomerViewModel()
        {
            Title = "Добавление клиента";
            User = new RegistrationUser
            {
                RoleId = 1
            };
        }

        private RelayCommand registerAsCustomer;

        public RelayCommand RegisterAsCustomer
        {
            get
            {
                if (registerAsCustomer == null)
                    registerAsCustomer = new RelayCommand(PerformRegisterAsCustomerAsync);

                return registerAsCustomer;
            }
        }

        private async void PerformRegisterAsCustomerAsync()
        {
            App.IsAddingUser = true;
            if (await Ioc.Default
                    .GetService<IRepository<RegistrationUser>>()
                    .CreateAsync(User))
            {
                Navigator.GoBack();
                StrongReferenceMessenger.Default
                    .Send("LoadCustomersAsync");
            }
            App.IsAddingUser = true;
        }
    }
}