using System.Text.Json.Serialization;
using Website.Models;

namespace Website.Areas.User.Models
{
    public class CartList
    {
        [JsonIgnore]
        public ISession? Session { get; set; }
        public List<Book> Books { get; set; } = new();
    }
}
