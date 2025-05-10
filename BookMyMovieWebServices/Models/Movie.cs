using System.ComponentModel.DataAnnotations;

namespace BookMyMovieWebServices.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Duration { get; set; }
        [Required]
        public DateOnly ReleaseDate { get; set; }
    }
}
