using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Models
{
    public class Member
    {
        [Key]
        [Required]
        public int idMember { get; set; }

        [Required]
        [StringLength(100)]
        public string statusMember { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 12)]
        public string idCard { get; set; }

        [Required]
        public int idUser { get; set; }


        [NotMapped]
        public Card? cardMember { get; set; }

        [NotMapped]
        public User? userMember { get; set; }
    }
}
