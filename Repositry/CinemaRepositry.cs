using ETickets.Data;
using ETickets.Models;
using ETickets.Repositry.IRepositry;
using ETickets.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repositry
{
    public class CinemaRepositry : ICinemaRepositry
    {
        private readonly ApplicationDbContext context;

        public CinemaRepositry(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void CreateCinema(Cinema cinema)
        {
            context.Cinemas.Add(cinema);
        }

        public void DeleteCinema(Cinema Cinema)
        {
            context.Cinemas.Remove(Cinema);
        }

        public Cinema GetCinema(int id)
        {
            var cinema = context.Cinemas
                .Find(id);

            return cinema;
        }

        public List<Cinema> GetCinemas()
        {
            var cinemas = context.Cinemas
                .ToList();

            return cinemas;
        }

        public void UpdateCinema(Cinema cinema)
        {
            context.Cinemas.Update(cinema);
        }
        public Cinema GetCinemasWithMovies(int id)
        {
            var cinema = context.Cinemas
                .Include(c => c.Movies)
                .ThenInclude(m => m.Category)
                .FirstOrDefault(c => c.Id == id);

            return cinema;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
