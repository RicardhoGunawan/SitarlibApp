using System;
using System.Windows;
using System.Windows.Data;
using SitarLib.Helpers;
using SitarLib.Services;
using SitarLib.ViewModels;
using SitarLib.Views;

namespace SitarLib
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Setup services
            var dataService = new DataService();
            var dialogService = new DialogService();
            var navigationService = new NavigationService();

            // ===== Command Definitions (shared across ViewModels) =====
            var navigateToDashboardCommand = new RelayCommand(_ => navigationService.NavigateTo("Dashboard"));
            var navigateToBookCommand = new RelayCommand(_ => navigationService.NavigateTo("Book"));
            var navigateToMemberCommand = new RelayCommand(_ => navigationService.NavigateTo("Member"));
            var navigateToBorrowingCommand = new RelayCommand(_ => navigationService.NavigateTo("Borrowing"));

            var refreshDataCommand = new RelayCommand(_ => { /* TODO: define global refresh logic */ });
            var searchBooksCommand = new RelayCommand(_ => { /* TODO: define global book search logic */ });
            var searchMembersCommand = new RelayCommand(_ => { /* TODO: define global member search logic */ });

            // Register view models with navigation service
            navigationService.Register("Dashboard", () =>
                new DashboardViewModel(dataService, dialogService, navigationService));

            navigationService.Register("Book", () =>
                new BookViewModel(
                    dataService,
                    dialogService,
                    navigationService,
                    navigateToBookCommand,
                    navigateToMemberCommand,
                    navigateToBorrowingCommand
                )
            );

            navigationService.Register("Member", () =>
                new MemberViewModel(
                    dataService,
                    dialogService,
                    navigationService,
                    navigateToBookCommand,
                    navigateToMemberCommand,
                    navigateToBorrowingCommand
                )
            );


            navigationService.Register("Borrowing", () =>
                new BorrowingViewModel(
                    dataService,
                    dialogService,
                    navigationService,
                    navigateToMemberCommand,
                    navigateToBorrowingCommand,
                    navigateToBookCommand,
                    refreshDataCommand,
                    searchBooksCommand,
                    searchMembersCommand
                )
            );

            // Pastikan app resources diinisialisasi terlebih dahulu
            if (Current.Resources == null || !Current.Resources.Contains("BooleanToStringConverter"))
            {
                // Buat BooleanToStringConverter secara manual jika belum ada
                if (!Current.Resources.Contains("BooleanToStringConverter"))
                {
                    Current.Resources.Add("BooleanToStringConverter", new BooleanToStringConverter());
                }
            }

            // Tampilkan LoginWindow terlebih dahulu
            var loginViewModel = new LoginViewModel(dataService, dialogService, navigationService);
            var loginWindow = new LoginWindow { DataContext = loginViewModel };

            // Ketika login berhasil, buka MainWindow
            loginViewModel.LoginSuccessful += (sender, args) =>
            {
                var mainViewModel = new MainViewModel(dataService, dialogService, navigationService);
                var mainWindow = new MainWindow { DataContext = mainViewModel };
                MainWindow = mainWindow;
                mainWindow.Show();

                loginWindow.Close();
            };

            loginWindow.Show();
        }
    }

    public class BooleanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool boolValue && parameter is string options)
            {
                string[] splitOptions = options.Split(';');
                if (splitOptions.Length >= 2)
                {
                    return boolValue ? splitOptions[0] : splitOptions[1];
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
