using System;
using System.Collections.Generic;

namespace BookMyMovieDataAccessLayer.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public int Duration { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
