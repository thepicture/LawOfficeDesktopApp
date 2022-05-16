using LawOfficeDesktopApp.Models.UserModels;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Xaml.Behaviors.Core;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CustomerLoginViewModel : ViewModelBase
    {
        public CustomerLoginUser User { get; set; } = new CustomerLoginUser();
        public CustomerLoginViewModel()
        {
            Title = "Авторизация";
        }

        private ActionCommand loginAsCustomer;

        public ICommand LoginAsCustomer
        {
            get
            {
                if (loginAsCustomer == null)
                    loginAsCustomer = new ActionCommand(PerformLoginAsCustomerAsync);

                return loginAsCustomer;
            }
        }

        private async void PerformLoginAsCustomerAsync()
        {
            if (await Ioc.Default
                    .GetService<IRepository<CustomerLoginUser>>()
                    .CreateAsync(User))
            {
                Navigator.Go<CustomerControlPanelViewModel>();
            }
        }
    }
}