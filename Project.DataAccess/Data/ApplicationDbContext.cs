using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.DataAccess.Data
{
    // Class for the DbContext of Entity Framework
    public class ApplicationDbContext: DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
		// With the following line of code, we SetUp the Table ready for migration, by using entity framework functionality
        public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		// These line of code with the overriding of the method OnModelCreating, provides the possibility to populate the table
		// with actual data
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
				new Category { Id = 2, Name = "Sci-Fi", DisplayOrder = 2 },
				new Category { Id = 3, Name = "Software Development", DisplayOrder = 3 }
				);

			modelBuilder.Entity<Product>().HasData(
					new Product
					{
						Id = 1,
						Author = "J.R. Tolkien",
						Title = "The lord of the Rings",
						ISBN = Guid.NewGuid().ToString(),
						ListPrice = 20.5,
						ListPrice50 = 0,
						ListPrice100 = 0,
						Description = "None",
						CategoryId = 1,
						ImageUrl = ""
					}
			); 
			// After this, remember to proceed with migration ad update!
		}
	}
}
