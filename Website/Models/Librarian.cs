using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Models.Custom;

namespace Website.Models
{
    public class Librarian
    {
        [Key]
        [Required]
        public int idLibrarian { get; set; }

        [Required]
        [StringLength(30)]
        public string roleLibrarian { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateFuture]
        public DateOnly hireLibrarian { get; set; }

        [Required]
        [StringLength(100)]
        public string statusLibrarian { get; set; }

        [Required]
        public int idUser { get; set; }


        [NotMapped]
        public User? userLibrarian { get; set; }
    }
}
