using ETickets.Data.Enum;
using ETickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using ETickets.ViewModels;

namespace ETickets.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ShoppingCart> shoppings { get; set; }

  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>()
                .ToTable("Movies");
            modelBuilder.Entity<Category>()
                .ToTable("Categories");
            modelBuilder.Entity<Cinema>()
                .ToTable("Cinemas");
            modelBuilder.Entity<Actor>()
                .ToTable("Actors");
            modelBuilder.Entity<ActorMovie>()
                .ToTable("ActorMovies");

            modelBuilder.Entity<Movie>(
                    eb =>
                    {
                        eb.HasKey(e => e.Id);
                        /*eb.Property(e => e.Id).ValueGeneratedOnAdd();
                        eb.Property(b => b.Name).HasColumnType("Nvarchar(50)").IsRequired();
                        eb.Property(b => b.Description).HasColumnType("Nvarchar(500)");
                        eb.Property(b => b.Price).HasColumnType("decimal(5, 2)").IsRequired();*/
                        eb.HasOne(a => a.Cinema)
                            .WithMany(c => c.Movies)
                            .HasForeignKey(a => a.CinemaId);
                        eb.HasOne(a => a.Category)
                            .WithMany(c => c.Movies)
                            .HasForeignKey(a => a.CategoryId);
                        eb.HasMany(a => a.Actors)
                            .WithMany(c => c.Movies);
                    });
            /*modelBuilder.Entity<Category>(
                    eb =>
                    {
                        eb.HasKey(e => e.Id);
                        eb.Property(e => e.Id).ValueGeneratedOnAdd();
                        eb.Property(b => b.Name).HasColumnType("Nvarchar(50)").IsRequired();
                    });*/

            /*modelBuilder.Entity<Cinema>(
                    eb =>
                    {
                        eb.HasKey(e => e.Id);
                        eb.Property(e => e.Id).ValueGeneratedOnAdd();
                        eb.Property(b => b.Name).HasColumnType("Nvarchar(50)").IsRequired();
                        eb.Property(b => b.Description).HasColumnType("Nvarchar(500)");
                        eb.Property(b => b.Address).HasColumnType("Nvarchar(100)").IsRequired();
                    });*/

            modelBuilder.Entity<Actor>(
                eb =>
                {
                    eb.HasKey(e => e.Id);
                    /*eb.Property(e => e.Id).ValueGeneratedOnAdd();
                    eb.Property(b => b.FirstName).HasColumnType("Nvarchar(50)").IsRequired();
                    eb.Property(b => b.LastName).HasColumnType("Nvarchar(50)").IsRequired();
                    eb.Property(b => b.Bio).HasColumnType("Nvarchar(500)").IsRequired();
                    eb.Property(b => b.News).HasColumnType("Nvarchar(2000)");*/
                    eb.HasMany(a => a.Movies)
                            .WithMany(c => c.Actors);
                });
            modelBuilder.Entity<Movie>()
                .HasMany(e => e.Actors)
                .WithMany(e => e.Movies)
                .UsingEntity<ActorMovie>(
                o => o.HasOne(a => a.Actor).WithMany(b => b.ActorMovies).HasForeignKey(c => c.ActorId).HasPrincipalKey(d => d.Id),
                o => o.HasOne(a => a.Movie).WithMany(b => b.ActorMovies).HasForeignKey(c => c.MovieId).HasPrincipalKey(d => d.Id),
                o =>
                {
                    o.HasKey(e => new { e.ActorId, e.MovieId });
                });
        }
    }
}
