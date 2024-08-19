using ETickets.Data;
using ETickets.ViewModels;
using ETickets.Models;

using ETickets.Repositry.IRepositry;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repositry
{
    public class MovieRepositry : IMovieRepositry
    {
        private readonly ApplicationDbContext context;

        public MovieRepositry(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task CreateMovie(Movie movie)
        {
            await context.Movies.AddAsync(movie);
            await context.SaveChangesAsync();
        }

        public void DeleteMovie(Movie Movie)
        {
            context.Movies.Remove(Movie);
        }

        public Movie GetMovie(int id)
        {
            var movie = context.Movies
                .Find(id);

            return movie;
        }

        public List<Movie> GetMovies()
        {
            var movies = context.Movies
                .ToList();

            return movies;
        }

        public void UpdateMovie(Movie movie)
        {
            context.Movies.Update(movie);
        }
        public Movie GetMovieWithActors(int id)
        {
            var movie = context.Movies
                .Include(c => c.Cinema)
                .Include(c => c.Category)
                .Include(m => m.ActorMovies)
                    .ThenInclude(am => am.Actor)
                .Where(m => m.Id == id)
                .FirstOrDefault();
            return movie;
        }
        public IEnumerable<Movie> SearchMovies(string name)
        {
            var movies = context.Movies
                .Include(m => m.Cinema)
                .Include(m => m.Category)
                .Where(m => m.Name.Contains(name));

            return movies;
        }
        public List<Movie> GetMoviesWithCategoryCinema()
        {
            var movies = context.Movies
                .Include(m => m.Category)
                .Include(m => m.Cinema)
                .ToList();

            return movies;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
