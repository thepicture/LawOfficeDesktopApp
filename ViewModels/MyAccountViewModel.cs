using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Xaml.Behaviors.Core;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class MyAccountViewModel : ViewModelBase
    {
        public User User { get; set; }
        public MyAccountViewModel()
        {
            Title = "Личный кабинет";
            LoadUserAsync();
        }

        private async void LoadUserAsync()
        {
            User = await Ioc.Default
                .GetService<IRepository<User>>()
                .GetSingleAsync(App.CurrentUser.Id);
        }

        private ActionCommand changeMyAccount;

        public ICommand ChangeMyAccount
        {
            get
            {
                if (changeMyAccount == null)
                    changeMyAccount = new ActionCommand(PerformChangeMyAccount);

                return changeMyAccount;
            }
        }

        private async void PerformChangeMyAccount()
        {
            await Ioc.Default
                .GetService<IRepository<User>>()
                .UpdateAsync(User);
        }
    }
}