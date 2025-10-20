using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Models.Custom;

namespace Website.Models
{
    public class Card
    {
        [Key]
        [Required(ErrorMessage = "Please enter card ID")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Invalid card ID")]
        public string idCard { get; set; } //Example: CA04B8AU91P. Mã random

        [Required(ErrorMessage = "Please enter full name")]
        [StringLength(100, ErrorMessage = "Can't exceed 100 character limit")]
        public string nameCard { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [StringLength(100, ErrorMessage = "Can't exceed 100 character limit")]
        public string emailCard { get; set; }

        [Required(ErrorMessage = "Please enter address")]
        [StringLength(200, ErrorMessage = "Can't exceed 200 character limit")]
        public string addressCard { get; set; }

        [Required(ErrorMessage = "Please enter phone number")]
        [StringLength(100, ErrorMessage = "Can't exceed 100 character limit")]
        public string phoneCard { get; set; }

        [Required(ErrorMessage = "Please enter date of birth")]
        [DataType(DataType.Date)]
        [BirthDate(16)]
        public DateOnly dateCard { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly startCard { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly expireCard { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Can't exceed 100 character limit")]
        public string statusCard { get; set; }


        [NotMapped]
        public List<Borrow> borrows { get; set; } = new();
    }
}
