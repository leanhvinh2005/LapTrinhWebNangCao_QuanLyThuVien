using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Librarian
    {
        [Key]
        public required int idLibrarian { get; set; }
        public required string roleLibrarian { get; set; }
        public required DateOnly hireLibrarian { get; set; }
        public required string statusLibrarian { get; set; }
        public required int idUser { get; set; }   
        
        public required User userLibrarian { get; set; }
    }
}
