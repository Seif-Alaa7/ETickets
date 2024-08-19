using ETickets.Models;
using ETickets.ViewModels;

namespace ETickets.Repositry.IRepositry
{
    public interface ICinemaRepositry
    {
        List<Cinema> GetCinemas();
        Cinema GetCinema(int Id);
        void DeleteCinema(Cinema Cinema);
        void UpdateCinema(Cinema Cinema);
        void CreateCinema(Cinema Cinema);
        Cinema GetCinemasWithMovies(int id);
        void Save();
    }
}
