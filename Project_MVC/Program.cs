using Microsoft.EntityFrameworkCore;
using Project.DataAccess.Data;
using Project.DataAccess.Repository.IRepository;
using Project.DataAccess.Repository;

namespace Project_MVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the dependency injection container.
			builder.Services.AddControllersWithViews();
			// After creating the ApplicationDbContex.cs class in the Data folder, here we set up the connection string with the provided Service
			builder.Services.AddDbContext<ApplicationDbContext>
				(
					options=> options.UseSqlServer
						(
						builder.Configuration.GetConnectionString("DefaultConnection") // Check DefaultConnection in the appsettings.json file
						)
				);
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}