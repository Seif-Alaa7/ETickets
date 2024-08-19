using ETickets.Models;
using System.ComponentModel.DataAnnotations;

namespace ETickets.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<MovieVM> Movies { get; set; }
    }
}
