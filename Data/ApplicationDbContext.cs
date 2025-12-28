using HygreWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HygreWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SiteImage> SiteImages { get; set; }
        public DbSet<TrainingWeek> TrainingWeeks { get; set; }
        public DbSet<TrainingBooking> TrainingBookings { get; set; }

    }
}
