using Microsoft.Xaml.Behaviors.Core;
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

        private ActionCommand goToPriceViewModel;

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

        private ActionCommand goToAddRequestViewModel;

        public ICommand GoToAddRequestViewModel
        {
            get
            {
                if (goToAddRequestViewModel == null)
                    goToAddRequestViewModel = new ActionCommand(PerformGoToAddRequestViewModel);

                return goToAddRequestViewModel;
            }
        }

        private void PerformGoToAddRequestViewModel()
        {
            Navigator.Go<AddRequestViewModel>();
        }

        private ActionCommand goToMyAccountViewModel;

        public ICommand GoToMyAccountViewModel
        {
            get
            {
                if (goToMyAccountViewModel == null)
                    goToMyAccountViewModel = new ActionCommand(PerformGoToMyAccountViewModel);

                return goToMyAccountViewModel;
            }
        }

        private void PerformGoToMyAccountViewModel()
        {
            Navigator.Go<MyAccountViewModel>();
        }
    }
}