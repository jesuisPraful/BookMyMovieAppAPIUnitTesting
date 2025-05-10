using BookMyMovieDataAccessLayer;
using BookMyMovieDataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BookMyMovieWebServices.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookMyMovieController : Controller
    {
        IBookMyMovieRepository repository;
        public BookMyMovieController(IBookMyMovieRepository repo)
        {
            repository = repo;
        }


        [HttpGet]
        public JsonResult GetAllBookings()
        {
            List<Booking> bookings = new List<Booking>();
            try
            {
                bookings = repository.GetAllBookings();
            }
            catch (Exception ex)
            {
                bookings = null;
            }
            return Json(bookings);
        }

        [HttpGet]
        public IActionResult GetUserDetails(int userId)
        {
            User user = new User();
            try
            {
                if(userId <= 0)
                {
                    return BadRequest("User Id must be greater than 0!");
                }
                user = repository.GetUserDetails(userId);
                if(user == null)
                {
                    return NotFound("User not found!");
                }
                else
                {
                    return Ok(user);    //OkObjectResult return.
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public JsonResult AddMovie(Models.Movie movie)
        {
            bool status = false;
            try
            {
                if(ModelState.IsValid)
                {
                    Movie movieObj = new Movie();
                    movieObj.MovieId = movie.MovieId;
                    movieObj.Title = movie.Title;
                    movieObj.Duration = movie.Duration;
                    movieObj.ReleaseDate = movie.ReleaseDate;
                    movieObj.Genre = movie.Genre;

                    status = repository.AddMovie(movieObj);
                }
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(status);
        }

        [HttpPost]
        public IActionResult AddTheatre(Models.Theater theater)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Theater theaterObj = new Theater();
                    theaterObj.TheaterId = theater.TheaterId;
                    theaterObj.Name = theater.Name;
                    theaterObj.Location = theater.Location;
                    theaterObj.TotalSeats = theater.TotalSeats;
                    bool status = repository.AddTheatre(theaterObj);
                    if (status)
                        return Ok(status);
                    else
                        return BadRequest("Theatre Details could not be added!");
                }
                else
                {
                    return BadRequest("Input not in proper format!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public JsonResult UpdateUserDetails(Models.UserDetails user)
        {
            int result = 0;
            try
            {
                if(ModelState.IsValid)
                {
                    User userObj = new User();
                    userObj.UserId = user.UserId;
                    userObj.Email = user.Email;
                    userObj.ContactNumber = user.ContactNumber;

                    result = repository.UpdateUserDetails(userObj);
                }
            }
            catch (Exception ex)
            {
                result = -99;
            }
            return Json(result);
        }

        [HttpDelete]
        public IActionResult CancelBooking(int bookingId)
        {
            try
            {
                int status = repository.CancelBooking(bookingId);
                if (status == 1)
                    return Ok("Booking cancelled successfully!");
                else if (status == -1)
                    return NotFound("Booking not found!");
                else
                    return BadRequest("Booking could not be cancelled!");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        public JsonResult DeleteUser(int userId)
        {
            bool status = false;
            try
            {
                status = repository.DeleteUser(userId);
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(status);
        }
    }
}
