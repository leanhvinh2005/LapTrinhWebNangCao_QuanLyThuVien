using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class User
    {
        [Key]
        public required int idUser { get; set; }
        public required string nameUser { get; set; }
        public required string emailUser { get; set; }
        public required string passwordUser { get; set; }

        public required Member member { get; set; }
        public required Librarian librarian { get; set; }
    }
}
