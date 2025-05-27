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

        // Property untuk mengecek apakah ada buku yang dipilih
        public bool IsBookSelected => SelectedBook != null;

        // Enhanced SelectedBook property dengan logic yang diperbaiki
        private Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                if (SetProperty(ref _selectedBook, value))
                {
                    // Debug logging
                    System.Diagnostics.Debug.WriteLine($"Book selected: {value?.Title ?? "null"}");
            
                    if (value != null && CurrentBorrowing != null)
                    {
                        CurrentBorrowing.BookId = value.Id;
                        CurrentBorrowing.Book = value;
                
                        // Update search text to match selected book WITHOUT triggering filter
                        _bookSearchText = value.Title;
                        OnPropertyChanged(nameof(BookSearchText));
                    }
            
                    // Notify UI bahwa IsBookSelected berubah
                    OnPropertyChanged(nameof(IsBookSelected));
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
                    
                    // Clear search text when member is selected from dropdown
                    if (!string.IsNullOrEmpty(MemberSearchText))
                    {
                        _memberSearchText = value.FullName;
                        OnPropertyChanged(nameof(MemberSearchText));
                    }
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
        private bool _isBookSearchTextUpdating = false; // Flag to prevent recursive updates

        public string BookSearchText
        {
            get => _bookSearchText;
            set
            {
                if (SetProperty(ref _bookSearchText, value))
                {
                    if (!_isBookSearchTextUpdating)
                    {
                        // Clear selected book if user is typing something different
                        if (SelectedBook != null && value != SelectedBook.Title)
                        {
                            SelectedBook = null;
                        }
                
                        FilterBooks();
                    }
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
                    // Only filter if the text doesn't match current selected member
                    if (SelectedMember == null || value != SelectedMember.FullName)
                    {
                        FilterMembers();
                    }
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
        public ICommand NavigateToReportCommand { get; }
        
        public ICommand IncrementQuantityCommand { get; private set; }
        public ICommand DecrementQuantityCommand { get; private set; }
        
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
            ICommand searchMembersCommand, 
            ICommand navigateToReportCommand)
            : base(dataService, dialogService, navigationService)
        {
            // Injected command bindings
            NavigateToMemberCommand = navigateToMemberCommand;
            NavigateToBorrowingCommand = navigateToBorrowingCommand;
            NavigateToBookCommand = navigateToBookCommand;
            RefreshDataCommand = refreshDataCommand;
            SearchBooksCommand = searchBooksCommand;
            SearchMembersCommand = searchMembersCommand;
            NavigateToReportCommand = navigateToReportCommand;

            // Title and initial state
            Title = "Manage Borrowings - SitarLib";

            // ViewModel commands
            AddNewCommand = new RelayCommand(_ => ExecuteAddNew());
            BorrowCommand = new RelayCommand(_ => ExecuteBorrow(), _ => CanBorrow());
            ReturnCommand = new RelayCommand(_ => ExecuteReturn(), _ => CanReturn());
            CancelCommand = new RelayCommand(_ => ExecuteCancel());
            
            // Initialize quantity commands
            IncrementQuantityCommand = new RelayCommand(_ => ExecuteIncrementQuantity());
            DecrementQuantityCommand = new RelayCommand(_ => ExecuteDecrementQuantity());

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

        private void ExecuteIncrementQuantity()
        {
            if (CurrentBorrowing != null && SelectedBook != null)
            {
                if (CurrentBorrowing.Quantity < SelectedBook.Stock)
                {
                    CurrentBorrowing.Quantity++;
                    OnPropertyChanged(nameof(CurrentBorrowing));
                }
            }
        }

        private void ExecuteDecrementQuantity()
        {
            if (CurrentBorrowing != null && CurrentBorrowing.Quantity > 1)
            {
                CurrentBorrowing.Quantity--;
                OnPropertyChanged(nameof(CurrentBorrowing));
            }
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
            
            // Debug: Check if books have cover paths
            System.Diagnostics.Debug.WriteLine($"Loaded {books.Count} books");
            foreach (var book in books.Take(3)) // Log first 3 books
            {
                System.Diagnostics.Debug.WriteLine($"Book: {book.Title}, Cover: {book.DisplayCoverPath}");
            }
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
    
            if (FilterStatus != "All")
            {
                allBorrowings = new List<Borrowing>(
                    allBorrowings.Where(b => b.Status == FilterStatus)
                );
            }
    
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
    
            Borrowings = new ObservableCollection<Borrowing>(allBorrowings);
        }

        // Improved FilterBooks method
        private void FilterBooks()
        {
            if (AvailableBooks == null) return;
    
            if (string.IsNullOrWhiteSpace(BookSearchText))
            {
                FilteredBooks = new ObservableCollection<Book>(AvailableBooks);
                return;
            }

            var searchTerm = BookSearchText.ToLower();
            var filteredBooks = AvailableBooks.Where(book => 
                (book.Title != null && book.Title.ToLower().Contains(searchTerm)) || 
                (book.ISBN != null && book.ISBN.ToLower().Contains(searchTerm)) ||
                (book.Author != null && book.Author.ToLower().Contains(searchTerm))
            ).ToList();

            FilteredBooks = new ObservableCollection<Book>(filteredBooks);
        }
        // Method to handle book selection programmatically
        public void SelectBook(Book book)
        {
            if (book != null)
            {
                _isBookSearchTextUpdating = true;
                SelectedBook = book;
                BookSearchText = book.Title;
                _isBookSearchTextUpdating = false;
            }
        }

        private void FilterMembers()
        {
            if (ActiveMembers == null) return;
            
            if (string.IsNullOrWhiteSpace(MemberSearchText))
            {
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

        // Updated ExecuteAddNew method
        private void ExecuteAddNew()
        {
            CurrentBorrowing = new Borrowing
            {
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(3),
                Quantity = 1
            };
    
            // Reset selections
            _isBookSearchTextUpdating = true;
            SelectedBook = null;
            SelectedMember = null;
            SelectedBorrowing = null;
    
            // Reset search fields
            BookSearchText = string.Empty;
            MemberSearchText = string.Empty;
            _isBookSearchTextUpdating = false;
    
            // Reset filtered collections
            FilterBooks();
            FilterMembers();
        }

        private bool CanBorrow()
        {
            return CurrentBorrowing != null &&
                   SelectedBook != null &&
                   SelectedMember != null &&
                   CurrentBorrowing.Quantity > 0 &&
                   CurrentBorrowing.Quantity <= SelectedBook.Stock;
        }

        private void ExecuteBorrow()
        {
            if (CurrentBorrowing.Quantity <= 0 || 
                (SelectedBook != null && SelectedBook.Stock < CurrentBorrowing.Quantity))
            {
                DialogService.ShowMessage("Quantity tidak valid atau stok tidak cukup.");
                return;
            }

            DataService.AddBorrowing(CurrentBorrowing);
            DialogService.ShowMessage($"{CurrentBorrowing.Quantity} buku berhasil dipinjam!");
            
            LoadBorrowings();
            LoadAvailableBooks();
            ExecuteAddNew();
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
                var confirmResult = MessageBox.Show(
                    $"Apakah Anda yakin ingin mengembalikan {SelectedBorrowing.Quantity} buku ini?",
                    "Konfirmasi Pengembalian",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (confirmResult == MessageBoxResult.Yes)
                {
                    DataService.ReturnBook(SelectedBorrowing.Id);
                    string message = $"{SelectedBorrowing.Quantity} buku berhasil dikembalikan!";
                    
                    DialogService.ShowMessage(message);
                    LoadBorrowings();
                    LoadAvailableBooks();
                }
            }
            else
            {
                DialogService.ShowMessage("Silakan pilih data peminjaman untuk mengembalikan.");
            }
        }

        private void ExecuteCancel()
        {
            ExecuteAddNew();
        }
    }
}