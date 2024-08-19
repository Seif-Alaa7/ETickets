using ETickets.Models;
using ETickets.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ETickets.Repositry.IRepositry
{
    public interface IMovieRepositry
    {
        List<Movie> GetMovies();
        Movie GetMovie(int Id);
        void DeleteMovie(Movie Movie);
        void UpdateMovie(Movie Movie);
        Task CreateMovie(Movie movie);
        Movie GetMovieWithActors(int id);
        IEnumerable<Movie> SearchMovies(string Name);
        public List<Movie> GetMoviesWithCategoryCinema();
        void Save();
    }
}
