using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Models.Custom;

namespace Website.Models
{
    public class Book
    {
        [Key]
        [Required(ErrorMessage = "Please enter book ID")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Invalid book ID")]
        public string idBook { get; set; } //Example: CR01. Hai chữ đầu là tên sách, hai số cuối là bản copy thứ mấy

        [Required(ErrorMessage = "Please enter name")]
        [StringLength(100, ErrorMessage = "Can't exceed 100 character limit")]
        public string nameBook { get; set; }

        [Required(ErrorMessage = "Please enter description")]
        public string descriptionBook { get; set; }

        [Required(ErrorMessage = "Please enter author")]
        [StringLength(100, ErrorMessage = "Can't exceed 100 character limit")]
        public string authorBook { get; set; }

        [Required(ErrorMessage = "Please enter publisher")]
        [StringLength(100, ErrorMessage = "Can't exceed 100 character limit")]
        public string publisherBook { get; set; }

        [Required(ErrorMessage = "Please enter date of publishing")]
        [DataType(DataType.Date)]
        [DateFuture]
        public DateOnly dateBook { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Can't exceed 20 character limit")]
        public string formatBook { get; set; }

        [StringLength(200, ErrorMessage = "Can't exceed 200 character limit")]
        public string? noteBook { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Can't exceed 100 character limit")]
        public string statusBook { get; set; }

        [Required(ErrorMessage = "Please enter image path")]
        [StringLength(200, ErrorMessage = "Can't exceed 200 character limit")]
        public string imageBook { get; set; }


        [NotMapped]
        public List<Tag> tags { get; set; } = new();

        [NotMapped]
        public List<Chapter> chapters { get; set; } = new();
    }
}
