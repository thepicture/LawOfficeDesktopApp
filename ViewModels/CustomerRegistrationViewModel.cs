using LawOfficeDesktopApp.Models.UserModels;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Xaml.Behaviors.Core;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CustomerRegistrationViewModel : ViewModelBase
    {
        public CustomerRegistrationUser User { get; set; } = new CustomerRegistrationUser();
        public CustomerRegistrationViewModel()
        {
            Title = "Регистрация";
        }

        private ActionCommand registerAsCustomer;

        public ICommand RegisterAsCustomer
        {
            get
            {
                if (registerAsCustomer == null)
                    registerAsCustomer = new ActionCommand(PerformRegisterAsCustomerAsync);

                return registerAsCustomer;
            }
        }

        private async void PerformRegisterAsCustomerAsync()
        {
            if (await Ioc.Default
                    .GetService<IRepository<CustomerRegistrationUser>>()
                    .CreateAsync(User))
            {

            }
        }
    }
}
