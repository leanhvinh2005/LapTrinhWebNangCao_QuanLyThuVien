using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Tag
    {
        [Key]
        public required int idTag { get; set; }
        public required string nameTag { get; set; }
    }
}
