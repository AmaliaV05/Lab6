using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project2.Models;

namespace Project2.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly string _userName;
        public ApplicationDbContext(
            DbContextOptions options,
            IHttpContextAccessor httpContextAccessor,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
            //_userName = httpContextAccessor.HttpContext.User.Identity.Name;
            _httpContextAccessor = httpContextAccessor;
            //_userID = userManager.GetUserId(httpContext.HttpContext.User);

        }
        public DbSet<Film> Films { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Film>()
                .HasIndex(p => p.Title)
                .IsUnique()
                .HasFilter(null);
        }
    }
}
