using System.Collections.ObjectModel;
using System.Windows.Input;
using SitarLib.Helpers;
using SitarLib.Models;
using SitarLib.Services;

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

        public ICommand NavigateToBookCommand { get; }
        public ICommand NavigateToMemberCommand { get; }
        public ICommand NavigateToBorrowingCommand { get; }
        public ICommand RefreshDataCommand { get; }

        public DashboardViewModel(DataService dataService, DialogService dialogService, NavigationService navigationService)
            : base(dataService, dialogService, navigationService)
        {
            Title = "Dashboard - SitarLib";
            
            NavigateToBookCommand = new RelayCommand(_ => NavigationService.NavigateTo("Book"));
            NavigateToMemberCommand = new RelayCommand(_ => NavigationService.NavigateTo("Member"));
            NavigateToBorrowingCommand = new RelayCommand(_ => NavigationService.NavigateTo("Borrowing"));
            RefreshDataCommand = new RelayCommand(_ => LoadDashboardData());
            
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
            
            // Get 5 most recent borrowings
            RecentBorrowings = new ObservableCollection<Borrowing>(
                allBorrowings.OrderByDescending(b => b.BorrowDate).Take(5)
            );
            
            IsBusy = false;
        }
    }
}