using ETickets.Data;
using ETickets.Repositry;
using ETickets.Repositry.IRepositry;
using Microsoft.EntityFrameworkCore;

namespace ETickets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

            builder.Services.AddScoped<IActorRepositry, ActorRepositry>();
            builder.Services.AddScoped<ICategoryRepositry, CategoryRepositry>();
            builder.Services.AddScoped<ICinemaRepositry, CinemaRepositry>();
            builder.Services.AddScoped<IMovieRepositry, MovieRepositry>();
            builder.Services.AddScoped<IShoppingCartRepositry, ShoppingCartRepositry>();



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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
