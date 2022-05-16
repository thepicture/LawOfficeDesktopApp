using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LawOfficeDesktopApp.ViewModels
{
    public class OurEmployeesViewModel : ViewModelBase
    {
        public OurEmployeesViewModel()
        {
            Title = "Наши работники";
            LoadOurEmployeesAsync();
        }

        private async void LoadOurEmployeesAsync()
        {
            IEnumerable<User> users = await Ioc.Default
                .GetService<IRepository<User>>()
                .GetAllAsync();
            users = users.Where(u => u.RoleId == 2);
            OurEmployees = new ObservableCollection<User>(users);
        }

        public ObservableCollection<User> OurEmployees { get; set; }
    }
}