using System;
using System.Collections.Generic;

namespace BookMyMovieDataAccessLayer.Models;

public partial class Theater
{
    public int TheaterId { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int TotalSeats { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
