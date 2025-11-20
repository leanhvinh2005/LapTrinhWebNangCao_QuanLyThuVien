using Website.Models.ViewModels;

namespace Website.Areas.User.Models
{
    public class BookshelfList
    {
        public List<BookshelfItem> BookshelfItems { get; set; } = new();
        public PagingInfo PagingInfo { get; set; } = new();
    }
}
