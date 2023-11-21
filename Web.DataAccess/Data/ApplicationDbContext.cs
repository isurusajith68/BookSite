using Microsoft.EntityFrameworkCore;
using ZeroToHero.Models.Models;
namespace ZeroToHero.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        { 
            
        
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                    new Category
                    {
                        Id = 1,
                        DisplayOrder = 0,
                        Name = "History"

                    },
                    new Category
                    {
                        Id = 2,
                        DisplayOrder = 1,
                        Name = "Action"

                    },
                    new Category
                    {
                        Id = 3,
                        DisplayOrder = 2,
                        Name = "ScriFi"

                    }


                );
        }

    }
}
