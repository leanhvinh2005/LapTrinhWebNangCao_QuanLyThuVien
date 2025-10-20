using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Chapter
    {
        [Key]
        [Required]
        public string idChapter { get; set; }

        [Required]
        public int numberChapter { get; set; }

        [Required(ErrorMessage = "Please enter title")]
        [StringLength(100, ErrorMessage = "Can't exceed 100 character limit")]
        public string titleChapter { get; set; }

        [Required(ErrorMessage = "Please enter content")]
        public string contentChapter { get; set; }


    }
}
