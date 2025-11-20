using Website.Models;

namespace Website.Areas.User.Models
{
    public class TagGroup
    {
        public string TypeTag { get; set; }
        public List<Tag> Tags { get; set; } = new();
    }
}
