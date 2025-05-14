using System.Collections.ObjectModel;
using System.Windows.Input;
using SitarLib.Helpers;
using SitarLib.Models;
using SitarLib.Services;

namespace SitarLib.ViewModels
{
    public class BookViewModel : BaseViewModel
    {
        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get => _books;
            set => SetProperty(ref _books, value);
        }

        private Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                if (SetProperty(ref _selectedBook, value) && value != null)
                {
                    // Create a clone for editing to prevent direct modification
                    CurrentBook = new Book
                    {
                        Id = value.Id,
                        ISBN = value.ISBN,
                        Title = value.Title,
                        Author = value.Author,
                        Publisher = value.Publisher,
                        PublicationYear = value.PublicationYear,
                        Category = value.Category,
                        Stock = value.Stock,
                        Description = value.Description,
                        AddedDate = value.AddedDate
                    };
                    IsEditing = true;
                }
            }
        }

        private Book _currentBook;
        public Book CurrentBook
        {
            get => _currentBook;
            set => SetProperty(ref _currentBook, value);
        }

        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterBooks();
                }
            }
        }

        public ICommand AddNewCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand NavigateToDashboardCommand { get; }

        public BookViewModel(DataService dataService, DialogService dialogService, NavigationService navigationService)
            : base(dataService, dialogService, navigationService)
        {
            Title = "Manage Books - SitarLib";
            
            AddNewCommand = new RelayCommand(_ => ExecuteAddNew());
            SaveCommand = new RelayCommand(_ => ExecuteSave(), _ => CanSave());
            DeleteCommand = new RelayCommand(_ => ExecuteDelete(), _ => SelectedBook != null);
            CancelCommand = new RelayCommand(_ => ExecuteCancel());
            NavigateToDashboardCommand = new RelayCommand(_ => NavigationService.NavigateTo("Dashboard"));
            
            LoadBooks();
            ExecuteAddNew(); // Start with a new book form
        }

        private void LoadBooks()
        {
            IsBusy = true;
            Books = new ObservableCollection<Book>(DataService.GetAllBooks());
            IsBusy = false;
        }

        private void FilterBooks()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                LoadBooks();
                return;
            }

            var searchTerm = SearchText.ToLower();
            var filteredBooks = DataService.GetAllBooks().Where(b =>
                b.Title.ToLower().Contains(searchTerm) ||
                b.Author.ToLower().Contains(searchTerm) ||
                b.ISBN.ToLower().Contains(searchTerm) ||
                b.Category.ToLower().Contains(searchTerm)
            );
            
            Books = new ObservableCollection<Book>(filteredBooks);
        }

        private void ExecuteAddNew()
        {
            CurrentBook = new Book
            {
                PublicationYear = DateTime.Now.Year,
                Stock = 1
            };
            IsEditing = false;
            SelectedBook = null;
        }

        private bool CanSave()
        {
            return CurrentBook != null &&
                   !string.IsNullOrWhiteSpace(CurrentBook.Title) &&
                   !string.IsNullOrWhiteSpace(CurrentBook.Author) &&
                   !string.IsNullOrWhiteSpace(CurrentBook.ISBN);
        }

        private void ExecuteSave()
        {
            if (IsEditing)
            {
                DataService.UpdateBook(CurrentBook);
                DialogService.ShowMessage("Book updated successfully!");
            }
            else
            {
                DataService.AddBook(CurrentBook);
                DialogService.ShowMessage("Book added successfully!");
            }
            
            LoadBooks();
            ExecuteAddNew(); // Reset form for a new entry
        }

        private void ExecuteDelete()
        {
            if (SelectedBook != null)
            {
                bool confirm = DialogService.ShowConfirmation($"Are you sure you want to delete '{SelectedBook.Title}'?");
                if (confirm)
                {
                    DataService.DeleteBook(SelectedBook.Id);
                    DialogService.ShowMessage("Book deleted successfully!");
                    LoadBooks();
                    ExecuteAddNew();
                }
            }
        }

        private void ExecuteCancel()
        {
            ExecuteAddNew();
        }
    }
}