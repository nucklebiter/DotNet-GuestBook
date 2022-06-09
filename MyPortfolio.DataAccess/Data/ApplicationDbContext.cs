using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.Models;

namespace MyPortfolioWeb.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {



        }


        public DbSet<GuestType> GuestTypes { get; set; }

        public DbSet<AlienType> AlienTypes { get; set; }
        public DbSet<GuestBook> GuestBooks { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
