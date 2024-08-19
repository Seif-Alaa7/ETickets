using ETickets.Models;
using System.ComponentModel.DataAnnotations;

namespace ETickets.ViewModels
{
    public class CinemaVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string CinemaLogo { get; set; }
        [Required]
        public string Address { get; set; }
        public List<MovieVM> Movies { get; set; }
    }
}
