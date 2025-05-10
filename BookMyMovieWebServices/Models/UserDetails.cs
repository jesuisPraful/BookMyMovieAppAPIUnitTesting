using System.ComponentModel.DataAnnotations;

namespace BookMyMovieWebServices.Models
{
    public class UserDetails
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string ContactNumber { get; set; }
    }
}
