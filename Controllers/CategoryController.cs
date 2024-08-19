using ETickets.Data;
using ETickets.Repositry;
using ETickets.Repositry.IRepositry;
using ETickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ETickets.ViewModels;

namespace ETickets.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepositry CategoryRepositry;

        public CategoryController(ICategoryRepositry CategoryRepositry)
        {
            this.CategoryRepositry = CategoryRepositry;
        }

        public IActionResult Index()
        {

            var Categories = CategoryRepositry.GetCategories();
            var CategoriesVM = Categories.Select(e => new CategoryVM
            {
                Id = e.Id,
                Name = e.Name
            });
            return View(CategoriesVM);
        }
        public IActionResult MoviesByCategory(int id)
        {
            var category = CategoryRepositry.GetCategoryWithMovies(id);
            var categoryVM = new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                Movies = category.Movies.Select(m => new MovieVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    ImgUrl = m.ImgUrl,
                    TrailerUrl = m.TrailerUrl,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    MovieStatus = m.MovieStatus,
                    CinemaId = m.CinemaId,
                    CinemaName = m.Cinema.Name,
                    CategoryName = m.Category.Name,
                    CategoryId = m.CategoryId
                }).ToList()
            };
            return View(categoryVM);
        }
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryVM categoryVM)
        {
            /*if (ModelState.IsValid)
            {*/
                var category = new Category
                {
                    Id= categoryVM.Id,
                    Name = categoryVM.Name
                };
                CategoryRepositry.CreateCategory(category);
                CategoryRepositry.Save();

                return RedirectToAction("Index");
            /*}
            return View("CreateCategory", categoryVM);*/
        }
    }
}
