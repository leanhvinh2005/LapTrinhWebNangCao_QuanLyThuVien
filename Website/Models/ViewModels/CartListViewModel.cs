using System.Text.Json.Serialization;

namespace Website.Models.ViewModels
{
    public class CartListViewModel
    {
        [JsonIgnore]
        public ISession? Session { get; set; }
        public List<Book> Books { get; set; } = new();
    }
}
