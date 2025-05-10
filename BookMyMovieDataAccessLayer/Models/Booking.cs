using System;
using System.Collections.Generic;

namespace BookMyMovieDataAccessLayer.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? MovieId { get; set; }

    public int? TheaterId { get; set; }

    public DateTime? BookingDate { get; set; }

    public int SeatsBooked { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual Theater? Theater { get; set; }

    public virtual User? User { get; set; }
}
