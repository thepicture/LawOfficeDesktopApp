using LawOfficeDesktopApp.Models.UserModels;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Xaml.Behaviors.Core;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class EmployeeLoginViewModel : ViewModelBase
    {
        public LoginUser User { get; set; }
        public EmployeeLoginViewModel()
        {
            Title = "Авторизация Персонал";
            User = new LoginUser();
        }

        private ActionCommand loginAsEmployee;

        public ICommand LoginAsEmployee
        {
            get
            {
                if (loginAsEmployee == null)
                    loginAsEmployee = new ActionCommand(PerformLoginAsEmployeeAsync);

                return loginAsEmployee;
            }
        }

        private async void PerformLoginAsEmployeeAsync()
        {
            if (await Ioc.Default
                   .GetService<IRepository<LoginUser>>()
                   .CreateAsync(User))
            {
                if (App.CurrentUser.RoleId == 1)
                {
                    App.CurrentUser = null;
                    await NotificationService
                        .NotifyErrorAsync("Вы не можете зайти как персонал, так как вы клиент");
                }
                else
                {
                    Navigator.Go<EmployeeControlPanelViewModel>();
                }
            }
        }
    }
}