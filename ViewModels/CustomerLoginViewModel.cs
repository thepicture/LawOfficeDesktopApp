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
        public LoginUser User { get; set; }
        public CustomerLoginViewModel()
        {
            Title = "Авторизация";
            User = new LoginUser();
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
                    .GetService<IRepository<LoginUser>>()
                    .CreateAsync(User))
            {
                Navigator.Go<CustomerControlPanelViewModel>();
            }
        }
    }
}