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
        
        // Tambahan untuk cover buku
        public string CoverImagePath { get; set; }
        public byte[] CoverImageData { get; set; }
        
        // Property untuk mendapatkan path cover image yang valid
        
        public string DisplayCoverPath
        {
            get
            {
                if (!string.IsNullOrEmpty(CoverImagePath) && System.IO.File.Exists(CoverImagePath))
                {
                    return CoverImagePath;
                }
                // Return default cover image path jika tidak ada cover
                return "/Assets/no-cover.png";
            }
        }
    }
}