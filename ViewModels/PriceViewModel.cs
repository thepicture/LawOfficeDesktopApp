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
    public class PriceViewModel : ViewModelBase
    {
        public PriceViewModel()
        {
            Title = "Прайс";
            LoadPricesAsync();
            StrongReferenceMessenger.Default
               .Register<string>(nameof(LoadPricesAsync),
                                         (_, __) => LoadPricesAsync());
            Navigator.Navigated += () =>
            {
                if (!(Navigator.CurrentTarget is AddEditServiceViewModel
                      || Navigator.CurrentTarget is PriceViewModel))
                {
                    StrongReferenceMessenger.Default
                                   .Unregister<string>(nameof(LoadPricesAsync));
                }
            };
        }

        private async void LoadPricesAsync()
        {
            IEnumerable<Service> prices = await Ioc.Default
             .GetService<IRepository<Service>>()
             .GetAllAsync();
            if (decimal.TryParse(FromPriceSearchText, out decimal fromPrice))
            {
                if (decimal.TryParse(ToPriceSearchText, out decimal toPrice))
                {
                    prices = prices.Where(p =>
                    {
                        return p.MinimumOrBasePrice >= fromPrice
                               && ((!p.MaximumPrice.HasValue
                                   && p.MinimumOrBasePrice <= toPrice)
                                   || p.MaximumPrice <= toPrice);
                    });
                }
                else
                {
                    prices = prices.Where(p => p.MinimumOrBasePrice >= fromPrice);
                }
            }
            Prices = new ObservableCollection<Service>(prices);
        }

        public ObservableCollection<Service> Prices { get; set; }

        private ActionCommand addServiceCommand;

        public ICommand AddServiceCommand
        {
            get
            {
                if (addServiceCommand == null)
                    addServiceCommand = new ActionCommand(AddService);

                return addServiceCommand;
            }
        }

        private void AddService()
        {
            Navigator.Go<AddEditServiceViewModel>();
        }

        private RelayCommand changeServiceCommand;

        public RelayCommand ChangeServiceCommand
        {
            get
            {
                if (changeServiceCommand == null)
                    changeServiceCommand = new RelayCommand(ChangeService,
                                                            () => SelectedService != null);

                return changeServiceCommand;
            }
        }

        private void ChangeService()
        {
            Navigator.Go<AddEditServiceViewModel, Service>(SelectedService);
        }

        private RelayCommand deleteServiceCommand;

        public RelayCommand DeleteServiceCommand
        {
            get
            {
                if (deleteServiceCommand == null)
                    deleteServiceCommand = new RelayCommand(DeleteServiceAsync,
                                                            () => SelectedService != null);

                return deleteServiceCommand;
            }
        }

        private async void DeleteServiceAsync()
        {
            if (await NotificationService.AskAsync("Удалить данные об услуге?"))
            {
                if (await Ioc.Default
                        .GetService<IRepository<Service>>()
                        .DeleteAsync(SelectedService.Id))
                {
                    LoadPricesAsync();
                }
            }
        }

        private Service selectedService;

        public Service SelectedService
        {
            get => selectedService;
            set
            {
                SetProperty(ref selectedService, value);
                ChangeServiceCommand?.NotifyCanExecuteChanged();
                DeleteServiceCommand?.NotifyCanExecuteChanged();
            }
        }

        private string fromPriceSearchText;

        public string FromPriceSearchText
        {
            get => fromPriceSearchText;
            set
            {
                if (SetProperty(ref fromPriceSearchText, value))
                {
                    LoadPricesAsync();
                }
            }
        }

        private string toPriceSearchText;

        public string ToPriceSearchText
        {
            get => toPriceSearchText;
            set
            {
                if (SetProperty(ref toPriceSearchText, value))
                {
                    LoadPricesAsync();
                }
            }
        }
    }
}