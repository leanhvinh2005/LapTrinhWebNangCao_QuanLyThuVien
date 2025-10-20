using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Tag
    {
        [Key]
        [Required]
        public int idTag { get; set; }

        [Required]
        [StringLength(30)]
        public string nameTag { get; set; }
    }
}
