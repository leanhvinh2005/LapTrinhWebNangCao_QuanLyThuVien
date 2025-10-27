namespace Website.Models.ViewModels
{
    public class TagGroup
    {
        public string TypeTag { get; set; }
        public List<Tag> Tags { get; set; } = new();
    }
}
