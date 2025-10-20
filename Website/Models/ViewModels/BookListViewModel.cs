namespace Website.Models.ViewModels
{
    public class BookListViewModel
    {
        public List<Book> Books { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
        public PagingInfo PagingInfo { get; set; } = new();
    }
}
