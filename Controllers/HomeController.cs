using ETickets.Data;
using ETickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ETickets.Repositry;
using ETickets.Repositry.IRepositry;
using ETickets.ViewModels;

namespace ETickets.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieRepositry movieRepositry;

        public HomeController(ILogger<HomeController> logger, IMovieRepositry movieRepositry)
        {
            _logger = logger;
            this.movieRepositry = movieRepositry;
        }

        public IActionResult Index()
        {
            var movies = movieRepositry.GetMoviesWithCategoryCinema();
            var moviesVM = movies.Select(m => new MovieVM
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
                CategoryName = m.Category.Name,
                CategoryId = m.CategoryId
            }).ToList();
            return View(moviesVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
