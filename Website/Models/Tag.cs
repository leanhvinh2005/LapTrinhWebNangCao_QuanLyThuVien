using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Tag
    {
        [Key]
        [Required]
        public int idTag { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [StringLength(30, ErrorMessage = "Can't exceed 30 character limit")]
        public string nameTag { get; set; }

        [Required(ErrorMessage = "Please enter type")]
        [StringLength(30, ErrorMessage = "Can't exceed 30 character limit")]
        public string typeTag { get; set; }
    }
}
