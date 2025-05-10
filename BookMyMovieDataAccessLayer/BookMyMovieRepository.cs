using BookMyMovieDataAccessLayer.Models;

namespace BookMyMovieDataAccessLayer
{
    public class BookMyMovieRepository : IBookMyMovieRepository
    {
        public BookMyMovieDbContext Context { get; set; }

        public BookMyMovieRepository()
        {
            Context = new BookMyMovieDbContext();
        }
        public List<Booking> GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();
            try
            {
                bookings = Context.Bookings
                    .Where(b => b.SeatsBooked >= 3)
                    .OrderBy(b => b.TheaterId)
                    .Select(b => b)
                    .ToList();
            }
            catch (Exception ex)
            {
                bookings = null;
            }
            return bookings;
        }

        public User GetUserDetails(int userId)
        {
            User user = new User();
            try
            {
                user = Context.Users.Find(userId);
            }
            catch (Exception ex)
            {
                user = null;
            }
            return user;
        }

        public bool AddMovie(Movie movie)
        {
            bool result = false;
            try
            {
                Context.Movies.Add(movie);
                Context.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool AddTheatre(Theater theater)
        {
            bool result = false;
            try
            {
                Context.Theaters.Add(theater);
                Context.SaveChanges(); 
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public int UpdateUserDetails(User user)
        {
            int result = 0;
            try
            {
                User obj = Context.Users.Find(user.UserId);
                if (obj == null)
                {
                    return -1;
                }
                else
                {
                    obj.Email = user.Email;
                    obj.ContactNumber = user.ContactNumber;
                    Context.SaveChanges();
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                result = -99;
            }
            return result;
        }

        public int CancelBooking(int bookingId)
        {
            int result = 0;
            try
            {
                Booking booking = Context.Bookings.Find(bookingId);
                if (booking == null)
                    return -1;
                else
                {
                    Context.Bookings.Remove(booking);
                    Context.SaveChanges();
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                result = -99;
            }
            return result;
        }

        public bool DeleteUser(int userId) 
        {
            bool result = false;
            try
            {
                Booking booking = Context.Bookings
                    .Where(b => b.UserId == userId)
                    .FirstOrDefault();
                if (booking == null)
                {
                    User user = Context.Users.Find(userId);
                    if(user != null)
                    {
                        Context.Users.Remove(user);
                        Context.SaveChanges();
                        result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
