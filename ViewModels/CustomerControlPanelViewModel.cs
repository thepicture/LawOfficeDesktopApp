using Microsoft.Xaml.Behaviors.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    public class CustomerControlPanelViewModel : ViewModelBase
    {
        public CustomerControlPanelViewModel()
        {
            Title = "Наши услуги";
        }

        private ActionCommand goToOurEmployeesViewModel;

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