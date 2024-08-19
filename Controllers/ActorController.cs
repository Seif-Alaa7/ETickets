using ETickets.Models;
using ETickets.Repositry;
using ETickets.Repositry.IRepositry;
using ETickets.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ETickets.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorRepositry ActorRepositry;

        public ActorController(IActorRepositry ActorRepositry)
        {
            this.ActorRepositry = ActorRepositry;
        }
        public IActionResult Index(int id)
        {
            var Actor = ActorRepositry.GetActorWithMovie(id);
            var actorVM = new ActorVM
            {
                Id = id,
                FirstName = Actor.FirstName,
                LastName = Actor.LastName,
                Bio = Actor.Bio,
                ProfilePicture = Actor.ProfilePicture,
                News = Actor.News,
                MoviesName = Actor.ActorMovies.Select(n => n.Movie.Name).ToList()
            };

            return View(actorVM);
        }

    }
}
