using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Xaml.Behaviors.Core;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    public class AddRequestViewModel : ViewModelBase
    {
        public CustomerRequest Request { get; set; } = new CustomerRequest
        {
            CustomerId = App.CurrentUser.Id
        };
        public AddRequestViewModel()
        {
            Title = "Оставить заявку";
        }

        private ActionCommand addRequest;

        public ICommand AddRequest
        {
            get
            {
                if (addRequest == null)
                    addRequest = new ActionCommand(PerformAddRequestAsync);

                return addRequest;
            }
        }

        private async void PerformAddRequestAsync()
        {
            if (await Ioc.Default
                   .GetService<IRepository<CustomerRequest>>()
                   .CreateAsync(Request))
            {
                Navigator.GoBack();
            }
        }
    }
}