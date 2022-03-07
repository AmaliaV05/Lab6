/*using NUnit.Framework;
using System;
using Project2.Models;
using Project2.Data;
using Project2.Services;
using Project2.ViewModels.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Tests
{
    class Test_AuthManagementService
    {
        private ApplicationDbContext _context;
        private SignInManager<ApplicationUser> _signInManager;
        private IConfiguration _configuration;
        private UserManager<ApplicationUser> _userManager;

        public Test_AuthManagementService(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
        }

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("In setup.");
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            _context = new ApplicationDbContext(options, new OperationalStoreOptionsForTests());

            _context.ApplicationUsers.Add(new ApplicationUser { 
                UserName = "test@test.com", 
                NormalizedUserName = "TEST@TEST.COM",
                Email = "test@test.com", 
                NormalizedEmail = "TEST@TEST.COM",
                EmailConfirmed = true, 
                PasswordHash = "AQAAAAEAACcQAAAAEEykXcOLtZ7nrVsF0R3kNkG3KQbSKDWxA7cGSsQilLOs2PJWX+vrf7TPhruvJXHQfw==", 
                SecurityStamp = "ICQVFQNDMTGMBZWLTFZAPOAEDR45KZCZ", 
                ConcurrencyStamp = "9fcc0a85-5b97-45e1-8217-c29ef26fcfc9" }
                );
                _context.SaveChanges();
        }

        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("In teardown");

            foreach (var user in _context.ApplicationUsers)
            {
                _context.Remove(user);
            }
            _context.SaveChanges();
        }

        [Test]
        public void TestRegisterUser()//NU MERGE
        {
            var service = new AuthManagementService(_userManager, _signInManager, _context, _configuration);

            var registerRequest = new RegisterRequest("Ana@Ana.ro", "Ana@Ana.ro", "Ana@Ana.ro");
            var registerResponse = service.RegisterUser(registerRequest);

            var ok = new RegisterResponse();

            Assert.AreEqual(ok, registerResponse);
            Assert.AreEqual(1, _context.ApplicationUsers.ToList().Count);
        }
        
        [Test]
        public void TestConfirmUserRequest()//NU MERGE
        {
            var service = new AuthManagementService(_userManager, _signInManager, _context, _configuration);

            var registerRequest = new RegisterRequest("Ana@Ana.ro", "Ana@Ana.ro", "Ana@Ana.ro");
            _ = service.RegisterUser(registerRequest);
            var newConfirmation = new ConfirmUserRequest("Ana@Ana.ro", "");
            _ = service.ConfirmUserRequest(newConfirmation);

            Assert.AreEqual(1, _context.ApplicationUsers.ToList().Count);
        }
    }
}
*/