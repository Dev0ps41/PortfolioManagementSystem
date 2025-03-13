using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioManagementSystem.Models;

namespace PortfolioManagementSystem.Data
{
    public class PortfolioDbContext : IdentityDbContext<IdentityUser> // Added Identity support
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
            : base(options) { }

        public DbSet<Investment> Investments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Ensure relationships and constraints are configured correctly
            builder.Entity<Investment>()
                .HasOne<IdentityUser>() // Investment belongs to a User
                .WithMany()
                .HasForeignKey(i => i.UserId) // Foreign key to track user ownership
                .OnDelete(DeleteBehavior.Cascade); // Delete investments if the user is deleted
        }
    }
}
