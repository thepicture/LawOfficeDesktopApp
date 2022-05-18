using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
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
            IEnumerable<Contact> contacts = await Ioc.Default
               .GetService<IRepository<Contact>>()
               .GetAllAsync();
            if (!string.IsNullOrWhiteSpace(AddressSearchText))
            {
                contacts = contacts.Where(c =>
                {
                    return c.Address.IndexOf(AddressSearchText,
                                             StringComparison.OrdinalIgnoreCase) != -1;
                });
            }
            if (!string.IsNullOrWhiteSpace(phoneSearchText))
            {
                contacts = contacts.Where(c =>
                {
                    string rawPhoneNumber = string.Join("",
                                                        c.PhoneNumber.Where(p =>
                                                        {
                                                            return char.IsDigit(p);
                                                        }));
                    return rawPhoneNumber.Contains(PhoneSearchText);
                });
            }
            Contacts = new ObservableCollection<Contact>(contacts);
        }

        private string addressSearchText;

        public string AddressSearchText
        {
            get => addressSearchText; set
            {
                if (SetProperty(ref addressSearchText, value))
                {
                    LoadContactsAsync();
                }
            }
        }

        private string phoneSearchText;

        public string PhoneSearchText
        {
            get => phoneSearchText; set
            {
                if (SetProperty(ref phoneSearchText, value))
                {
                    LoadContactsAsync();
                }
            }
        }
    }
}