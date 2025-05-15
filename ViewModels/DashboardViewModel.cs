using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SitarLib.Helpers;
using SitarLib.Models;
using SitarLib.Services;
using SitarLib.Views;

namespace SitarLib.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private int _totalBooks;
        public int TotalBooks
        {
            get => _totalBooks;
            set => SetProperty(ref _totalBooks, value);
        }

        private int _totalMembers;
        public int TotalMembers
        {
            get => _totalMembers;
            set => SetProperty(ref _totalMembers, value);
        }

        private int _activeBorrowings;
        public int ActiveBorrowings
        {
            get => _activeBorrowings;
            set => SetProperty(ref _activeBorrowings, value);
        }

        private int _overdueBorrowings;
        public int OverdueBorrowings
        {
            get => _overdueBorrowings;
            set => SetProperty(ref _overdueBorrowings, value);
        }

        private ObservableCollection<Borrowing> _recentBorrowings;
        public ObservableCollection<Borrowing> RecentBorrowings
        {
            get => _recentBorrowings;
            set => SetProperty(ref _recentBorrowings, value);
        }

        // Properti untuk menandakan loading / animasi aktif
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public ICommand NavigateToBookCommand { get; }
        public ICommand NavigateToMemberCommand { get; }
        public ICommand NavigateToBorrowingCommand { get; }
        public ICommand RefreshDataCommand { get; }
        public ICommand LogoutCommand { get; }

        public DashboardViewModel(DataService dataService, DialogService dialogService, NavigationService navigationService)
            : base(dataService, dialogService, navigationService)
        {
            Title = "Dashboard - SitarLib";

            NavigateToBookCommand = new RelayCommand(_ => NavigationService.NavigateTo("Book"));
            NavigateToMemberCommand = new RelayCommand(_ => NavigationService.NavigateTo("Member"));
            NavigateToBorrowingCommand = new RelayCommand(_ => NavigationService.NavigateTo("Borrowing"));
            RefreshDataCommand = new RelayCommand(_ => LoadDashboardData());
            LogoutCommand = new RelayCommand(async _ => await LogoutAsync());

            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            IsBusy = true;

            var allBooks = DataService.GetAllBooks();
            var allMembers = DataService.GetAllMembers();
            var allBorrowings = DataService.GetAllBorrowings();
            var overdueBorrowings = DataService.GetOverdueBorrowings();

            TotalBooks = allBooks.Count;
            TotalMembers = allMembers.Count;
            ActiveBorrowings = allBorrowings.Count(b => b.ReturnDate == null);
            OverdueBorrowings = overdueBorrowings.Count;

            RecentBorrowings = new ObservableCollection<Borrowing>(
                allBorrowings.OrderByDescending(b => b.BorrowDate).Take(5)
            );

            IsBusy = false;
        }

        // Versi async untuk logout dengan loading
        private async Task LogoutAsync()
        {
            var confirmMessage = "Are you sure you want to log out from the application?";
            var result = DialogService.ShowConfirmation("Confirm Logout", confirmMessage);

            if (result)
            {
                try
                {
                    IsBusy = true; // mulai loading

                    // Tambahkan delay simulasi supaya animasi loading terlihat
                    await Task.Delay(800);

                    DataService.ClearCurrentUser();

                    var loginViewModel = new LoginViewModel(DataService, DialogService, NavigationService);
                    var loginWindow = new LoginWindow { DataContext = loginViewModel };

                    loginViewModel.LoginSuccessful += (sender, args) =>
                    {
                        var mainViewModel = new MainViewModel(DataService, DialogService, NavigationService);
                        var mainWindow = new MainWindow { DataContext = mainViewModel };
                        Application.Current.MainWindow = mainWindow;
                        mainWindow.Show();

                        loginWindow.Close();
                    };

                    Application.Current.MainWindow = loginWindow;
                    loginWindow.Show();

                    // Tutup window selain loginWindow
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window != loginWindow)
                        {
                            window.Close();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    DialogService.ShowError("Logout Error", $"An error occurred while logging out: {ex.Message}");
                }
                finally
                {
                    IsBusy = false; // selesai loading
                }
            }
        }
    }
}
