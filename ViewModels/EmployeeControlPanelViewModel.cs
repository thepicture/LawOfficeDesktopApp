using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Xaml.Behaviors.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    public class EmployeeControlPanelViewModel : ViewModelBase
    {

        private ActionCommand goToPriceViewModel;
        public ObservableCollection<User> Customers { get; set; }
        public ObservableCollection<Consultation> Consultations { get; set; }

        public ICommand GoToPriceViewModel
        {
            get
            {
                if (goToPriceViewModel == null)
                    goToPriceViewModel = new ActionCommand(PerformGoToPriceViewModel);

                return goToPriceViewModel;
            }
        }

        private void PerformGoToPriceViewModel()
        {
            Navigator.Go<PriceViewModel>();
        }

        private ActionCommand goToOurEmployeesViewModel;

        public EmployeeControlPanelViewModel()
        {
            Title = "Адвокатское бюро - "
                    + $"авторизован как {App.CurrentUser.LastName} "
                    + $"{App.CurrentUser.FirstName}";
            LoadCustomersAsync();
            LoadConsultationsAsync();
        }

        private async void LoadConsultationsAsync()
        {
            IEnumerable<Consultation> currentConsulations = await Ioc.Default
                .GetService<IRepository<Consultation>>()
                .GetAllAsync();
            Consultations = new ObservableCollection<Consultation>(currentConsulations);
        }

        private async void LoadCustomersAsync()
        {
            IEnumerable<User> currentCustomers = await Ioc.Default
                .GetService<IRepository<User>>()
                .GetAllAsync();
            currentCustomers = currentCustomers.Where(u => u.RoleId == 1);
            Customers = new ObservableCollection<User>(currentCustomers);
        }

        public ICommand GoToOurEmployeesViewModel
        {
            get
            {
                if (goToOurEmployeesViewModel == null)
                    goToOurEmployeesViewModel = new ActionCommand(PerformGoToOurEmployeesViewModel);

                return goToOurEmployeesViewModel;
            }
        }

        private void PerformGoToOurEmployeesViewModel()
        {
            Navigator.Go<OurEmployeesViewModel>();
        }
    }
}