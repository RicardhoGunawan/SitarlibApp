using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Microsoft.Win32;
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
                        AddedDate = value.AddedDate,
                        CoverImagePath = value.CoverImagePath,
                        CoverImageData = value.CoverImageData
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
        
        // Existing properties
        private bool _isImportOptionsOpen;
        private string _busyMessage;
    
        // New properties for import dropdown and loading status
        public bool IsImportOptionsOpen
        {
            get => _isImportOptionsOpen;
            set
            {
                _isImportOptionsOpen = value;
                OnPropertyChanged(nameof(IsImportOptionsOpen));
            }
        }
    
        public string BusyMessage
        {
            get => _busyMessage;
            set
            {
                _busyMessage = value;
                OnPropertyChanged(nameof(BusyMessage));
            }
        }

        public ICommand AddNewCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand NavigateToDashboardCommand { get; }
        public ICommand NavigateToBookCommand { get; }
        public ICommand NavigateToMemberCommand { get; }
        public ICommand NavigateToBorrowingCommand { get; }
        public ICommand NavigateToReportCommand { get; } // Ditambahkan
        public ICommand ImportBooksCommand { get; }
        // Commands for import functionality
        public ICommand ShowImportOptionsCommand { get; }
        public ICommand ImportCsvCommand { get; }
        public ICommand ImportXlsxCommand { get; }
        // New commands for cover image
        public ICommand SelectCoverImageCommand { get; }
        public ICommand RemoveCoverImageCommand { get; }
        



        public BookViewModel(DataService dataService, DialogService dialogService, NavigationService navigationService, ICommand navigateToBookCommand, ICommand navigateToMemberCommand, ICommand navigateToBorrowingCommand, ICommand navigateToReportCommand)
            : base(dataService, dialogService, navigationService)
        {
            NavigateToBookCommand = navigateToBookCommand;
            NavigateToMemberCommand = navigateToMemberCommand;
            NavigateToBorrowingCommand = navigateToBorrowingCommand;
            NavigateToReportCommand = navigateToReportCommand;
            Title = "Manage Books - SitarLib";
            
            AddNewCommand = new RelayCommand(_ => ExecuteAddNew());
            SaveCommand = new RelayCommand(_ => ExecuteSave(), _ => CanSave());
            DeleteCommand = new RelayCommand(_ => ExecuteDelete(), _ => SelectedBook != null);
            CancelCommand = new RelayCommand(_ => ExecuteCancel());
            NavigateToDashboardCommand = new RelayCommand(_ => NavigationService.NavigateTo("Dashboard"));
            ShowImportOptionsCommand = new RelayCommand(_ => IsImportOptionsOpen = !IsImportOptionsOpen);
            ImportCsvCommand = new RelayCommand(_ => ExecuteImportBooks("csv"));
            ImportXlsxCommand = new RelayCommand(_ => ExecuteImportBooks("xlsx"));
            
            // New commands
            SelectCoverImageCommand = new RelayCommand(_ => ExecuteSelectCoverImage());
            RemoveCoverImageCommand = new RelayCommand(_ => ExecuteRemoveCoverImage(), _ => CurrentBook != null && !string.IsNullOrEmpty(CurrentBook.CoverImagePath));
            
            BusyMessage = "Loading...";
            
            LoadBooks();
            ExecuteAddNew(); // Start with a new book form
        }
        // New method to select cover image
        private void ExecuteSelectCoverImage()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Pilih Cover Buku",
                Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files (*.*)|*.*",
                FilterIndex = 1
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string selectedPath = openFileDialog.FileName;
                    
                    // Create covers directory if it doesn't exist
                    string coversDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Covers");
                    if (!Directory.Exists(coversDirectory))
                    {
                        Directory.CreateDirectory(coversDirectory);
                    }

                    // Generate unique filename
                    string fileExtension = Path.GetExtension(selectedPath);
                    string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
                    string destinationPath = Path.Combine(coversDirectory, uniqueFileName);

                    // Copy file to covers directory
                    File.Copy(selectedPath, destinationPath, true);

                    // Update current book
                    if (CurrentBook != null)
                    {
                        CurrentBook.CoverImagePath = destinationPath;
                        OnPropertyChanged(nameof(CurrentBook));
                    }

                    DialogService.ShowMessage("Cover buku berhasil dipilih!");
                }
                catch (Exception ex)
                {
                    DialogService.ShowMessage($"Gagal memilih cover buku: {ex.Message}");
                }
            }
        }
        // New method to remove cover image
        private void ExecuteRemoveCoverImage()
        {
            if (CurrentBook != null)
            {
                bool confirm = DialogService.ShowConfirmation("Apakah Anda yakin ingin menghapus cover buku ini?");
                if (confirm)
                {
                    // Delete physical file if exists
                    if (!string.IsNullOrEmpty(CurrentBook.CoverImagePath) && File.Exists(CurrentBook.CoverImagePath))
                    {
                        try
                        {
                            File.Delete(CurrentBook.CoverImagePath);
                        }
                        catch (Exception ex)
                        {
                            // Log error but continue
                            System.Diagnostics.Debug.WriteLine($"Failed to delete cover file: {ex.Message}");
                        }
                    }

                    CurrentBook.CoverImagePath = null;
                    CurrentBook.CoverImageData = null;
                    OnPropertyChanged(nameof(CurrentBook));
                    
                    DialogService.ShowMessage("Cover buku berhasil dihapus!");
                }
            }
        }
        // Async import method
        private async void ExecuteImportBooks(string fileType)
        {
            // Tutup popup
            IsImportOptionsOpen = false;
            
            // Siapkan dialog pemilihan file
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            
            if (fileType.ToLower() == "csv")
            {
                openFileDialog.Filter = "File CSV (*.csv)|*.csv|Semua File (*.*)|*.*";
                openFileDialog.Title = "Pilih File CSV untuk Impor Buku";
            }
            else if (fileType.ToLower() == "xlsx")
            {
                openFileDialog.Filter = "File Excel (*.xlsx)|*.xlsx|Semua File (*.*)|*.*";
                openFileDialog.Title = "Pilih File Excel untuk Impor Buku";
            }
            
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Tampilkan indikator loading
                    IsBusy = true;
                    BusyMessage = $"Mengimpor buku dari {System.IO.Path.GetFileName(openFileDialog.FileName)}...";
                    
                    string filePath = openFileDialog.FileName;
                    
                    // Panggil metode import di DataService secara asynchronous
                    var result = await DataService.ImportBooksFromFileAsync(filePath, fileType);
                    
                    // Proses hasil import
                    if (result.imported >= 0)
                    {
                        if (result.imported == 0 && result.duplicates > 0)
                        {
                            // Semua buku adalah duplikat
                            DialogService.ShowMessage(
                                $"Import Gagal. Semua {result.duplicates} buku adalah duplikat dan dilewati."
                            );
                        }
                        else if (result.imported > 0 && result.duplicates > 0)
                        {
                            // Sebagian terimpor, sebagian duplikat
                            DialogService.ShowMessage(
                                $"Berhasil mengimpor {result.imported} buku. " +
                                $"{result.duplicates} buku duplikat dilewati."
                            );
                        }
                        else if (result.imported > 0 && result.duplicates == 0)
                        {
                            // Semua berhasil diimpor
                            DialogService.ShowMessage(
                                $"Berhasil mengimpor {result.imported} buku!"
                            );
                        }
                        else
                        {
                            // Semua buku di file adalah duplikat
                            DialogService.ShowMessage(
                                $"Semua {result.duplicates} buku pada file sudah ada sebelumnya dan tidak dimasukkan."
                            );

                        }
                        
                        LoadBooks(); // Segarkan daftar buku
                    }
                    else
                    {
                        DialogService.ShowMessage(
                            "Gagal mengimpor buku. Silakan periksa format file dan coba lagi."
                        );
                    }
                }
                catch (Exception ex)
                {
                    DialogService.ShowMessage(
                        $"Terjadi kesalahan saat impor buku: {ex.Message}"
                    );
                }
                finally
                {
                    // Sembunyikan indikator loading
                    IsBusy = false;
                }
            }
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
            // Jika sedang dalam mode edit, tidak perlu cek duplikat ISBN
            // karena buku tersebut sudah ada di database
            if (IsEditing)
            {
                DataService.UpdateBook(CurrentBook);
                DialogService.ShowMessage("Buku berhasil diperbarui!");
            }
            else
            {
                // Cek apakah buku dengan ISBN yang sama sudah ada
                bool isbnExists = IsIsbnExists(CurrentBook.ISBN);
                
                if (isbnExists)
                {
                    // Jika buku dengan ISBN yang sama sudah ada, tampilkan pesan error
                    DialogService.ShowMessage(
                        $"Buku dengan ISBN {CurrentBook.ISBN} sudah ada dalam database. " +
                        "Silakan gunakan ISBN yang berbeda."
                    );
                    return; // Keluar dari metode tanpa menyimpan
                }
                
                // Jika buku dengan ISBN belum ada, lanjutkan penyimpanan
                DataService.AddBook(CurrentBook);
                DialogService.ShowMessage("Buku berhasil ditambahkan!");
            }
            
            LoadBooks();
            ExecuteAddNew(); // Reset form for a new entry
        }

        // Metode untuk memeriksa apakah buku dengan ISBN tertentu sudah ada di database
        private bool IsIsbnExists(string isbn)
        {
            // Mendapatkan semua buku dari DataService
            var allBooks = DataService.GetAllBooks();
            
            // Periksa apakah ada buku dengan ISBN yang sama
            return allBooks.Any(book => book.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
        }

        private void ExecuteDelete()
        {
            if (SelectedBook != null)
            {
                bool confirm = DialogService.ShowConfirmation($"Are you sure you want to delete '{SelectedBook.Title}'?");
                if (confirm)
                {
                    DataService.DeleteBook(SelectedBook.Id);
                    DialogService.ShowMessage("Buku berhasil dihapus!");
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