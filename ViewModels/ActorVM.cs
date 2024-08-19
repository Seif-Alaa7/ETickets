using ETickets.Models;
using System.ComponentModel.DataAnnotations;

namespace ETickets.ViewModels
{
    public class ActorVM
    {
        
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Bio { get; set; }
        public string ProfilePicture { get; set; }
        public string News { get; set;}
        public List<string> MoviesName { get; set; }
    }
}
