using Website.Models;
using Website.Models.ViewModels;

namespace Website.Areas.User.Models
{
    public class BookList
    {
        public List<Book> Books { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
        public PagingInfo PagingInfo { get; set; } = new();
    }
}
