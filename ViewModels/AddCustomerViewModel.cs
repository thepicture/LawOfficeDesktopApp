using LawOfficeDesktopApp.Models.UserModels;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace LawOfficeDesktopApp.ViewModels
{
    public class AddCustomerViewModel : ViewModelBase
    {
        public CustomerRegistrationUser User { get; set; }
        public AddCustomerViewModel()
        {
            Title = "Добавление клиента";
            User = new CustomerRegistrationUser();
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
            App.IsAddingCustomer = true;
            if (await Ioc.Default
                    .GetService<IRepository<CustomerRegistrationUser>>()
                    .CreateAsync(User))
            {
                Navigator.GoBack();
                StrongReferenceMessenger.Default
                    .Send("LoadCustomersAsync");
            }
            App.IsAddingCustomer = true;
        }
    }
}