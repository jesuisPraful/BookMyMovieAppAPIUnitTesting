using System.ComponentModel.DataAnnotations;

namespace BookMyMovieWebServices.Models
{
    public class Theater
    {
        public int TheaterId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public int TotalSeats { get; set; }
    }
}
