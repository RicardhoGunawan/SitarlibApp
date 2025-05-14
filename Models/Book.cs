using System;

namespace SitarLib.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public string Category { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public DateTime AddedDate { get; set; }
    }
}