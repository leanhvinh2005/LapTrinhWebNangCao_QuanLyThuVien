using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Chapter
    {
        [Key]
        public required int idChapter { get; set; }
        public required int numberChapter { get; set; }
        public required string titleChapter { get; set; }
        public required string contentChapter { get; set; }
    }
}
