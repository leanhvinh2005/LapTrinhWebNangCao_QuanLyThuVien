using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Borrow
    {
        [Key]
        public required int idBorrow { get; set; }
        public required DateOnly dateBorrow { get; set; }
        public required string statusBorrow { get; set; }
        public required char idCard { get; set; }
        public required int? idLibrarian { get; set; }

        public required Card cardBorrow { get; set; }
        public Librarian? librarinBorrow { get; set; }
        public required List<BookBorrow> bookborrows { get; set; } = new(); //List sách trong phiếu mượn và chi tiết từng sách như thế nào
    }
}
