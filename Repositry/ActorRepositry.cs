using ETickets.Data;
using ETickets.Models;
using ETickets.Repositry.IRepositry;
using ETickets.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repositry
{
    public class ActorRepositry : IActorRepositry
    {
        private readonly ApplicationDbContext context;

        public ActorRepositry(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void CreateActor(Actor actor)
        {
            context.Actors.Add(actor);
        }

        public void DeleteActor(Actor actor)
        {
            context.Actors.Remove(actor);
        }

        public Actor GetActor(int Id)
        {
            var actor = context.Actors
                .Find(Id);
            return actor;
        }

        public List<Actor> GetActors()
        {
            var actors = context.Actors
                .ToList();
            return actors;
        }

        public void UpdateActor(Actor actor)
        {
            context.Actors.Update(actor);
        }

        public Actor GetActorWithMovie(int id)
        {
            var actor = context.Actors
                .Include(a => a.ActorMovies)
                    .ThenInclude(am => am.Movie)
                .Where(a => a.Id == id)
                .FirstOrDefault();

            return actor;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
