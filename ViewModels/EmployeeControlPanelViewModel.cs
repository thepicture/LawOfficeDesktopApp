using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Xaml.Behaviors.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
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
            StrongReferenceMessenger.Default
                .Register<string, string>(nameof(LoadCustomersAsync),
                                          (_, __) => LoadCustomersAsync());
            StrongReferenceMessenger.Default
                .Register<string>(nameof(LoadConsultationsAsync),
                                  (_, __) => LoadConsultationsAsync());
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

        private User selectedCustomer;

        public User SelectedCustomer
        {
            get => selectedCustomer;
            set
            {
                SetProperty(ref selectedCustomer, value);
                DeleteCustomerCommand.NotifyCanExecuteChanged();
            }
        }

        private Consultation selectedConsultation;

        public Consultation SelectedConsultation
        {
            get => selectedConsultation;
            set
            {
                SetProperty(ref selectedConsultation, value);
                DeleteConsultationCommand.NotifyCanExecuteChanged();
            }
        }

        private RelayCommand deleteCustomerCommand;

        public RelayCommand DeleteCustomerCommand
        {
            get
            {
                if (deleteCustomerCommand == null)
                    deleteCustomerCommand = new RelayCommand(DeleteCustomerAsync,
                                                             () => SelectedCustomer != null);

                return deleteCustomerCommand;
            }
        }

        private async void DeleteCustomerAsync()
        {
            if (await NotificationService.AskAsync("Удалить данные о клиенте?"))
            {
                if (await Ioc.Default
                        .GetService<IRepository<User>>()
                        .DeleteAsync(SelectedCustomer.Id))
                {
                    LoadCustomersAsync();
                }
            }
        }

        private RelayCommand deleteConsultationCommand;

        public RelayCommand DeleteConsultationCommand
        {
            get
            {
                if (deleteConsultationCommand == null)
                    deleteConsultationCommand = new RelayCommand(DeleteConsultationAsync,
                                                                 () => SelectedConsultation != null);

                return deleteConsultationCommand;
            }
        }

        private async void DeleteConsultationAsync()
        {
            if (await NotificationService.AskAsync("Удалить данные о консультации?"))
            {
                if (await Ioc.Default
                        .GetService<IRepository<Consultation>>()
                        .DeleteAsync(SelectedConsultation.Id))
                {
                    LoadConsultationsAsync();
                }
            }
        }

        private RelayCommand goToAddCustomerViewModel;

        public ICommand GoToAddCustomerViewModel
        {
            get
            {
                if (goToAddCustomerViewModel == null)
                    goToAddCustomerViewModel = new RelayCommand(PerformGoToAddCustomerViewModel);

                return goToAddCustomerViewModel;
            }
        }

        private void PerformGoToAddCustomerViewModel()
        {
            Navigator.Go<AddCustomerViewModel>();
        }

        private RelayCommand goToAddConsultationViewModel;

        public ICommand GoToAddConsultationViewModel
        {
            get
            {
                if (goToAddConsultationViewModel == null)
                    goToAddConsultationViewModel = new RelayCommand(PerformGoToAddConsultationViewModel);

                return goToAddConsultationViewModel;
            }
        }

        private void PerformGoToAddConsultationViewModel()
        {
            Navigator.Go<AddConsultationViewModel>();
        }
    }
}