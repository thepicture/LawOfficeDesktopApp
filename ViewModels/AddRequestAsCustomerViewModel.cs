using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class AddRequestAsCustomerViewModel : ViewModelBase
    {
        public CustomerRequest Request { get; set; } = new CustomerRequest
        {
            CustomerId = App.CurrentUser.Id
        };
        public AddRequestAsCustomerViewModel()
        {
            Title = "Оставить заявку";
            LoadServicesAsync();
        }

        private async void LoadServicesAsync()
        {
            IEnumerable<Service> currentServices = await Ioc.Default
               .GetService<IRepository<Service>>()
               .GetAllAsync();
            Services = new ObservableCollection<Service>(currentServices);
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

        public ObservableCollection<Service> Services { get; set; }
    }
}