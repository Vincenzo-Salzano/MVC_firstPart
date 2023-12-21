using Microsoft.EntityFrameworkCore;
using Project_Razor_Temp.Models;

namespace Project_Razor_Temp.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Category> Categories {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id=1, Name="Azione", DisplayOrder= 1 }
                );
        }
    }
}
