using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SitarLib.Helpers;
using SitarLib.Models;
using SitarLib.Services;

namespace SitarLib.ViewModels
{
    public class BorrowingViewModel : BaseViewModel
    {
        private ObservableCollection<Borrowing> _borrowings;
        public ObservableCollection<Borrowing> Borrowings
        {
            get => _borrowings;
            set => SetProperty(ref _borrowings, value);
        }

        private Borrowing _selectedBorrowing;
        public Borrowing SelectedBorrowing
        {
            get => _selectedBorrowing;
            set => SetProperty(ref _selectedBorrowing, value);
        }

        private Borrowing _currentBorrowing;
        public Borrowing CurrentBorrowing
        {
            get => _currentBorrowing;
            set => SetProperty(ref _currentBorrowing, value);
        }

        private ObservableCollection<Book> _availableBooks;
        public ObservableCollection<Book> AvailableBooks
        {
            get => _availableBooks;
            set => SetProperty(ref _availableBooks, value);
        }

        private ObservableCollection<Member> _activeMembers;
        public ObservableCollection<Member> ActiveMembers
        {
            get => _activeMembers;
            set => SetProperty(ref _activeMembers, value);
        }

        private Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                if (SetProperty(ref _selectedBook, value) && value != null)
                {
                    CurrentBorrowing.BookId = value.Id;
                    CurrentBorrowing.Book = value;
                }
            }
        }

        private Member _selectedMember;
        public Member SelectedMember
        {
            get => _selectedMember;
            set
            {
                if (SetProperty(ref _selectedMember, value) && value != null)
                {
                    CurrentBorrowing.MemberId = value.Id;
                    CurrentBorrowing.Member = value;
                }
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterBorrowings();
                }
            }
        }

        private string _filterStatus;
        public string FilterStatus
        {
            get => _filterStatus;
            set
            {
                if (SetProperty(ref _filterStatus, value))
                {
                    FilterBorrowings();
                }
            }
        }

        public ObservableCollection<string> StatusFilters { get; } = new ObservableCollection<string>
        {
            "All", "Borrowed", "Returned", "Overdue"
        };

        public ICommand AddNewCommand { get; }
        public ICommand BorrowCommand { get; }
        public ICommand ReturnCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand NavigateToDashboardCommand { get; }

        public BorrowingViewModel(DataService dataService, DialogService dialogService, NavigationService navigationService)
            : base(dataService, dialogService, navigationService)
        {
            Title = "Manage Borrowings - SitarLib";
            
            AddNewCommand = new RelayCommand(_ => ExecuteAddNew());
            BorrowCommand = new RelayCommand(_ => ExecuteBorrow(), _ => CanBorrow());
            ReturnCommand = new RelayCommand(_ => ExecuteReturn(), _ => CanReturn());
            CancelCommand = new RelayCommand(_ => ExecuteCancel());
            NavigateToDashboardCommand = new RelayCommand(_ => NavigationService.NavigateTo("Dashboard"));
            
            FilterStatus = "All";
            LoadBorrowings();
            LoadAvailableBooks();
            LoadActiveMembers();
            ExecuteAddNew(); // Start with a new borrowing form
        }

        private void LoadBorrowings()
        {
            IsBusy = true;
            Borrowings = new ObservableCollection<Borrowing>(DataService.GetAllBorrowings()); // ✅
            IsBusy = false;
        }

        private void LoadAvailableBooks()
        {
            AvailableBooks = new ObservableCollection<Book>(
                DataService.GetAllBooks().Where(b => b.Stock > 0)
            );
        }

        private void LoadActiveMembers()
        {
            ActiveMembers = new ObservableCollection<Member>(
                DataService.GetAllMembers().Where(m => m.IsActive)
            );
        }

        private void FilterBorrowings()
        {
            var allBorrowings = DataService.GetAllBorrowings();
    
            // Filter by status if needed
            if (FilterStatus != "All")
            {
                allBorrowings = new List<Borrowing>(
                    allBorrowings.Where(b => b.Status == FilterStatus)
                );
            }
    
            // Filter by search text if provided
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var searchTerm = SearchText.ToLower();
                allBorrowings = new List<Borrowing>(
                    allBorrowings.Where(b => 
                        b.Book.Title.ToLower().Contains(searchTerm) ||
                        b.Member.FullName.ToLower().Contains(searchTerm) ||
                        b.Member.MemberCode.ToLower().Contains(searchTerm)
                    )
                );
            }
    
            // Convert List to ObservableCollection
            Borrowings = new ObservableCollection<Borrowing>(allBorrowings); // ✅
        }


        private void ExecuteAddNew()
        {
            CurrentBorrowing = new Borrowing
            {
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14) // Default loan period: 2 weeks
            };
            SelectedBook = null;
            SelectedMember = null;
            SelectedBorrowing = null;
        }

        private bool CanBorrow()
        {
            return CurrentBorrowing != null &&
                   SelectedBook != null &&
                   SelectedMember != null;
        }

        private void ExecuteBorrow()
        {
            DataService.AddBorrowing(CurrentBorrowing);
            DialogService.ShowMessage("Book borrowed successfully!");
            
            LoadBorrowings();
            LoadAvailableBooks(); // Refresh available books as stock changed
            ExecuteAddNew(); // Reset form for a new entry
        }

        private bool CanReturn()
        {
            return SelectedBorrowing != null && 
                   SelectedBorrowing.ReturnDate == null;
        }

        private void ExecuteReturn()
        {
            if (SelectedBorrowing != null)
            {
                DataService.ReturnBook(SelectedBorrowing.Id);
                
                string message = "Book returned successfully!";
                if (SelectedBorrowing.Fine > 0)
                {
                    message += $" A fine of ${SelectedBorrowing.Fine} has been charged for overdue.";
                }
                
                DialogService.ShowMessage(message);
                LoadBorrowings();
                LoadAvailableBooks(); // Refresh available books as stock changed
            }
        }

        private void ExecuteCancel()
        {
            ExecuteAddNew();
        }
    }
}
