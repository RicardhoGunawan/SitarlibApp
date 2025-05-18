using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using SitarLib.Models;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Member = SitarLib.Models.Member;

namespace SitarLib.Services
{
    public class DataService
    {
        private readonly string _connectionString;
        private readonly string _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SitarLib.db");


        public DataService()
        {
            _connectionString = $"Data Source={_dbPath};Version=3;";
        
            // Create database if it doesn't exist
            if (!File.Exists(_dbPath))
            {
                CreateDatabase();
            }
        }

        private void CreateDatabase()
        {
            Console.WriteLine("Database Path: " + _dbPath);
            SQLiteConnection.CreateFile(_dbPath);
        
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
            
                // Create tables
                CreateBookTable(connection);
                CreateMemberTable(connection);
                CreateUserTable(connection);
                CreateBorrowingTable(connection);
            
                // Insert default admin user
                InsertDefaultUser(connection);
            
                connection.Close();
            }
        }

        private void CreateBookTable(SQLiteConnection connection)
        {
            string sql = @"
                CREATE TABLE Books (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ISBN TEXT NOT NULL,
                    Title TEXT NOT NULL,
                    Author TEXT NOT NULL,
                    Publisher TEXT,
                    PublicationYear INTEGER,
                    Category TEXT,
                    Stock INTEGER,
                    Description TEXT,
                    AddedDate TEXT
                );";
                    
            ExecuteNonQuery(connection, sql);
        }

        private void CreateMemberTable(SQLiteConnection connection)
        {
            string sql = @"
                CREATE TABLE Members (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    MemberCode TEXT NOT NULL,
                    FullName TEXT NOT NULL,
                    Class TEXT,
                    Address TEXT,
                    PhoneNumber TEXT,
                    Email TEXT,
                    RegistrationDate TEXT,
                    MembershipExpiry TEXT,
                    IsActive INTEGER
                );";
                    
            ExecuteNonQuery(connection, sql);
        }

        private void CreateUserTable(SQLiteConnection connection)
        {
            string sql = @"
                CREATE TABLE Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Username TEXT NOT NULL UNIQUE,
                    Password TEXT NOT NULL,
                    FullName TEXT,
                    Role TEXT,
                    IsActive INTEGER,
                    LastLogin TEXT
                );";
                    
            ExecuteNonQuery(connection, sql);
        }

        private void CreateBorrowingTable(SQLiteConnection connection)
        {
            string sql = @"
                CREATE TABLE Borrowings (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    BookId INTEGER,
                    MemberId INTEGER,
                    BorrowDate TEXT,
                    DueDate TEXT,
                    ReturnDate TEXT NULL,
                    Status TEXT,
                    Fine REAL,
                    Quantity INTEGER DEFAULT 1,
                    FOREIGN KEY (BookId) REFERENCES Books(Id),
                    FOREIGN KEY (MemberId) REFERENCES Members(Id)
                );";
                    
            ExecuteNonQuery(connection, sql);
        }

        private void InsertDefaultUser(SQLiteConnection connection)
        {
            string sql = @"
                INSERT INTO Users (Username, Password, FullName, Role, IsActive, LastLogin)
                VALUES (@Username, @Password, @FullName, @Role, @IsActive, @LastLogin);";
                    
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Username", "admin");
                command.Parameters.AddWithValue("@Password", "admin123"); // should be hashed in production
                command.Parameters.AddWithValue("@FullName", "Administrator");
                command.Parameters.AddWithValue("@Role", "Admin");
                command.Parameters.AddWithValue("@IsActive", 1);
                command.Parameters.AddWithValue("@LastLogin", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.ExecuteNonQuery();
            }

            // Tambah staff user juga
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Username", "staff");
                command.Parameters.AddWithValue("@Password", "staff123");
                command.Parameters.AddWithValue("@FullName", "Staff User");
                command.Parameters.AddWithValue("@Role", "Staff");
                command.Parameters.AddWithValue("@IsActive", 1);
                command.Parameters.AddWithValue("@LastLogin", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                command.ExecuteNonQuery();
            }
        }

        private void ExecuteNonQuery(SQLiteConnection connection, string sql)
        {
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }

        // Helper method for executing queries
        private List<T> ExecuteQuery<T>(string sql, Func<SQLiteDataReader, T> map, Dictionary<string, object> parameters = null)
        {
            var result = new List<T>();
            
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                
                using (var command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue($"@{param.Key}", param.Value);
                        }
                    }
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(map(reader));
                        }
                    }
                }
                
                connection.Close();
            }
            
            return result;
        }

        // Helper method for executing non-query commands
        private int ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                
                using (var command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue($"@{param.Key}", param.Value);
                        }
                    }
                    
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }

        // Helper method for executing scalar queries
        private T ExecuteScalar<T>(string sql, Dictionary<string, object> parameters = null)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                
                using (var command = new SQLiteCommand(sql, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue($"@{param.Key}", param.Value);
                        }
                    }
                    
                    var result = command.ExecuteScalar();
                    connection.Close();
                    return (T)Convert.ChangeType(result, typeof(T));
                }
            }
        }

        #region Book Methods
        
        public List<Book> GetAllBooks()
        {
            string sql = "SELECT * FROM Books ORDER BY Title;";
            
            return ExecuteQuery(sql, reader => new Book
            {
                Id = Convert.ToInt32(reader["Id"]),
                ISBN = reader["ISBN"].ToString(),
                Title = reader["Title"].ToString(),
                Author = reader["Author"].ToString(),
                Publisher = reader["Publisher"].ToString(),
                PublicationYear = reader["PublicationYear"] != DBNull.Value ? Convert.ToInt32(reader["PublicationYear"]) : 0,
                Category = reader["Category"].ToString(),
                Stock = reader["Stock"] != DBNull.Value ? Convert.ToInt32(reader["Stock"]) : 0,
                Description = reader["Description"].ToString(),
                AddedDate = reader["AddedDate"] != DBNull.Value ? DateTime.Parse(reader["AddedDate"].ToString()) : DateTime.Now
            });
        }
        
        public Book GetBookById(int id)
        {
            string sql = "SELECT * FROM Books WHERE Id = @Id;";
            var parameters = new Dictionary<string, object> { { "Id", id } };
            
            var books = ExecuteQuery(sql, reader => new Book
            {
                Id = Convert.ToInt32(reader["Id"]),
                ISBN = reader["ISBN"].ToString(),
                Title = reader["Title"].ToString(),
                Author = reader["Author"].ToString(),
                Publisher = reader["Publisher"].ToString(),
                PublicationYear = reader["PublicationYear"] != DBNull.Value ? Convert.ToInt32(reader["PublicationYear"]) : 0,
                Category = reader["Category"].ToString(),
                Stock = reader["Stock"] != DBNull.Value ? Convert.ToInt32(reader["Stock"]) : 0,
                Description = reader["Description"].ToString(),
                AddedDate = reader["AddedDate"] != DBNull.Value ? DateTime.Parse(reader["AddedDate"].ToString()) : DateTime.Now
            }, parameters);
            
            return books.Count > 0 ? books[0] : null;
        }
        
        public int AddBook(Book book)
        {
            string sql = @"
                INSERT INTO Books (ISBN, Title, Author, Publisher, PublicationYear, Category, Stock, Description, AddedDate)
                VALUES (@ISBN, @Title, @Author, @Publisher, @PublicationYear, @Category, @Stock, @Description, @AddedDate);
                SELECT last_insert_rowid();";
            
            var parameters = new Dictionary<string, object>
            {
                { "ISBN", book.ISBN },
                { "Title", book.Title },
                { "Author", book.Author },
                { "Publisher", book.Publisher ?? (object)DBNull.Value },
                { "PublicationYear", book.PublicationYear },
                { "Category", book.Category ?? (object)DBNull.Value },
                { "Stock", book.Stock },
                { "Description", book.Description ?? (object)DBNull.Value },
                { "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }
            };
            
            return ExecuteScalar<int>(sql, parameters);
        }
        
        public bool UpdateBook(Book book)
        {
            string sql = @"
                UPDATE Books
                SET ISBN = @ISBN,
                    Title = @Title,
                    Author = @Author,
                    Publisher = @Publisher,
                    PublicationYear = @PublicationYear,
                    Category = @Category,
                    Stock = @Stock,
                    Description = @Description
                WHERE Id = @Id;";
            
            var parameters = new Dictionary<string, object>
            {
                { "Id", book.Id },
                { "ISBN", book.ISBN },
                { "Title", book.Title },
                { "Author", book.Author },
                { "Publisher", book.Publisher ?? (object)DBNull.Value },
                { "PublicationYear", book.PublicationYear },
                { "Category", book.Category ?? (object)DBNull.Value },
                { "Stock", book.Stock },
                { "Description", book.Description ?? (object)DBNull.Value }
            };
            
            return ExecuteNonQuery(sql, parameters) > 0;
        }
        
        public bool DeleteBook(int id)
        {
            // First check if the book is being borrowed
            string checkSql = "SELECT COUNT(*) FROM Borrowings WHERE BookId = @BookId AND ReturnDate IS NULL;";
            var checkParams = new Dictionary<string, object> { { "BookId", id } };
            int borrowedCount = ExecuteScalar<int>(checkSql, checkParams);
            
            if (borrowedCount > 0)
            {
                return false; // Cannot delete a book that is currently being borrowed
            }
            
            string sql = "DELETE FROM Books WHERE Id = @Id;";
            var parameters = new Dictionary<string, object> { { "Id", id } };
            
            return ExecuteNonQuery(sql, parameters) > 0;
        }
        private int _duplicateCount = 0;

        /*private int GetDuplicateCount()
        {
            int count = _duplicateCount;
            _duplicateCount = 0; // Reset for next import
            return count;
        }*/
        // Add this method to check if a book already exists
        public bool BookExists(string isbn)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();
            
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT COUNT(*) FROM Books WHERE ISBN = @ISBN";
                        command.Parameters.AddWithValue("@ISBN", isbn);
                
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[BookExists] Error: {ex}");
                return false;
            }
        }
        public async Task<(int imported, int duplicates)> ImportBooksFromFileAsync(string filePath, string fileType)
        {
            return await Task.Run(() => 
            {
                int imported = -1;
                int duplicates = 0;

                if (fileType.ToLower() == "csv")
                {
                    // Ubah untuk menerima tuple yang berisi imported dan duplicates
                    var result = ImportBooksFromCsv(filePath);
                    imported = result.imported;
                    duplicates = result.duplicates;
                }
                else if (fileType.ToLower() == "xlsx")
                {
                    // Ubah untuk menerima tuple yang berisi imported dan duplicates
                    var result = ImportBooksFromXlsx(filePath);
                    imported = result.imported;
                    duplicates = result.duplicates;
                }

                return (imported, duplicates);
            });
        }
        // Modified ImportBooksFromCsv method to check for duplicates
        // Ubah return type menjadi tuple untuk mengembalikan jumlah yang diimpor dan duplikat
        public (int imported, int duplicates) ImportBooksFromCsv(string filePath)
        {
            int importedCount = 0;
            int duplicateCount = 0;
            
            try
            {
                // Check if file exists
                if (!File.Exists(filePath))
                {
                    return (-1, 0);
                }
                
                // Read all lines from the CSV file
                string[] lines = File.ReadAllLines(filePath);
                
                // Skip header row
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (string.IsNullOrWhiteSpace(line))
                        continue;
                    
                    string[] parts = line.Split(',');
                    
                    // Validate the CSV format (should have at least basic required fields)
                    if (parts.Length < 3)
                        continue;
                    
                    string isbn = parts[0].Trim('"');
                    
                    // Check if book already exists
                    if (BookExists(isbn))
                    {
                        duplicateCount++;
                        continue; // Skip this book
                    }
                    
                    // Create a new book object
                    Book book = new Book
                    {
                        ISBN = isbn,
                        Title = parts[1].Trim('"'),
                        Author = parts[2].Trim('"'),
                        Publisher = parts.Length > 3 ? parts[3].Trim('"') : null,
                        PublicationYear = parts.Length > 4 && int.TryParse(parts[4].Trim('"'), out int year) ? year : DateTime.Now.Year,
                        Category = parts.Length > 5 ? parts[5].Trim('"') : null,
                        Stock = parts.Length > 6 && int.TryParse(parts[6].Trim('"'), out int stock) ? stock : 1,
                        Description = parts.Length > 7 ? parts[7].Trim('"') : null,
                        AddedDate = DateTime.Now
                    };
                    
                    // Add the book to the database
                    AddBook(book);
                    importedCount++;
                }
                
                // Return both the imported count and duplicate count
                return (importedCount, duplicateCount);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ImportBooksFromCsv] Error: {ex}");
                return (-1, 0);
            }
        }
        // Modified ImportBooksFromXlsx method to check for duplicates
        // Ubah return type menjadi tuple untuk mengembalikan jumlah yang diimpor dan duplikat
        public (int imported, int duplicates) ImportBooksFromXlsx(string filePath)
        {
            int importedCount = 0;
            int duplicateCount = 0;

            try
            {
                if (!File.Exists(filePath))
                    return (-1, 0);

                using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
                {
                    WorkbookPart workbookPart = document.WorkbookPart;
                    var worksheetPart = workbookPart.WorksheetParts.First();
                    var sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                    var rows = sheetData.Elements<Row>().ToList();

                    if (rows.Count <= 1)
                        return (-1, 0); // tidak ada data selain header

                    // Proses setiap baris, skip header
                    foreach (var row in rows.Skip(1))
                    {
                        var values = GetRowValues(row, workbookPart);

                        // Minimal harus ada ISBN (col 0), Title (col 1), Author (col 2)
                        if (!values.ContainsKey(0) || !values.ContainsKey(1) || !values.ContainsKey(2))
                            continue;

                        string isbn = values[0];
                        
                        // Check if book already exists
                        if (BookExists(isbn))
                        {
                            duplicateCount++;
                            continue; // Skip this book
                        }

                        var book = new Book
                        {
                            ISBN = isbn,
                            Title = values[1],
                            Author = values[2],
                            Publisher = values.ContainsKey(3) ? values[3] : null,
                            PublicationYear = values.ContainsKey(4) && int.TryParse(values[4], out var y) ? y : DateTime.Now.Year,
                            Category = values.ContainsKey(5) ? values[5] : null,
                            Stock = values.ContainsKey(6) && int.TryParse(values[6], out var s) ? s : 1,
                            Description = values.ContainsKey(7) ? values[7] : null,
                            AddedDate = DateTime.Now
                        };

                        AddBook(book);
                        importedCount++;
                    }
                }

                // Return both the imported count and duplicate count
                return (importedCount, duplicateCount);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ImportBooksFromXlsx] Error: {ex}");
                return (-1, 0);
            }
        }
        // Helper method to extract cell values from Excel
        private Dictionary<int, string> GetRowValues(Row row, WorkbookPart wp)
        {
            var result = new Dictionary<int, string>();

            foreach (var cell in row.Elements<Cell>())
            {
                // Extract huruf dari CellReference, misal "C5" → "C"
                string colRef = new string(cell.CellReference.Value
                    .Where(char.IsLetter)
                    .ToArray());
                int colIndex = ConvertColumnNameToNumber(colRef) - 1;
                result[colIndex] = GetCellValue(cell, wp);
            }

            return result;
        }
        private int ConvertColumnNameToNumber(string columnName)
        {
            int sum = 0;
            foreach (char c in columnName.ToUpper())
            {
                sum *= 26;
                sum += (c - 'A' + 1);
            }
            return sum;
        }
        private string GetCellValue(Cell cell, WorkbookPart wp)
        {
            if (cell == null)
                return string.Empty;

            string value = cell.InnerText ?? string.Empty;

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                var sst = wp.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                if (sst != null && int.TryParse(value, out int idx))
                    return sst.SharedStringTable.ElementAt(idx).InnerText;
            }

            return value;
        }
        
        #endregion
        
        #region Member Methods
        
        public List<Member> GetAllMembers()
        {
            string sql = "SELECT * FROM Members ORDER BY FullName;";
            
            return ExecuteQuery(sql, reader => new Member
            {
                Id = Convert.ToInt32(reader["Id"]),
                MemberCode = reader["MemberCode"].ToString(),
                FullName = reader["FullName"].ToString(),
                Class = reader["Class"].ToString(),
                Address = reader["Address"].ToString(),
                PhoneNumber = reader["PhoneNumber"].ToString(),
                Email = reader["Email"].ToString(),
                RegistrationDate = reader["RegistrationDate"] != DBNull.Value ? DateTime.Parse(reader["RegistrationDate"].ToString()) : DateTime.Now,
                MembershipExpiry = reader["MembershipExpiry"] != DBNull.Value ? DateTime.Parse(reader["MembershipExpiry"].ToString()) : DateTime.Now.AddYears(1),
                IsActive = Convert.ToBoolean(reader["IsActive"])
            });
        }
        
        public Member GetMemberById(int id)
        {
            string sql = "SELECT * FROM Members WHERE Id = @Id;";
            var parameters = new Dictionary<string, object> { { "Id", id } };
            
            var members = ExecuteQuery(sql, reader => new Member
            {
                Id = Convert.ToInt32(reader["Id"]),
                MemberCode = reader["MemberCode"].ToString(),
                FullName = reader["FullName"].ToString(),
                Class = reader["Class"].ToString(),
                Address = reader["Address"].ToString(),
                PhoneNumber = reader["PhoneNumber"].ToString(),
                Email = reader["Email"].ToString(),
                RegistrationDate = reader["RegistrationDate"] != DBNull.Value ? DateTime.Parse(reader["RegistrationDate"].ToString()) : DateTime.Now,
                MembershipExpiry = reader["MembershipExpiry"] != DBNull.Value ? DateTime.Parse(reader["MembershipExpiry"].ToString()) : DateTime.Now.AddYears(1),
                IsActive = Convert.ToBoolean(reader["IsActive"])
            }, parameters);
            
            return members.Count > 0 ? members[0] : null;
        }
        
        public int AddMember(Member member)
        {
            string sql = @"
            INSERT INTO Members (MemberCode, FullName, Class, Address, PhoneNumber, Email, RegistrationDate, MembershipExpiry, IsActive)
            VALUES (@MemberCode, @FullName, @Class, @Address, @PhoneNumber, @Email, @RegistrationDate, @MembershipExpiry, @IsActive);
            SELECT last_insert_rowid();";
        
            var parameters = new Dictionary<string, object>
            {
                { "MemberCode", member.MemberCode },
                { "FullName", member.FullName },
                { "Class", member.Class ?? (object)DBNull.Value },
                { "Address", member.Address ?? (object)DBNull.Value },
                { "PhoneNumber", member.PhoneNumber ?? (object)DBNull.Value },
                { "Email", member.Email ?? (object)DBNull.Value },
                { "RegistrationDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                { "MembershipExpiry", DateTime.Now.AddYears(1).ToString("yyyy-MM-dd HH:mm:ss") },
                { "IsActive", member.IsActive ? 1 : 0 }
            };
        
            return ExecuteScalar<int>(sql, parameters);
        }
        
        public bool UpdateMember(Member member)
        {
            string sql = @"
                UPDATE Members
                SET MemberCode = @MemberCode,
                    FullName = @FullName,
                    Class = @Class,
                    Address = @Address,
                    PhoneNumber = @PhoneNumber,
                    Email = @Email,
                    MembershipExpiry = @MembershipExpiry,
                    IsActive = @IsActive
                WHERE Id = @Id;";
            
            var parameters = new Dictionary<string, object>
            {
                { "Id", member.Id },
                { "MemberCode", member.MemberCode },
                { "FullName", member.FullName },
                { "Class", member.Class ?? (object)DBNull.Value },
                { "Address", member.Address ?? (object)DBNull.Value },
                { "PhoneNumber", member.PhoneNumber ?? (object)DBNull.Value },
                { "Email", member.Email ?? (object)DBNull.Value },
                { "MembershipExpiry", member.MembershipExpiry.ToString("yyyy-MM-dd HH:mm:ss") },
                { "IsActive", member.IsActive ? 1 : 0 }
            };
            
            return ExecuteNonQuery(sql, parameters) > 0;
        }
        
        public bool DeleteMember(int id)
        {
            // First check if the member has active borrowings
            string checkSql = "SELECT COUNT(*) FROM Borrowings WHERE MemberId = @MemberId AND ReturnDate IS NULL;";
            var checkParams = new Dictionary<string, object> { { "MemberId", id } };
            int activeBorrowings = ExecuteScalar<int>(checkSql, checkParams);
            
            if (activeBorrowings > 0)
            {
                return false; // Cannot delete a member with active borrowings
            }
            
            string sql = "DELETE FROM Members WHERE Id = @Id;";
            var parameters = new Dictionary<string, object> { { "Id", id } };
            
            return ExecuteNonQuery(sql, parameters) > 0;
        }
        
        #endregion
        
        #region Borrowing Methods
        
        public List<Borrowing> GetAllBorrowings()
        {
            string sql = @"
                SELECT b.*, bk.Title as BookTitle, m.FullName as MemberName
                FROM Borrowings b
                INNER JOIN Books bk ON b.BookId = bk.Id
                INNER JOIN Members m ON b.MemberId = m.Id
                ORDER BY b.BorrowDate DESC;";
            
            return ExecuteQuery(sql, reader =>
            {
                var borrowing = new Borrowing
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    BookId = Convert.ToInt32(reader["BookId"]),
                    MemberId = Convert.ToInt32(reader["MemberId"]),
                    BorrowDate = DateTime.Parse(reader["BorrowDate"].ToString()),
                    DueDate = DateTime.Parse(reader["DueDate"].ToString()),
                    ReturnDate = reader["ReturnDate"] != DBNull.Value ? (DateTime?)DateTime.Parse(reader["ReturnDate"].ToString()) : null,
                    Status = reader["Status"].ToString(),
                    //Fine = Convert.ToDecimal(reader["Fine"]),
                    Quantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 1, // Add this line

                    
                    // Create partial Book and Member objects with just the ID and name
                    Book = new Book { Id = Convert.ToInt32(reader["BookId"]), Title = reader["BookTitle"].ToString() },
                    Member = new Member { Id = Convert.ToInt32(reader["MemberId"]), FullName = reader["MemberName"].ToString() }
                };
                
                return borrowing;
            });
        }
        
        public List<Borrowing> GetOverdueBorrowings()
        {
            const string sql = @"
            SELECT b.*,
                   bk.Title    AS BookTitle,
                   m.FullName  AS MemberName
            FROM Borrowings b
            JOIN Books   bk ON bk.Id = b.BookId
            JOIN Members m  ON m.Id  = b.MemberId
            WHERE b.ReturnDate IS NULL
              AND DATE(b.DueDate) < DATE(datetime('now', 'localtime'))
            ORDER BY b.DueDate;";

            return ExecuteQuery(sql, reader =>
            {
                return new Borrowing
                {
                    Id         = Convert.ToInt32(reader["Id"]),
                    BookId     = Convert.ToInt32(reader["BookId"]),
                    MemberId   = Convert.ToInt32(reader["MemberId"]),
                    BorrowDate = DateTime.Parse(reader["BorrowDate"].ToString()),
                    DueDate    = DateTime.Parse(reader["DueDate"].ToString()),
                    ReturnDate = null,
                    Status     = "Overdue",
                    Quantity   = reader["Quantity"] != DBNull.Value
                        ? Convert.ToInt32(reader["Quantity"])
                        : 1,
                    Book   = new Book   { Id = Convert.ToInt32(reader["BookId"]),   Title    = reader["BookTitle"].ToString() },
                    Member = new Member { Id = Convert.ToInt32(reader["MemberId"]), FullName = reader["MemberName"].ToString() }
                };
            });
        }



        
        public Borrowing GetBorrowingById(int id)
        {
            string sql = @"
                SELECT b.*, bk.Title as BookTitle, m.FullName as MemberName
                FROM Borrowings b
                INNER JOIN Books bk ON b.BookId = bk.Id
                INNER JOIN Members m ON b.MemberId = m.Id
                WHERE b.Id = @Id;";
            
            var parameters = new Dictionary<string, object> { { "Id", id } };
            
            var borrowings = ExecuteQuery(sql, reader =>
            {
                var borrowing = new Borrowing
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    BookId = Convert.ToInt32(reader["BookId"]),
                    MemberId = Convert.ToInt32(reader["MemberId"]),
                    BorrowDate = DateTime.Parse(reader["BorrowDate"].ToString()),
                    DueDate = DateTime.Parse(reader["DueDate"].ToString()),
                    ReturnDate = reader["ReturnDate"] != DBNull.Value ? (DateTime?)DateTime.Parse(reader["ReturnDate"].ToString()) : null,
                    Status = reader["Status"].ToString(),
                    Quantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 1, // Add this line
                    //Fine = Convert.ToDecimal(reader["Fine"]),
                    
                    // Create partial Book and Member objects with just the ID and name
                    Book = new Book { Id = Convert.ToInt32(reader["BookId"]), Title = reader["BookTitle"].ToString() },
                    Member = new Member { Id = Convert.ToInt32(reader["MemberId"]), FullName = reader["MemberName"].ToString() }
                };
                
                return borrowing;
            }, parameters);
            
            return borrowings.Count > 0 ? borrowings[0] : null;
        }
        
        public int AddBorrowing(Borrowing borrowing)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                
                // Begin transaction
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // First check if book is available in sufficient quantity
                        string checkSql = "SELECT Stock FROM Books WHERE Id = @BookId;";
                        using (var command = new SQLiteCommand(checkSql, connection))
                        {
                            command.Parameters.AddWithValue("@BookId", borrowing.BookId);
                            var stock = Convert.ToInt32(command.ExecuteScalar());
                            
                            if (stock < borrowing.Quantity)
                            {
                                transaction.Rollback();
                                return -1; // Insufficient stock
                            }
                        }
                        
                        // Insert borrowing record
                        string sql = @"
                            INSERT INTO Borrowings (BookId, MemberId, BorrowDate, DueDate, Status, Fine, Quantity)
                            VALUES (@BookId, @MemberId, @BorrowDate, @DueDate, @Status, @Fine, @Quantity);
                            SELECT last_insert_rowid();";
                        
                        int newId;
                        using (var command = new SQLiteCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@BookId", borrowing.BookId);
                            command.Parameters.AddWithValue("@MemberId", borrowing.MemberId);
                            command.Parameters.AddWithValue("@BorrowDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            command.Parameters.AddWithValue("@DueDate", borrowing.DueDate.ToString("yyyy-MM-dd HH:mm:ss"));
                            command.Parameters.AddWithValue("@Status", "Borrowed");
                            command.Parameters.AddWithValue("@Fine", 0);
                            command.Parameters.AddWithValue("@Quantity", borrowing.Quantity);
                            
                            newId = Convert.ToInt32(command.ExecuteScalar());
                        }
                        
                        // Update book stock
                        string updateSql = "UPDATE Books SET Stock = Stock - @Quantity WHERE Id = @BookId;";
                        using (var command = new SQLiteCommand(updateSql, connection))
                        {
                            command.Parameters.AddWithValue("@BookId", borrowing.BookId);
                            command.Parameters.AddWithValue("@Quantity", borrowing.Quantity);
                            command.ExecuteNonQuery();
                        }
                        
                        transaction.Commit();
                        return newId;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        
        public bool ReturnBook(int borrowingId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                
                // Begin transaction
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Get borrowing info first
                        string getSql = "SELECT BookId, DueDate, Quantity FROM Borrowings WHERE Id = @Id AND ReturnDate IS NULL;";
                        int bookId;
                        DateTime dueDate;
                        int quantity;
                        
                        using (var command = new SQLiteCommand(getSql, connection))
                        {
                            command.Parameters.AddWithValue("@Id", borrowingId);
                            using (var reader = command.ExecuteReader())
                            {
                                if (!reader.Read())
                                {
                                    transaction.Rollback();
                                    return false; // Borrowing not found or already returned
                                }
                                
                                bookId = Convert.ToInt32(reader["BookId"]);
                                dueDate = DateTime.Parse(reader["DueDate"].ToString());
                                quantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 1;
                            }
                        }
                        
                        // Calculate fine if overdue (assume $1 per day per book)
                        decimal fine = 0;
                        string status = "Returned";
                        
                        TimeSpan overdueSpan = DateTime.Now.Date - dueDate.Date;
                        if (overdueSpan.TotalDays > 1)
                        {
                            fine = (decimal)overdueSpan.TotalDays * 1.0m * quantity;
                            status = "Returned Late";
                        }

                        
                        // Update borrowing record
                        string updateSql = @"
                            UPDATE Borrowings
                            SET ReturnDate = @ReturnDate,
                                Status = @Status,
                                Fine = @Fine
                            WHERE Id = @Id;";
                        
                        using (var command = new SQLiteCommand(updateSql, connection))
                        {
                            command.Parameters.AddWithValue("@Id", borrowingId);
                            command.Parameters.AddWithValue("@ReturnDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            command.Parameters.AddWithValue("@Status", status);
                            command.Parameters.AddWithValue("@Fine", fine);
                            
                            command.ExecuteNonQuery();
                        }
                        
                        // Update book stock
                        string bookSql = "UPDATE Books SET Stock = Stock + @Quantity WHERE Id = @BookId;";
                        using (var command = new SQLiteCommand(bookSql, connection))
                        {
                            command.Parameters.AddWithValue("@BookId", bookId);
                            command.Parameters.AddWithValue("@Quantity", quantity);
                            command.ExecuteNonQuery();
                        }
                        
                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        
        #endregion
        
        #region User Methods
        
        public User AuthenticateUser(string username, string password)
        {
            string sql = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password AND IsActive = 1;";
            var parameters = new Dictionary<string, object>
            {
                { "Username", username },
                { "Password", password }
            };
            
            var users = ExecuteQuery(sql, reader => new User
            {
                Id = Convert.ToInt32(reader["Id"]),
                Username = reader["Username"].ToString(),
                Password = reader["Password"].ToString(),
                FullName = reader["FullName"].ToString(),
                Role = reader["Role"].ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                LastLogin = reader["LastLogin"] != DBNull.Value ? DateTime.Parse(reader["LastLogin"].ToString()) : DateTime.Now
            }, parameters);
            
            if (users.Count > 0)
            {
                // Update last login time
                string updateSql = "UPDATE Users SET LastLogin = @LastLogin WHERE Id = @Id;";
                var updateParams = new Dictionary<string, object>
                {
                    { "Id", users[0].Id },
                    { "LastLogin", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") }
                };
                
                ExecuteNonQuery(updateSql, updateParams);
                
                return users[0];
            }
            
            return null;
        }
        
        public List<User> GetAllUsers()
        {
            string sql = "SELECT * FROM Users;";
            
            return ExecuteQuery(sql, reader => new User
            {
                Id = Convert.ToInt32(reader["Id"]),
                Username = reader["Username"].ToString(),
                Password = reader["Password"].ToString(),
                FullName = reader["FullName"].ToString(),
                Role = reader["Role"].ToString(),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                LastLogin = reader["LastLogin"] != DBNull.Value ? DateTime.Parse(reader["LastLogin"].ToString()) : DateTime.Now
            });
        }
        
        #endregion

        public User? CurrentUser { get; private set; }

        public void ClearCurrentUser()
        {
            CurrentUser = null;
        }

    }
}