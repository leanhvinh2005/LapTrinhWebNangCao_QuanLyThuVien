using System.ComponentModel.DataAnnotations;

namespace Website.Models
{
    public class Card
    {
        [Key]
        public required char idCard { get; set; } //Example: CA04B8AU91P. Mã random
        public required string nameCard { get; set; }
        public required string emailCard { get; set; }
        public required string addressCard { get; set; }
        public required string phoneCard { get; set; }
        public required DateOnly dateCard { get; set; }
        public required DateOnly startCard { get; set; }
        public required DateOnly expireCard { get; set; }
        public required string statusCard { get; set; }

        public List<Borrow> borrows { get; set; } = new();
    }
}
