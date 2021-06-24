using NUnit.Framework;
using System;
using Project2.Models;
using Project2.Data;
using Project2.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Tests
{
    class Test_ReservationService
    {
        private ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("In setup.");
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            _context = new ApplicationDbContext(options, new OperationalStoreOptionsForTests());

            _context.Films.Add(new Film { Title = "p1 test", Description = "fsd ds fsd fsd", Genre = Genre.Action, Duration = "2 h", YearOfRelease = 1222, Director = "", DateAdded = new DateTime(2008, 3, 1, 7, 0, 0), Rating = 5, Watched = "yes" });
            _context.Films.Add(new Film { Title = "p2 test", Description = "dfs sd sd fsd", Genre = Genre.Comedy, Duration = "2 h", YearOfRelease = 1222, Director = "", DateAdded = new DateTime(2020, 3, 1, 7, 0, 0), Rating = 5, Watched = "yes" });
            //List<Film> filmList = new List<Film>({ "p1 test", "fsd ds fsd fsd", Genre.Action, "2 h", 1222, "", new DateTime(2008, 3, 1, 7, 0, 0), 5, "yes" }, ReservationDateTime = new DateTime(2008, 3, 1, 7, 0, 0)});
            //_context.Reservations.Add(new Reservation { Films { }, ReservationDateTime = new DateTime(2008, 3, 1, 7, 0, 0)});
            //_context.Films.Add(new Film { Title = "p2 test", Description = "dfs sd sd fsd", Genre = Genre.Comedy, Duration = "2 h", YearOfRelease = 1222, Director = "", DateAdded = new DateTime(2020, 3, 1, 7, 0, 0), Rating = 5, Watched = "yes" });
            _context.SaveChanges();
        }

        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("In teardown");

            foreach (var film in _context.Films)
            {
                _context.Remove(film);
            }
            _context.SaveChanges();
        }

        [Test]
        public void TestGetAllReservations()
        {
            var service = new ReservationService(_context, _httpContextAccessor);
            Assert.AreEqual(1, service.GetAll().Result);
        }
    }
}
