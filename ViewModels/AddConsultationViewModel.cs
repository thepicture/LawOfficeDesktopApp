using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class AddConsultationViewModel : ViewModelBase
    {
        public Consultation Consultation { get; set; }
        public ObservableCollection<Service> Services { get; set; }
        public ObservableCollection<User> Customers { get; set; }
        public AddConsultationViewModel()
        {
            Title = "Запись клиента на консультацию";
            Consultation = new Consultation();
            LoadCustomersAsync();
            LoadServicesAsync();
        }

        private async void LoadServicesAsync()
        {
            IEnumerable<Service> currentCustomers = await Ioc.Default
                .GetService<IRepository<Service>>()
                .GetAllAsync();
            Services = new ObservableCollection<Service>(currentCustomers);
        }

        private async void LoadCustomersAsync()
        {
            IEnumerable<User> currentCustomers = await Ioc.Default
                .GetService<IRepository<User>>()
                .GetAllAsync();
            currentCustomers = currentCustomers.Where(u => u.RoleId == 1);
            Customers = new ObservableCollection<User>(currentCustomers);
        }

        private RelayCommand saveConsultation;

        public RelayCommand SaveConsultation
        {
            get
            {
                if (saveConsultation == null)
                    saveConsultation = new RelayCommand(PerformSaveConsultationAsync);

                return saveConsultation;
            }
        }

        private async void PerformSaveConsultationAsync()
        {
            if (await Ioc.Default
                    .GetService<IRepository<Consultation>>()
                    .CreateAsync(Consultation))
            {
                Navigator.GoBack();
                StrongReferenceMessenger.Default
                    .Send("LoadConsultationsAsync");
            }
        }
    }
}