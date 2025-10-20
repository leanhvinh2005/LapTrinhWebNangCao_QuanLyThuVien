using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models
{
    public class Collection
    {
        [Key]
        [Required(ErrorMessage = "Please enter collection ID")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Invalid collection ID")]
        public string idCollection { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [StringLength(100, ErrorMessage = "Can't exceed 100 character limit")]
        public string nameCollection { get; set; }

        [Required(ErrorMessage = "Please enter description")]
        public string descriptionCollection { get; set; }

        [Required(ErrorMessage = "Please enter image path")]
        [StringLength(200, ErrorMessage = "Can't exceed 200 character limit")]
        public string imageCollection { get; set; }


        [NotMapped]
        public List<Book> books { get; set; } = new();
    }
}
