using Microsoft.Xaml.Behaviors.Core;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class EnterEmployeePasswordViewModel : ViewModelBase
    {
        public EnterEmployeePasswordViewModel()
        {
            Title = "Введите пароль для доступа";
        }

        private string password;

        public string Password { get => password; set => SetProperty(ref password, value); }

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
        }
    }
}