using Microsoft.EntityFrameworkCore;
using Project_Razor_Temp.Models;

namespace Project_Razor_Temp.Data
{
    public class ApplicationDBContext: DbContext  // Inheritance from DbContext
    {
       public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {

        }

        //DbSet properties. Before to use it, import the name space by using Project_Razor_Temp.Models
        public DbSet<Category> Categories { get; set; }


        // Method for creating Entities according to the Model
        protected override void OnModelCreating(ModelBuilder mb) 
        {
                 mb.Entity<Category>().HasData(

                 new Category { Id = 1, Name = "Action", DisplayOrder = 1 }
                   );
        }
    }
}
