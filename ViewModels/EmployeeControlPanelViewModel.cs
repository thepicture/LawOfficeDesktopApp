using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Xaml.Behaviors.Core;
using System;
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
        public ObservableCollection<CustomerRequest> Requests { get; set; }

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
            Navigator.Navigated += () =>
            {
                LoadCustomersAsync();
                LoadRequests();
            };
        }

        private async void LoadRequests()
        {
            IEnumerable<CustomerRequest> currentRequests = await Ioc.Default
                .GetService<IRepository<CustomerRequest>>()
                .GetAllAsync();
            if (!string.IsNullOrWhiteSpace(RequestSearchText))
            {
                currentRequests = currentRequests.Where(c =>
                {
                    string rawPhoneNumber = string.Join("",
                                                        c.PhoneNumber.Where(p =>
                                                        {
                                                            return char.IsDigit(p);
                                                        }));
                    return rawPhoneNumber.Contains(RequestSearchText)
                           || c.User.Login.IndexOf(RequestSearchText,
                                                   StringComparison.OrdinalIgnoreCase) != -1
                           || c.Service.Title.IndexOf(RequestSearchText,
                                                      StringComparison.OrdinalIgnoreCase) != -1;
                });
            }
            Requests = new ObservableCollection<CustomerRequest>(currentRequests);
        }

        private async void LoadCustomersAsync()
        {
            IEnumerable<User> currentCustomers = await Ioc.Default
                .GetService<IRepository<User>>()
                .GetAllAsync();
            currentCustomers = currentCustomers.Where(u => u.RoleId == 1);
            if (!string.IsNullOrWhiteSpace(CustomerSearchText))
            {
                currentCustomers = currentCustomers.Where(c =>
                    {
                        string rawPhoneNumber = string.Join("",
                                                            c.PhoneNumber.Where(p =>
                                                            {
                                                                return char.IsDigit(p);
                                                            }));
                        return rawPhoneNumber.Contains(CustomerSearchText)
                               || c.Login.IndexOf(CustomerSearchText,
                                                  StringComparison.OrdinalIgnoreCase) != -1;
                    });
            }
            Customers = new ObservableCollection<User>(currentCustomers);
        }

        public ICommand GoToOurEmployeesViewModel
        {
            get
            {
                if (goToOurEmployeesViewModel == null)
                    goToOurEmployeesViewModel =
                        new ActionCommand(PerformGoToOurEmployeesViewModel);

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

        private CustomerRequest selectedRequest;

        public CustomerRequest SelectedRequest
        {
            get => selectedRequest;
            set
            {
                SetProperty(ref selectedRequest, value);
                DeleteRequestCommand.NotifyCanExecuteChanged();
            }
        }

        private RelayCommand deleteCustomerCommand;

        public RelayCommand DeleteCustomerCommand
        {
            get
            {
                if (deleteCustomerCommand == null)
                    deleteCustomerCommand =
                        new RelayCommand(DeleteCustomerAsync,
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

        private RelayCommand deleteRequestCommand;

        public RelayCommand DeleteRequestCommand
        {
            get
            {
                if (deleteRequestCommand == null)
                    deleteRequestCommand = new RelayCommand(DeleteRequestAsync,
                                                                 () => SelectedRequest != null);

                return deleteRequestCommand;
            }
        }

        private async void DeleteRequestAsync()
        {
            if (await NotificationService.AskAsync("Удалить данные о заявке?"))
            {
                if (await Ioc.Default
                        .GetService<IRepository<CustomerRequest>>()
                        .DeleteAsync(SelectedRequest.Id))
                {
                    LoadRequests();
                }
            }
        }

        private RelayCommand goToAddCustomerViewModel;

        public ICommand GoToAddCustomerViewModel
        {
            get
            {
                if (goToAddCustomerViewModel == null)
                    goToAddCustomerViewModel =
                        new RelayCommand(PerformGoToAddCustomerViewModel);

                return goToAddCustomerViewModel;
            }
        }

        private void PerformGoToAddCustomerViewModel()
        {
            Navigator.Go<AddCustomerViewModel>();
        }

        private RelayCommand goToAddRequestViewModel;

        public ICommand GoToAddRequestViewModel
        {
            get
            {
                if (goToAddRequestViewModel == null)
                    goToAddRequestViewModel =
                        new RelayCommand(PerformGoToAddRequestViewModel);

                return goToAddRequestViewModel;
            }
        }

        private void PerformGoToAddRequestViewModel()
        {
            Navigator.Go<AddRequestAsEmployeeViewModel>();
        }

        private string customerSearchText;

        public string CustomerSearchText
        {
            get => customerSearchText; set
            {
                if (SetProperty(ref customerSearchText, value))
                {
                    LoadCustomersAsync();
                }
            }
        }

        private string requestSearchText;

        public string RequestSearchText
        {
            get => requestSearchText; set
            {
                if (SetProperty(ref requestSearchText, value))
                {
                    LoadRequests();
                }
            }
        }
    }
}