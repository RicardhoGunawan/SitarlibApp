namespace SitarLib.Models
{
    public class Borrowing
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } // "Borrowed", "Returned", "Overdue"
        //public decimal Fine { get; set; }
        
        // Navigation properties
        public Book Book { get; set; }
        public Member Member { get; set; }
    }
}