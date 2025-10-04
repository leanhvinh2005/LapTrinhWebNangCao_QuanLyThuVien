using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Member
    {
        [Key]
        public required int idMember { get; set; }
        public required string statusMember { get; set; }
        public required char idCard { get; set; }
        public required int idUser { get; set; }

        public required Card cardMember { get; set; }
        public required User userMember { get; set; }
    }
}
