using cinemasecondtask.Data;
using cinemasecondtask.Models;
using cinemasecondtask.Repository;
using Microsoft.EntityFrameworkCore;

namespace cinemasecondtask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connectionString =
           builder.Configuration.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string"
           + "'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options=>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<IRepository<Category>, Repositories<Category>>();
            builder.Services.AddScoped<IRepository<Movie>, Repositories<Movie>>();
            builder.Services.AddScoped<IRepository<Cinema>, Repositories<Cinema>>();
            builder.Services.AddScoped<IRepository<Actors>, Repositories<Actors>>();
            builder.Services.AddScoped<IRepository<Book>, Repositories<Book>>();
            builder.Services.AddScoped<IRepository<User>, Repositories<User>>();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Admin}/{controller=Category}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
