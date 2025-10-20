using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models
{
    public class User
    {
        [Key]
        [Required]
        public int idUser { get; set; }

        [Required(ErrorMessage = "Please enter username")]
        [StringLength(100, ErrorMessage = "Can't exceed 100 character limit")]
        public string nameUser { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [StringLength(100, ErrorMessage = "Can't exceed 100 character limit")]
        public string emailUser { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [StringLength(100, ErrorMessage = "Can't exceed 100 character limit")]
        public string passwordUser { get; set; }


        [NotMapped]
        public Member? member { get; set; }

        [NotMapped]
        public Librarian? librarian { get; set; }
    }
}
