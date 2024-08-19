using ETickets.Data.Enum;
using ETickets.Models;
using ETickets.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ETickets.ViewModels
{
    public class MovieVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the movie name.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter the price.")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 1000.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please enter the image URL.")]
        public string ImgUrl { get; set; }

        [Required(ErrorMessage = "Please enter the trailer URL.")]
        public string TrailerUrl { get; set; }

        [Required(ErrorMessage = "Please select a start date.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please select an end date.")]
        public DateTime EndDate { get; set; }

        public MovieStatus MovieStatus { get; set; }

        [Required(ErrorMessage = "Please select a cinema.")]
        public int CinemaId { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }
        public string CinemaName { get; set; }
        public string CategoryName { get; set; }
        public int ViewCount { get; set; }
        public List<ActorVM> Actors { get; set; }

    }
}
