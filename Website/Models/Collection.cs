using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Collection
    {
        [Key]
        public required char idCollection { get; set; }
        public required string nameCollection { get; set; }
        public required string descriptionCollection { get; set; }
        public required string imageCollection { get; set; }

        public List<Book> books { get; set; } = new();
    }
}
