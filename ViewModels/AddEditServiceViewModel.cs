using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Messaging;
using Microsoft.Xaml.Behaviors.Core;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    public class AddEditServiceViewModel : ViewModelBase
    {
        public Service Service { get; set; }
        public AddEditServiceViewModel()
        {
            Title = "Добавление услуги";
            Service = new Service();
        }

        public AddEditServiceViewModel(Service inputService)
        {
            Title = "Редактирование услуги";
            Service = inputService;
            inputService.IsHasMaximumPrice = inputService.MaximumPrice.HasValue;
        }

        private ActionCommand saveChanges;

        public ICommand SaveChanges
        {
            get
            {
                if (saveChanges == null)
                    saveChanges = new ActionCommand(PerformSaveChangesAsync);

                return saveChanges;
            }
        }

        private async void PerformSaveChangesAsync()
        {
            if (await Ioc.Default
                    .GetService<IRepository<Service>>()
                    .CreateAsync(Service))
            {
                Navigator.GoBack();
                StrongReferenceMessenger.Default
                    .Send("LoadPricesAsync");
            }
        }
    }
}