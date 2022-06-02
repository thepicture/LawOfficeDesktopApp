using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Xaml.Behaviors.Core;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class EnterEmployeePasswordViewModel : ViewModelBase
    {
        public IGuard<string> PasswordGuard =>
            Ioc.Default.GetService<IGuard<string>>();
        public EnterEmployeePasswordViewModel()
        {
            Title = "Введите пароль для доступа";
        }

        private string password;

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private ActionCommand goToEmployeeRegistrationViewModel;

        public ICommand GoToEmployeeRegistrationViewModel
        {
            get
            {
                if (goToEmployeeRegistrationViewModel == null)
                    goToEmployeeRegistrationViewModel =
                        new ActionCommand(PerformGoToEmployeeRegistrationViewModel);

                return goToEmployeeRegistrationViewModel;
            }
        }

        private void PerformGoToEmployeeRegistrationViewModel()
        {
            if (PasswordGuard.Verify(Password))
            {
                Navigator.Go<EmployeeRegistrationViewModel>();
            }
            else
            {
                NotificationService.NotifyErrorAsync("Неверный пароль доступа. "
                                                     + "Проверьте введённые данные "
                                                     + "и попробуйте ещё раз");
            }
        }
    }
}