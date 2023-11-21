using CrudRazor.Model;
using Microsoft.EntityFrameworkCore;

namespace CrudRazor.Data
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> option) : base(option)
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
