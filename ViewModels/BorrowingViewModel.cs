using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
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

        private ObservableCollection<Book> _filteredBooks;
        public ObservableCollection<Book> FilteredBooks
        {
            get => _filteredBooks;
            set => SetProperty(ref _filteredBooks, value);
        }

        private ObservableCollection<Member> _activeMembers;
        public ObservableCollection<Member> ActiveMembers
        {
            get => _activeMembers;
            set => SetProperty(ref _activeMembers, value);
        }

        private ObservableCollection<Member> _filteredMembers;
        public ObservableCollection<Member> FilteredMembers
        {
            get => _filteredMembers;
            set => SetProperty(ref _filteredMembers, value);
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

        private string _bookSearchText = string.Empty;
        public string BookSearchText
        {
            get => _bookSearchText;
            set
            {
                if (SetProperty(ref _bookSearchText, value))
                {
                    // Filter as the user types in the combo box
                    FilterBooks();
                }
            }
        }

        private string _memberSearchText = string.Empty;
        public string MemberSearchText
        {
            get => _memberSearchText;
            set
            {
                if (SetProperty(ref _memberSearchText, value))
                {
                    // Filter as the user types in the combo box
                    FilterMembers();
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
        public ICommand NavigateToBookCommand { get; }
        public ICommand NavigateToMemberCommand { get; }
        public ICommand NavigateToBorrowingCommand { get; }
        
        public ICommand RefreshDataCommand { get; }
        public ICommand SearchBooksCommand { get; }
        public ICommand SearchMembersCommand { get; }

        public BorrowingViewModel(
            DataService dataService,
            DialogService dialogService,
            NavigationService navigationService,
            ICommand navigateToMemberCommand,
            ICommand navigateToBorrowingCommand,
            ICommand navigateToBookCommand,
            ICommand refreshDataCommand,
            ICommand searchBooksCommand,
            ICommand searchMembersCommand)
            : base(dataService, dialogService, navigationService)
        {
            // Injected command bindings
            NavigateToMemberCommand = navigateToMemberCommand;
            NavigateToBorrowingCommand = navigateToBorrowingCommand;
            NavigateToBookCommand = navigateToBookCommand;
            RefreshDataCommand = refreshDataCommand;
            SearchBooksCommand = searchBooksCommand;
            SearchMembersCommand = searchMembersCommand;

            // Title and initial state
            Title = "Manage Borrowings - SitarLib";

            // ViewModel commands
            AddNewCommand = new RelayCommand(_ => ExecuteAddNew());
            BorrowCommand = new RelayCommand(_ => ExecuteBorrow(), _ => CanBorrow());
            ReturnCommand = new RelayCommand(_ => ExecuteReturn(), _ => CanReturn());
            CancelCommand = new RelayCommand(_ => ExecuteCancel());

            // Navigation command to dashboard
            NavigateToDashboardCommand = new RelayCommand(_ => NavigationService.NavigateTo("Dashboard"));

            // Initialize search/filter state
            BookSearchText = string.Empty;
            MemberSearchText = string.Empty;
            FilterStatus = "All";

            // Load initial data
            LoadBorrowings();
            LoadAvailableBooks();
            LoadActiveMembers();

            // Prepare form
            ExecuteAddNew();
        }


        private void LoadBorrowings()
        {
            IsBusy = true;
            Borrowings = new ObservableCollection<Borrowing>(DataService.GetAllBorrowings());
            IsBusy = false;
        }

        private void LoadAvailableBooks()
        {
            var books = DataService.GetAllBooks().Where(b => b.Stock > 0).ToList();
            AvailableBooks = new ObservableCollection<Book>(books);
            FilteredBooks = new ObservableCollection<Book>(books);
        }

        private void LoadActiveMembers()
        {
            var members = DataService.GetAllMembers().Where(m => m.IsActive).ToList();
            ActiveMembers = new ObservableCollection<Member>(members);
            FilteredMembers = new ObservableCollection<Member>(members);
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
            Borrowings = new ObservableCollection<Borrowing>(allBorrowings);
        }

        private void FilterBooks()
        {
            if (AvailableBooks == null) return;
            
            if (string.IsNullOrWhiteSpace(BookSearchText))
            {
                // If no search text, show all available books
                FilteredBooks = new ObservableCollection<Book>(AvailableBooks);
                return;
            }

            var searchTerm = BookSearchText.ToLower();
            var filteredBooks = AvailableBooks.Where(book => 
                (book.Title != null && book.Title.ToLower().Contains(searchTerm)) || 
                (book.ISBN != null && book.ISBN.ToLower().Contains(searchTerm))
            ).ToList();

            FilteredBooks = new ObservableCollection<Book>(filteredBooks);
        }

        private void FilterMembers()
        {
            if (ActiveMembers == null) return;
            
            if (string.IsNullOrWhiteSpace(MemberSearchText))
            {
                // If no search text, show all active members
                FilteredMembers = new ObservableCollection<Member>(ActiveMembers);
                return;
            }

            var searchTerm = MemberSearchText.ToLower();
            var filteredMembers = ActiveMembers.Where(member => 
                (member.FullName != null && member.FullName.ToLower().Contains(searchTerm)) || 
                (member.MemberCode != null && member.MemberCode.ToLower().Contains(searchTerm))
            ).ToList();

            FilteredMembers = new ObservableCollection<Member>(filteredMembers);
        }

        private void ExecuteAddNew()
        {
            CurrentBorrowing = new Borrowing
            {
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7) // Default loan period: 1 week
            };
            SelectedBook = null;
            SelectedMember = null;
            SelectedBorrowing = null;
            
            // Reset search fields
            BookSearchText = string.Empty;
            MemberSearchText = string.Empty;
            
            // Reset filtered collections
            FilterBooks();
            FilterMembers();
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
                // Add confirmation dialog first
                var confirmResult = MessageBox.Show(
                    "Apakah Anda yakin ingin mengembalikan buku ini?",
                    "Konfirmasi Pengembalian",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (confirmResult == MessageBoxResult.Yes)
                {
                    DataService.ReturnBook(SelectedBorrowing.Id);

                    string message = "Book returned successfully!";
                    // If you want to display fines later, reactivate this section
                    // if (SelectedBorrowing.Fine > 0)
                    // {
                    //     message += $" A fine of ${SelectedBorrowing.Fine} has been charged for overdue.";
                    // }

                    DialogService.ShowMessage(message);
                    LoadBorrowings();
                    LoadAvailableBooks(); // Refresh book stock
                }
            }
            else
            {
                // If nothing is selected, notify the user
                DialogService.ShowMessage("Please select a borrowing record to return.");
            }
        }

        private void ExecuteCancel()
        {
            ExecuteAddNew();
        }
    }
}