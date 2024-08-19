using ETickets.Models;
using ETickets.ViewModels;

namespace ETickets.Repositry.IRepositry
{
    public interface IActorRepositry
    {
        List<Actor> GetActors();
        Actor GetActor(int Id);
        void DeleteActor(Actor actor);
        void UpdateActor(Actor actor);
        void CreateActor(Actor actor);
        Actor GetActorWithMovie(int id);
        void Save();
    }
}
