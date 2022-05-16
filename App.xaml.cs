﻿using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Models.UserModels;
using LawOfficeDesktopApp.Services;
using LawOfficeDesktopApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Windows;

namespace LawOfficeDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User CurrentUser { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            CheckDatabaseConnection();
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<INavigator<ViewModelBase>, Navigator>()
                    .AddSingleton<INotificationService, NotificationService>()
                    .AddSingleton<IRepository<CustomerRegistrationUser>, CustomerRegistrationUserRepository>()
                    .AddSingleton<IRepository<CustomerLoginUser>, CustomerLoginUserRepository>()
                    .AddSingleton<IRepository<User>, UserRepository>()
                    .AddSingleton<IRepository<Service>, ServiceRepository>()
                    .AddTransient<NavigatorViewModel>()
                    .BuildServiceProvider());
            Ioc.Default
                .GetService<INavigator<ViewModelBase>>()
                .Go<CustomerRegistrationViewModel>();
        }

        private static void CheckDatabaseConnection()
        {
            try
            {
                using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities())
                {
                    entities.Database.Connection.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString());
            }
        }
    }
}
