using System;
using System.Windows;
using System.Windows.Data;
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
            
            // Register view models with navigation service
            navigationService.Register("Dashboard", () => new DashboardViewModel(dataService, dialogService, navigationService));
            navigationService.Register("Book", () => new BookViewModel(dataService, dialogService, navigationService));
            navigationService.Register("Member", () => new MemberViewModel(dataService, dialogService, navigationService));
            navigationService.Register("Borrowing", () => new BorrowingViewModel(dataService, dialogService, navigationService));
            
            // Pastikan app resources diinisialisasi terlebih dahulu
            if (Current.Resources == null || !Current.Resources.Contains("BooleanToStringConverter"))
            {
                // Buat BooleanToStringConverter secara manual jika belum ada
                if (!Current.Resources.Contains("BooleanToStringConverter"))
                {
                    Current.Resources.Add("BooleanToStringConverter", new BooleanToStringConverter());
                }
            }
            
            // Buat dan tampilkan LoginWindow terlebih dahulu
            var loginViewModel = new LoginViewModel(dataService, dialogService, navigationService);
            var loginWindow = new LoginWindow { DataContext = loginViewModel };
            
            // Ketika login berhasil, buka MainWindow
            loginViewModel.LoginSuccessful += (sender, args) =>
            {
                // Buat dan tampilkan MainWindow dengan DataContext-nya
                var mainViewModel = new MainViewModel(dataService, dialogService, navigationService);
                var mainWindow = new MainWindow { DataContext = mainViewModel };
                MainWindow = mainWindow;
                mainWindow.Show();
                
                // Tutup window login
                loginWindow.Close();
            };
            
            // Tampilkan window login
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