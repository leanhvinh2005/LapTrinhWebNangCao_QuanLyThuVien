using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Models.Custom;

namespace Website.Models
{
    public class Borrow
    {
        [Key]
        [Required]
        public int idBorrow { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateFuture]
        public DateOnly dateBorrow { get; set; }

        [Required]
        [StringLength(100)]
        public string statusBorrow { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 12)]
        public string idCard { get; set; }

        public int? idLibrarian { get; set; }


        [NotMapped]
        public Card? cardBorrow { get; set; }

        [NotMapped]
        public Librarian? librarinBorrow { get; set; }

        //[NotMapped]
        //public List<BookBorrow> bookborrows { get; set; } = new(); 
    }
}
