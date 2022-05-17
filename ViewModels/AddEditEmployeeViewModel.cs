using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Models.UserModels;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class AddEditEmployeeViewModel : ViewModelBase
    {
        public CustomerRegistrationUser Employee { get; set; }
        public AddEditEmployeeViewModel()
        {
            Title = "Добавление персонала";
            Employee = new CustomerRegistrationUser
            {
                RoleId = 2
            };
            Employee.PropertyChanged += (_, __) =>
            {
                OnPropertyChanged(
                    nameof(IsCanSaveEmployee));
            };
        }

        public AddEditEmployeeViewModel(User inputEmployee)
        {
            Title = "Редактирование персонала";
            Employee = new CustomerRegistrationUser
            {
                Id = inputEmployee.Id,
                Login = inputEmployee.Login,
                LastName = inputEmployee.LastName,
                FirstName = inputEmployee.FirstName,
                ExperienceInYears = inputEmployee.ExperienceInYears,
                RoleId = inputEmployee.RoleId
            };
            Employee.PropertyChanged += (_, __) =>
            {
                OnPropertyChanged(
                    nameof(IsCanSaveEmployee));
            };
        }

        private RelayCommand registerAsEmployee;

        public RelayCommand RegisterAsEmployee
        {
            get
            {
                if (registerAsEmployee == null)
                    registerAsEmployee = new RelayCommand(PerformRegisterAsEmployeeAsync);

                return registerAsEmployee;
            }
        }

        private async void PerformRegisterAsEmployeeAsync()
        {
            App.IsAddingUser = true;
            if (Employee.Id == 0)
            {

                if (await Ioc.Default
                        .GetService<IRepository<CustomerRegistrationUser>>()
                        .CreateAsync(Employee))
                {
                    Navigator.GoBack();
                    StrongReferenceMessenger.Default
                        .Send("LoadOurEmployeesAsync");
                }
            }
            else
            {
                if (await Ioc.Default
                        .GetService<IRepository<CustomerRegistrationUser>>()
                        .UpdateAsync(Employee))
                {
                    Navigator.GoBack();
                    StrongReferenceMessenger.Default
                        .Send("LoadOurEmployeesAsync");
                }
            }
            App.IsAddingUser = true;
        }

        public bool IsCanSaveEmployee =>
            !string.IsNullOrWhiteSpace(Employee.Login)
            && !string.IsNullOrWhiteSpace(Employee.PlainPassword);
    }
}