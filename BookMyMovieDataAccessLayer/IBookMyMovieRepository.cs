using BookMyMovieDataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyMovieDataAccessLayer
{
    public interface IBookMyMovieRepository
    {
        List<Booking> GetAllBookings();
        User GetUserDetails(int userId);
        public bool AddMovie(Movie movie);
        public bool AddTheatre(Theater theater);
        public int UpdateUserDetails(User user);
        public int CancelBooking(int bookingId);
        public bool DeleteUser(int userId);

    }
}
