using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class BookBorrow
    {
        public required int idBorrow { get; set; }
        public required char idBook { get; set; }
        public required DateOnly startDate { get; set; }
        public required DateOnly returnDate { get; set; }
        public required string statusBookBorrow { get; set; }

        public required Borrow borrow { get; set; }
        public required Book book { get; set; }
    }
}
