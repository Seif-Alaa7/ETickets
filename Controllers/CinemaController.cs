using ETickets.Repositry;
using ETickets.Repositry.IRepositry;
using ETickets.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ETickets.Controllers
{
    public class CinemaController : Controller
    {
        private readonly ICinemaRepositry CinemaRepositry;
        public CinemaController(ICinemaRepositry CinemaRepositry)
        {
            this.CinemaRepositry = CinemaRepositry;
        }

        public IActionResult Index()
        {
            var Cinemas = CinemaRepositry.GetCinemas();
            var CinemasVM = Cinemas.Select(c => new CinemaVM
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Address = c.Address,
                CinemaLogo = c.CinemaLogo
            });
            return View(CinemasVM);
        }
        public IActionResult MoviesByCinema(int id)
        {
            var Cinema = CinemaRepositry.GetCinemasWithMovies(id);
            var cinemaVM = new CinemaVM
            {
                Id = Cinema.Id,
                Name = Cinema.Name,
                Description = Cinema.Description,
                Address = Cinema.Address,
                CinemaLogo = Cinema.CinemaLogo,
                Movies = Cinema.Movies.Select(m => new MovieVM
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
                }).ToList()
            };
            return View(cinemaVM.Movies);
        }
    }
}
