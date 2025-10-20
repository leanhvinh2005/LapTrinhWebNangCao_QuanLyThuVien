using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Website.Models.Custom;

namespace Website.Models
{
    public class BookBorrow
    {
        [Required]
        public int idBorrow { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string idBook { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DateFuture]
        public DateOnly startDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly returnDate { get; set; }

        [Required]
        [StringLength(100)]
        public string statusBookBorrow { get; set; }


        [NotMapped]
        public Borrow? borrow { get; set; }

        [NotMapped]
        public Book? book { get; set; }
    }
}
