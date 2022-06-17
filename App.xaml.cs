using LawOfficeDesktopApp.Models.Entities;
using LawOfficeDesktopApp.Models.UserModels;
using LawOfficeDesktopApp.Services;
using LawOfficeDesktopApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace LawOfficeDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User CurrentUser { get; set; }
        public static bool IsAddingUser { get; set; }
        public static string Connection { get; internal set; }

        public static readonly string DataSourcesPath = "./../../DataSources.txt";

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ShutDownIfDataSourcesAreInvalid();

            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<INavigator<ViewModelBase>, Navigator>()
                    .AddSingleton<INotificationService, NotificationService>()
                    .AddSingleton<IGuard<string>, Guard>()
                    .AddSingleton<IRepository<RegistrationUser>, RegistrationUserRepository>()
                    .AddSingleton<IRepository<LoginUser>, LoginUserRepository>()
                    .AddSingleton<IRepository<User>, UserRepository>()
                    .AddSingleton<IRepository<Service>, ServiceRepository>()
                    .AddSingleton<IRepository<CustomerRequest>, CustomerRequestRepository>()
                    .AddSingleton<IRepository<Contact>, ContactsRepository>()
                    .AddSingleton<IRepository<UserRole>, RoleRepository>()
                    .AddTransient<NavigatorViewModel>()
                    .BuildServiceProvider());
            Ioc.Default
                .GetService<INavigator<ViewModelBase>>()
                .Go<CustomerRegistrationViewModel>();
        }

        private void ShutDownIfDataSourcesAreInvalid()
        {
            if (!IsNoneOfDataSourcesWorks())
            {
                return;
            }
            else
            {
                MessageBox.Show("Работа программы невозможна. "
                                + "Все строки "
                                + "подключения недоступны "
                                + "для связи с хранилищем данных");
                Shutdown();
            }
        }

        private static bool IsNoneOfDataSourcesWorks()
        {
            // File must contain line-by-line data sources. Looping from the last to the first.
            Stack<string> sources = new Stack<string>(
                File.ReadAllLines(DataSourcesPath));
            while (sources.Count > 0)
            {
                string connection = default;
                try
                {
                    connection = $@"metadata=res://*/Models.Entities.BaseModel.csdl|res://*/Models.Entities.BaseModel.ssdl|res://*/Models.Entities.BaseModel.msl;
                                    provider=System.Data.SqlClient;
                                    provider connection string="";
                                    data source={sources.Pop()};
                                    initial catalog=LawOfficeBase;
                                    integrated security=True;
                                    MultipleActiveResultSets=True;
                                    App=EntityFramework""";
                    using (LawOfficeBaseEntities entities = new LawOfficeBaseEntities(connection))
                    {
                        entities.Database.Connection.Open();
                    }
                    Connection = connection;
                    return false;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Connection string "
                                            + connection
                                            + " is not working: "
                                            + ex.ToString());
                    continue;
                }
            }
            return true;
        }
    }
}
