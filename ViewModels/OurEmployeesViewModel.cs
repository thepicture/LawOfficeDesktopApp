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
    public class OurEmployeesViewModel : ViewModelBase
    {
        public OurEmployeesViewModel()
        {
            Title = "Наши работники";
            LoadOurEmployeesAsync();
            StrongReferenceMessenger.Default
               .Register<string, string>(nameof(LoadOurEmployeesAsync),
                                         (_, __) => LoadOurEmployeesAsync());
        }

        private async void LoadOurEmployeesAsync()
        {
            IEnumerable<User> users = await Ioc.Default
                .GetService<IRepository<User>>()
                .GetAllAsync();
            users = users.Where(u => u.RoleId == 2);
            OurEmployees = new ObservableCollection<User>(users);
        }

        public ObservableCollection<User> OurEmployees { get; set; }

        private RelayCommand changeEmployeeCommand;

        public RelayCommand ChangeEmployeeCommand
        {
            get
            {
                if (changeEmployeeCommand == null)
                    changeEmployeeCommand = new RelayCommand(ChangeEmployee,
                                                             () => SelectedEmployee != null);

                return changeEmployeeCommand;
            }
        }

        private void ChangeEmployee()
        {
            Navigator.Go<AddEditEmployeeViewModel, User>(SelectedEmployee);
        }

        private ActionCommand addEmployeeCommand;

        public ICommand AddEmployeeCommand
        {
            get
            {
                if (addEmployeeCommand == null)
                    addEmployeeCommand = new ActionCommand(AddEmployee);

                return addEmployeeCommand;
            }
        }

        private void AddEmployee()
        {
            Navigator.Go<AddEditEmployeeViewModel>();
        }

        private RelayCommand deleteEmployeeCommand;

        public RelayCommand DeleteEmployeeCommand
        {
            get
            {
                if (deleteEmployeeCommand == null)
                    deleteEmployeeCommand = new RelayCommand(DeleteEmployeeAsync, () => SelectedEmployee != null);

                return deleteEmployeeCommand;
            }
        }

        private async void DeleteEmployeeAsync()
        {
            if (await NotificationService.AskAsync("Удалить данные о сотруднике?"))
            {
                if (await Ioc.Default
                        .GetService<IRepository<User>>()
                        .DeleteAsync(SelectedEmployee.Id))
                {
                    LoadOurEmployeesAsync();
                }
            }
        }

        private User selectedEmployee;

        public User SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                SetProperty(ref selectedEmployee, value);
                DeleteEmployeeCommand.NotifyCanExecuteChanged();
                ChangeEmployeeCommand.NotifyCanExecuteChanged();
            }
        }
    }
}