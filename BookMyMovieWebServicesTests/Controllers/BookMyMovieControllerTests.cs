using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookMyMovieWebServices.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using BookMyMovieDataAccessLayer;
using Xunit;
using BookMyMovieDataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;

namespace BookMyMovieWebServices.Controllers.Tests
{

    [TestClass()]
    public class BookMyMovieControllerTests
    {
        private Mock<IBookMyMovieRepository> mockRepository;
        private BookMyMovieController controller;
        public BookMyMovieControllerTests()
        {
            mockRepository = new Mock<IBookMyMovieRepository>();
            controller = new BookMyMovieController(mockRepository.Object);
        }
        [Fact]
        public void TestGetAllBookings_success()
        {
            List<Booking> bookings = new List<Booking>();
            bookings.Add(new Booking() { BookingId = 1 });
            mockRepository.
                 Setup(repo => repo.GetAllBookings())
                 .Returns(bookings);
            //Act means actually calling the function
            var result = controller.GetAllBookings();

            //Assert
            Xunit.Assert.IsType<JsonResult>(result);  //Checks if the result is a type of Json result
            Xunit.Assert.Equal(bookings, result.Value);
        }

        [Fact]
        public void TestGetAllBookings_Exception()
        {
            //Arrange Part
            mockRepository
                .Setup(repo => repo.GetAllBookings())
                .Throws(new Exception());     //For exception using throw

            //Act
            var result = controller.GetAllBookings();
            Xunit.Assert.IsType<JsonResult>(result);
            Xunit.Assert.Null(result.Value);
        }

        [Fact]
        public void TestGetUserDetails_Success()
        {
            //Arrange
            User user = new User { UserId = 1 };
            mockRepository
                .Setup(repo => repo.GetUserDetails(It.IsAny<int>()))
                .Returns(user);

            //Act
            var result = controller.GetUserDetails(1);

            //Assert
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            Xunit.Assert.Equal(user, okResult.Value);
        }

        [Fact]
        public void TestGetUserDetails_Failure()
        {
            //Arange
            int userId = 0;
            //No need to setup the mock as the method is called after badrequest

            //Act
            var result = controller.GetUserDetails(userId);

            var badRequestResult = Xunit.Assert.IsType<BadRequestObjectResult>(result);
            Xunit.Assert.Equal("User Id must be greater than 0!", badRequestResult.Value);

        }

        [Fact]
        public void TestGetUserDetails_NotFound()
        {

            mockRepository.Setup(repo => repo.GetUserDetails(It.IsAny<int>())).Returns((User)null);
            var result = controller.GetUserDetails(1); //Parameter doesn't matter.
            var notFoundResult = Xunit.Assert.IsType<NotFoundObjectResult>(result);
            Xunit.Assert.Equal("User not found!", notFoundResult.Value);

        }
        [Fact]
        public void TestGetUserDetails_Exception()
        {
            mockRepository.Setup(repo => repo.GetUserDetails(It.IsAny<int>())).Throws(new Exception());
            var result = controller.GetUserDetails(1);

            var ObjResult = Xunit.Assert.IsType<ObjectResult>(result);
            Xunit.Assert.Equal(500, ObjResult.StatusCode);
        }

        [Fact]
        public void TestAddMovie_Success()
        {
            bool status = true;

            Models.Movie movie = new Models.Movie { MovieId = 1 };
            mockRepository.Setup(repo => repo.AddMovie(It.IsAny<Movie>())).Returns(status);

            var result = controller.AddMovie(movie);
            Xunit.Assert.IsType<JsonResult>(result);
            Xunit.Assert.Equal(true, result.Value);

        }
        [Fact]
        public void TestAddMovie_Exception()
        {
            bool status = false;
            Models.Movie movie = new Models.Movie { MovieId = 1 };
            mockRepository
                .Setup(repo => repo.AddMovie(It.IsAny<Movie>()))
                .Returns(status);
            var result = controller.AddMovie(movie);
            Xunit.Assert.IsType<JsonResult>(result);
            Xunit.Assert.Equal(false, result.Value);
        }
        [Fact]
        public void TestAddTheatre_Success()
        {

            Models.Theater theater = new Models.Theater { TheaterId = 1 };
            mockRepository
                .Setup(repo => repo.AddTheatre(It.IsAny<Theater>())).Returns(true);
            var result = controller.AddTheatre(theater);
            var OKresult = Xunit.Assert.IsType<OkObjectResult>(result);

            Xunit.Assert.True((bool)OKresult.Value);
        }
        [Fact]



        public void TestAddTheatre_Failure()
        {
            // Arrange
            var theater = new Models.Theater { TheaterId = 1 };
            mockRepository.Setup(repo =>
            repo.AddTheatre(It.IsAny<Theater>())).Returns(false);
            // Act
            var result = controller.AddTheatre(theater);
            // Assert
            var badRequestResult = Xunit.Assert.IsType<BadRequestObjectResult>(result);
            Xunit.Assert.Equal("Theatre Details could not be added!", badRequestResult.Value);

        }
        [Fact]
        public void TestAddTheatre_ModelStateInvalid()
        {
            // Arrange
            Models.Theater theater = new Models.Theater { TheaterId = 1 };

            controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = controller.AddTheatre(theater);
            // Assert
            var badRequestResult = Xunit.Assert.IsType<BadRequestObjectResult>(result);
            Xunit.Assert.Equal("Input not in proper format!", badRequestResult.Value);

        }
        [Fact]

        public void TestAddTheatre_Exception()
        {
            // Arrange
            Models.Theater theater = new Models.Theater { TheaterId = 1 };

            mockRepository.Setup(repo => repo.AddTheatre(It.IsAny<Theater>())).Throws(new Exception());

            // Act
            var result = controller.AddTheatre(theater);
            // Assert
            var ObjResult = Xunit.Assert.IsType<ObjectResult>(result);
            Xunit.Assert.Equal(StatusCodes.Status500InternalServerError, ObjResult.StatusCode);

        }
        [Fact]
        public void TestUpdateUserDetails_Success()
        {
            var user = new Models.UserDetails { UserId = 1, Email = "test@example.com" };
            mockRepository.Setup(repo => repo.UpdateUserDetails(It.IsAny<User>())).Returns(1);

            var result = controller.UpdateUserDetails(user);

            var jsonResult = Xunit.Assert.IsType<JsonResult>(result);
            Xunit.Assert.Equal(1, jsonResult.Value);
        }
        [Fact]
        public void TestUpdateUserDetails_Failure()
        {
            var user = new Models.UserDetails { UserId = 1 };
            mockRepository.Setup(repo => repo.UpdateUserDetails(It.IsAny<User>())).Throws(new Exception());

            var result = controller.UpdateUserDetails(user);
            // Assert
            var jsonResult = Xunit.Assert.IsType<JsonResult>(result);
            Xunit.Assert.Equal(-99, jsonResult.Value);
        }

        [Fact]
        public void CancelBookingTest_Success()
        {
            // Arrange
            mockRepository.Setup(repo =>
            repo.CancelBooking(It.IsAny<int>())).Returns(1);
            // Act
            var result = controller.CancelBooking(1);
            // Assert
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            Xunit.Assert.Equal("Booking cancelled successfully!", okResult.Value);
        }
        [Fact]
        public void CancelBookingTest_Failure()
        {
            mockRepository.Setup(repo =>
            repo.CancelBooking(It.IsAny<int>())).Throws(new Exception());
            var result = controller.CancelBooking(1);
            var statusCodeResult = Xunit.Assert.IsType<ObjectResult>(result);
            Xunit.Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }

         
    }
}
