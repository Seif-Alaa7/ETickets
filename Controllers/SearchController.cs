using ETickets.Repositry;
using ETickets.Repositry.IRepositry;
using ETickets.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ETickets.Controllers
{
    public class SearchController : Controller
    {
        private readonly IMovieRepositry MovieRepositry;
        public SearchController(IMovieRepositry MovieRepositry)
        {
            this.MovieRepositry = MovieRepositry;
        }
        public IActionResult Index(string Name)
        {
            var searchResults = MovieRepositry.SearchMovies(Name);
            var searchResultsVM = searchResults.Select(m => new MovieVM
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                Price = m.Price,
                ImgUrl = m.ImgUrl,
                TrailerUrl = m.TrailerUrl,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                MovieStatus = m.MovieStatus,
                CinemaId = m.CinemaId,
                CinemaName = m.Cinema.Name,
                CategoryId = m.CategoryId,
                CategoryName = m.Category.Name
            });
            return View(searchResultsVM);
        }
    }
}
