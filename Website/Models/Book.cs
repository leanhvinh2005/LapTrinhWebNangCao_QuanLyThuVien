using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Book
    {
        [Key]
        public required char idBook { get; set; } //Example: CR01. Hai chữ đầu là tên sách, hai số cuối là bản copy thứ mấy
        public required string nameBook { get; set; }
        public required string descriptionBook { get; set; }
        public required string typeBook { get; set; }
        public required string authorBook { get; set; }
        public required string publisherBook { get; set; }
        public required DateOnly dateBook { get; set; }
        public required string formatBook { get; set; }
        public required string noteBook { get; set; }
        public required string statusBook { get; set; }
        public required string imageBook { get; set; }

        public List<Tag> tags { get; set; } = new();
        public List<Chapter> chapters { get; set; } = new();
    }
}
