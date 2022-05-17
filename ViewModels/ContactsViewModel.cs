using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ContactsViewModel : ViewModelBase
    {
        public ContactsViewModel()
        {
            Title = "Наши контакты";
            LoadContactsAsync();
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        private async void LoadContactsAsync()
        {
            IEnumerable<Contact> users = await Ioc.Default
               .GetService<IRepository<Contact>>()
               .GetAllAsync();
            Contacts = new ObservableCollection<Contact>(users);
        }
    }
}