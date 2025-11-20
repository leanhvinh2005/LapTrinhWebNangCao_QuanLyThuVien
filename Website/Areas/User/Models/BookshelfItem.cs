using Website.Models;

namespace Website.Areas.User.Models
{
    public class BookshelfItem
    {
        public Book Book { get; set; }
        public BookBorrow BookBorrow { get; set; }
    }
}
