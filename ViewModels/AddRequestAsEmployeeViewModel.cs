using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class AddRequestAsEmployeeViewModel : ViewModelBase
    {
        public CustomerRequest Request { get; set; }
        public ObservableCollection<Service> Services { get; set; }
        public ObservableCollection<User> Customers { get; set; }
        public AddRequestAsEmployeeViewModel()
        {
            Title = "Запись клиента на консультацию";
            Request = new CustomerRequest();
            LoadCustomersAsync();
            LoadServicesAsync();
        }

        private async void LoadServicesAsync()
        {
            IEnumerable<Service> currentServices = await Ioc.Default
                .GetService<IRepository<Service>>()
                .GetAllAsync();
            Services = new ObservableCollection<Service>(currentServices);
        }

        private async void LoadCustomersAsync()
        {
            IEnumerable<User> currentCustomers = await Ioc.Default
                .GetService<IRepository<User>>()
                .GetAllAsync();
            currentCustomers = currentCustomers.Where(u => u.RoleId == 1);
            Customers = new ObservableCollection<User>(currentCustomers);
        }

        private RelayCommand saveRequest;

        public RelayCommand SaveRequest
        {
            get
            {
                if (saveRequest == null)
                    saveRequest = new RelayCommand(PerformSaveRequestAsync);

                return saveRequest;
            }
        }

        private async void PerformSaveRequestAsync()
        {
            if (await Ioc.Default
                    .GetService<IRepository<CustomerRequest>>()
                    .CreateAsync(Request))
            {
                Navigator.GoBack();
                StrongReferenceMessenger.Default
                    .Send("LoadRequestsAsync");
            }
        }
    }
}