using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LawOfficeDesktopApp.ViewModels
{
    public class PriceViewModel : ViewModelBase
    {
        public PriceViewModel()
        {
            Title = "Прайс";
            LoadPricesAsync();
        }

        private async void LoadPricesAsync()
        {
            IEnumerable<Service> prices = await Ioc.Default
             .GetService<IRepository<Service>>()
             .GetAllAsync();
            Prices = new ObservableCollection<Service>(prices);
        }

        public ObservableCollection<Service> Prices { get; set; }
    }
}