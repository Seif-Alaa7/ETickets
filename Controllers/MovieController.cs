using ETickets.Data.Enum;
using ETickets.Models;
using ETickets.Repositry;
using ETickets.Repositry.IRepositry;
using ETickets.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepositry movieRepositry;
        private readonly ICategoryRepositry categoryRepositry;
        private readonly ICinemaRepositry cinemaRepositry;

        public MovieController(IMovieRepositry MovieRepositry , ICategoryRepositry CategoryRepositry , ICinemaRepositry CinemaRepositry )
        {
            this.movieRepositry = MovieRepositry;
            this.categoryRepositry = CategoryRepositry;
            this.cinemaRepositry = CinemaRepositry;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            var movie = movieRepositry.GetMovieWithActors(id);
            movie.ViewCount += 1;
            movieRepositry.UpdateMovie(movie);
            movieRepositry.Save();
            var movieVM = new MovieVM
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImgUrl = movie.ImgUrl,
                TrailerUrl = movie.TrailerUrl,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieStatus = movie.MovieStatus,
                CinemaId = movie.CinemaId,
                CinemaName = movie.Cinema.Name,
                CategoryId = movie.CategoryId,
                CategoryName = movie.Category.Name,
                ViewCount = movie.ViewCount,
                Actors = movie.Actors.Select(a => new ActorVM
                {
                    Id = a.Id,
                    Bio = a.Bio,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    News = a.News,
                    ProfilePicture = a.ProfilePicture,
                    MoviesName = a.ActorMovies.Select(n => n.Movie.Name).ToList()
                }).ToList()
            };
            return View(movieVM);
        }

        public async Task<IActionResult> CreateMovie()
        {
            var movieVM = new MovieVM();
            ViewBag.Categories = new SelectList(categoryRepositry.GetCategories(), "Id", "Name");
            ViewBag.Cinemas = new SelectList(cinemaRepositry.GetCinemas(), "Id", "Name");
            return View(movieVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieVM movieVM)
        {
            /*if (ModelState.IsValid)
            {*/
                var movie = new Movie
                {
                    Id = movieVM.Id,
                    Name = movieVM.Name,
                    Description = movieVM.Description,
                    Price = movieVM.Price,
                    ImgUrl = movieVM.ImgUrl,
                    TrailerUrl = movieVM.TrailerUrl,
                    StartDate = movieVM.StartDate,
                    EndDate = movieVM.EndDate,
                    MovieStatus = movieVM.MovieStatus,
                    CinemaId = movieVM.CinemaId,
                    CategoryId = movieVM.CategoryId
                };
                await movieRepositry.CreateMovie(movie);
                return RedirectToAction("Index", "Home");
        /*  }
        var categories = categoryRepositry.GetCategories();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");*/
        /*return View("CreateMovie", movieVM);*/
        }
        public IActionResult Edit(int id)
        {
            var movie = movieRepositry.GetMovie(id);
            ViewBag.MovieStatuses = new SelectList(Enum.GetValues(typeof(MovieStatus)).Cast<MovieStatus>());
            var movieVM = new MovieVM
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImgUrl = movie.ImgUrl,
                TrailerUrl = movie.TrailerUrl,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieStatus = movie.MovieStatus,
                CinemaId = movie.CinemaId,
                CategoryId = movie.CategoryId
            };

            var categories = categoryRepositry.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", movieVM.CategoryId);

            return View(movieVM);
        }
        [HttpPost]
        public IActionResult SaveEdit(MovieVM movieVM)
        {
            /*if (ModelState.IsValid)
            {*/
                var movie = movieRepositry.GetMovie(movieVM.Id);

                movie.Name = movieVM.Name;
                movie.Description = movieVM.Description;
                movie.Price = movieVM.Price;
                movie.ImgUrl = movieVM.ImgUrl;
                movie.TrailerUrl = movieVM.TrailerUrl;
                movie.StartDate = movieVM.StartDate;
                movie.EndDate = movieVM.EndDate;
                movie.MovieStatus = movieVM.MovieStatus;
                movie.CinemaId = movieVM.CinemaId;
                movie.CategoryId = movieVM.CategoryId;

                movie.Actors = movieVM.Actors.Select(a => new Actor
                {
                    Id = a.Id,
                    News = a.News,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Bio = a.Bio,
                    ProfilePicture = a.ProfilePicture
                }).ToList();

                movieRepositry.UpdateMovie(movie);
                movieRepositry.Save();

                return RedirectToAction("Index", "Movies");
            /*}

            var categories = categoryRepositry.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", movieVM.CategoryId);

            return View("Edit", movieVM);*/
        }
        /*public IActionResult BookAppointment(int id)
        {
            var movie = movieRepositry.GetMovie(id);
            shoppingCartRepository.AddToCart(movie);
            TempData["Message"] = "Add ticket to cart success";
            return RedirectToAction("index");
        }*/
    }
}
