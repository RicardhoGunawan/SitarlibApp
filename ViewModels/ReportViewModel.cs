using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;
using SitarLib.Helpers;
using SitarLib.Models;
using SitarLib.Services;

namespace SitarLib.ViewModels
{
    public class ReportViewModel : BaseViewModel
    {
        private readonly DataService _dataService;
        private DateTime _startDate = DateTime.Now.AddMonths(-1);
        private DateTime _endDate = DateTime.Now;

        public ReportViewModel(DataService dataService, DialogService dialogService, NavigationService navigationService) 
            : base(dataService, dialogService, navigationService)
        {
            _dataService = dataService;
            Title = "Reports - SitarLib";

            // Initialize commands
            ApplyFilterCommand = new RelayCommand(_ => LoadReportData());
            NavigateToDashboardCommand = new RelayCommand(_ => navigationService.NavigateTo("Dashboard"));
            NavigateToBookCommand = new RelayCommand(_ => navigationService.NavigateTo("Book"));
            NavigateToMemberCommand = new RelayCommand(_ => navigationService.NavigateTo("Member"));
            NavigateToBorrowingCommand = new RelayCommand(_ => navigationService.NavigateTo("Borrowing"));

            // Initialize chart series
            InitializeChartSeries();

            // Load initial data
            LoadReportData();
        }

        public ICommand ApplyFilterCommand { get; }
        public ICommand NavigateToDashboardCommand { get; }
        public ICommand NavigateToBookCommand { get; }
        public ICommand NavigateToMemberCommand { get; }
        public ICommand NavigateToBorrowingCommand { get; }

        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        #region Chart Properties

        // Borrowings by Month Chart
        public SeriesCollection BorrowingsByMonthSeries { get; set; }
        public string[] BorrowingsByMonthLabels { get; set; }
        public Func<double, string> BorrowingsByMonthFormatter { get; set; }

        // Book Categories Chart
        public SeriesCollection BookCategoriesSeries { get; set; }

        // Borrowing Status Chart
        public SeriesCollection BorrowingStatusSeries { get; set; }

        // Daily Activity Chart
        public SeriesCollection DailyActivitySeries { get; set; }
        public string[] DailyActivityLabels { get; set; }
        public Func<double, string> DailyActivityFormatter { get; set; }

        #endregion

        #region Data Properties

        public ObservableCollection<Book> MostBorrowedBooks { get; set; } = new ObservableCollection<Book>();
        public ObservableCollection<Member> MostActiveMembers { get; set; } = new ObservableCollection<Member>();
        public ObservableCollection<Borrowing> RecentBorrowings { get; set; } = new ObservableCollection<Borrowing>();

        #endregion

        private void InitializeChartSeries()
        {
            // Borrowings by Month
            BorrowingsByMonthSeries = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Borrowings",
                    Values = new ChartValues<int>(),
                    Fill = System.Windows.Media.Brushes.DodgerBlue
                }
            };
            BorrowingsByMonthFormatter = value => value.ToString("N0");

            // Book Categories - Inisialisasi dengan koleksi kosong
            BookCategoriesSeries = new SeriesCollection();
    
            // Borrowing Status - Inisialisasi dengan koleksi kosong
            BorrowingStatusSeries = new SeriesCollection();
    
            // Daily Activity
            DailyActivitySeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Borrowings",
                    Values = new ChartValues<int>(),
                    Stroke = System.Windows.Media.Brushes.DodgerBlue,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 10
                },
                new LineSeries
                {
                    Title = "Returns",
                    Values = new ChartValues<int>(),
                    Stroke = System.Windows.Media.Brushes.LimeGreen,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 10
                }
            };
            DailyActivityFormatter = value => value.ToString("N0");
        }

        private void LoadReportData()
        {
            IsBusy = true;

            try
            {
                // Load all necessary data
                var borrowings = _dataService.GetAllBorrowings()
                    .Where(b => b.BorrowDate >= StartDate && b.BorrowDate <= EndDate)
                    .ToList();

                var books = _dataService.GetAllBooks();
                var members = _dataService.GetAllMembers();

                // Update Borrowings by Month chart
                UpdateBorrowingsByMonthChart(borrowings);

                // Update Book Categories chart
                UpdateBookCategoriesChart(books);

                // Update Borrowing Status chart
                UpdateBorrowingStatusChart(borrowings);

                // Update Daily Activity chart
                UpdateDailyActivityChart(borrowings);

                // Update Most Borrowed Books
                UpdateMostBorrowedBooks(borrowings, books);

                // Update Most Active Members
                UpdateMostActiveMembers(borrowings, members);

                // Update Recent Borrowings
                UpdateRecentBorrowings(borrowings);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void UpdateBorrowingsByMonthChart(List<Borrowing> borrowings)
        {
            var borrowingsByMonth = borrowings
                .GroupBy(b => new { b.BorrowDate.Year, b.BorrowDate.Month })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Select(g => new
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                    Count = g.Count()
                })
                .ToList();

            BorrowingsByMonthSeries[0].Values.Clear();
            foreach (var item in borrowingsByMonth)
            {
                BorrowingsByMonthSeries[0].Values.Add(item.Count);
            }

            BorrowingsByMonthLabels = borrowingsByMonth
                .Select(b => b.Month.ToString("MMM yyyy"))
                .ToArray();
        }

        private void UpdateBookCategoriesChart(List<Book> books)
        {
            try
            {
                var categories = books
                    .GroupBy(b => string.IsNullOrEmpty(b.Category) ? "Uncategorized" : b.Category)
                    .Select(g => new { Category = g.Key, Count = g.Count() })
                    .OrderByDescending(g => g.Count)
                    .ToList();

                // Pastikan BookCategoriesSeries sudah diinisialisasi
                if (BookCategoriesSeries == null)
                {
                    BookCategoriesSeries = new SeriesCollection();
                }
                else
                {
                    BookCategoriesSeries.Clear();
                }

                foreach (var category in categories)
                {
                    BookCategoriesSeries.Add(new PieSeries
                    {
                        Title = category.Category,
                        Values = new ChartValues<double> { category.Count },
                        DataLabels = true,
                        LabelPoint = point => $"{point.Y} ({point.Participation:P1})"
                    });
                }
            }
            catch (Exception ex)
            {
                // Log error atau tampilkan pesan ke user
                Console.WriteLine($"Error updating book categories chart: {ex.Message}");
            }
        }

        private void UpdateBorrowingStatusChart(List<Borrowing> borrowings)
        {
            var statusGroups = borrowings
                .GroupBy(b => b.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToList();

            BorrowingStatusSeries.Clear();
            foreach (var status in statusGroups)
            {
                BorrowingStatusSeries.Add(new PieSeries
                {
                    Title = status.Status,
                    Values = new ChartValues<double> { status.Count },
                    DataLabels = true,
                    LabelPoint = point => $"{point.Y} ({point.Participation:P1})"
                });
            }
        }

        private void UpdateDailyActivityChart(List<Borrowing> borrowings)
        {
            var dailyActivity = borrowings
                .GroupBy(b => b.BorrowDate.Date)
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Date = g.Key,
                    BorrowCount = g.Count(),
                    ReturnCount = g.Count(b => b.ReturnDate.HasValue && b.ReturnDate.Value.Date == g.Key)
                })
                .ToList();

            DailyActivitySeries[0].Values.Clear();
            DailyActivitySeries[1].Values.Clear();
            
            foreach (var day in dailyActivity)
            {
                DailyActivitySeries[0].Values.Add(day.BorrowCount);
                DailyActivitySeries[1].Values.Add(day.ReturnCount);
            }

            DailyActivityLabels = dailyActivity
                .Select(d => d.Date.ToString("MMM dd"))
                .ToArray();
        }

        private void UpdateMostBorrowedBooks(List<Borrowing> borrowings, List<Book> books)
        {
            var mostBorrowed = borrowings
                .GroupBy(b => b.BookId)
                .Select(g => new { BookId = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .Join(books, b => b.BookId, book => book.Id, (b, book) => new { Book = book, Count = b.Count })
                .ToList();

            MostBorrowedBooks.Clear();
            foreach (var item in mostBorrowed)
            {
                MostBorrowedBooks.Add(item.Book);
            }
        }

        private void UpdateMostActiveMembers(List<Borrowing> borrowings, List<Member> members)
        {
            var mostActive = borrowings
                .GroupBy(b => b.MemberId)
                .Select(g => new { MemberId = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .Join(members, m => m.MemberId, member => member.Id, (m, member) => new { Member = member, Count = m.Count })
                .ToList();

            MostActiveMembers.Clear();
            foreach (var item in mostActive)
            {
                MostActiveMembers.Add(item.Member);
            }
        }

        private void UpdateRecentBorrowings(List<Borrowing> borrowings)
        {
            var recent = borrowings
                .OrderByDescending(b => b.BorrowDate)
                .Take(10)
                .ToList();

            RecentBorrowings.Clear();
            foreach (var item in recent)
            {
                RecentBorrowings.Add(item);
            }
        }
    }
}