using Website.Models;

namespace Website.Areas.User.Models
{
    public class HomeList
    {
        public List<Book> Banner { get; set; } = new();
        public List<Book> Carousel1 { get; set; } = new();
        public List<Book> Carousel2 { get; set; } = new();
        public List<Book> Carousel3 { get; set; } = new();
        public List<Book> Carousel4 { get; set; } = new();
    }
}
