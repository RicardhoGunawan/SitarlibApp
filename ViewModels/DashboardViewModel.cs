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

        // Properties untuk filter tanggal
        private DateTime? _startDateFilter;
        public DateTime? StartDateFilter
        {
            get => _startDateFilter;
            set => SetProperty(ref _startDateFilter, value);
        }

        private DateTime? _endDateFilter;
        public DateTime? EndDateFilter
        {
            get => _endDateFilter;
            set => SetProperty(ref _endDateFilter, value);
        }

        // Koleksi semua peminjaman untuk memudahkan filter
        private ObservableCollection<Borrowing> _allBorrowings;

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
        public ICommand NavigateToReportCommand { get; } // Ditambahkan
        public ICommand RefreshDataCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand ApplyDateFilterCommand { get; }

        public DashboardViewModel(DataService dataService, DialogService dialogService, NavigationService navigationService)
            : base(dataService, dialogService, navigationService)
        {
            Title = "Dashboard - SitarLib";

            // Initialize commands
            NavigateToBookCommand = new RelayCommand(_ => NavigationService.NavigateTo("Book"));
            NavigateToMemberCommand = new RelayCommand(_ => NavigationService.NavigateTo("Member"));
            NavigateToBorrowingCommand = new RelayCommand(_ => NavigationService.NavigateTo("Borrowing"));
            NavigateToReportCommand = new RelayCommand(_ => NavigationService.NavigateTo("Report")); // Ditambahkan
            RefreshDataCommand = new RelayCommand(_ => LoadDashboardData());
            LogoutCommand = new RelayCommand(async _ => await LogoutAsync());
            ApplyDateFilterCommand = new RelayCommand(_ => ApplyDateFilter());

            // Set default filter tanggal ke satu bulan terakhir
            EndDateFilter = DateTime.Today;
            StartDateFilter = DateTime.Today.AddMonths(-1);

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

            // Simpan semua peminjaman di list terpisah untuk memudahkan filter
            _allBorrowings = new ObservableCollection<Borrowing>(
                allBorrowings.OrderByDescending(b => b.BorrowDate)
            );

            // Tampilkan data berdasarkan filter yang aktif
            ApplyDateFilter();

            IsBusy = false;
        }

        private void ApplyDateFilter()
        {
            IsBusy = true;

            // Cek apakah tanggal valid
            if (StartDateFilter == null || EndDateFilter == null)
            {
                DialogService.ShowError("Invalid Date Range", "Start date must be before or equal to end date.");                IsBusy = false;
                return;
            }

            // Pastikan rentang tanggal valid
            if (StartDateFilter > EndDateFilter)
            {
                DialogService.ShowError("Invalid Date Range", "Start date must be before or equal to end date.");                IsBusy = false;
                return;
            }

            // Konversi nullable DateTime ke DateTime dengan nilai default jika null
            DateTime startDate = StartDateFilter ?? DateTime.MinValue;
            DateTime endDate = EndDateFilter ?? DateTime.MaxValue;

            // Sesuaikan end date untuk mencakup sampai akhir hari
            endDate = endDate.Date.AddDays(1).AddTicks(-1);

            // Filter data berdasarkan rentang tanggal
            var filteredBorrowings = _allBorrowings
                .Where(b => b.BorrowDate >= startDate && b.BorrowDate <= endDate)
                .OrderByDescending(b => b.BorrowDate)
                .Take(20) // Batasi jumlah data yang ditampilkan jika diperlukan
                .ToList();

            RecentBorrowings = new ObservableCollection<Borrowing>(filteredBorrowings);

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