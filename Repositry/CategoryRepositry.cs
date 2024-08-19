using ETickets.Data;
using ETickets.Models;
using ETickets.Repositry.IRepositry;
using ETickets.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repositry
{
    public class CategoryRepositry : ICategoryRepositry
    {
        private readonly ApplicationDbContext context;

        public CategoryRepositry(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void CreateCategory(Category category)
        {
            context.Categories.Add(category);
        }

        public void DeleteCategory(Category Category)
        {
            context.Categories.Remove(Category);
        }

        public Category GetCategory(int id)
        {
            var category = context.Categories
                .Find(id);

            return category;
        }

        public List<Category> GetCategories()
        {
            var categories = context.Categories
                .ToList();

            return categories;
        }

        public void UpdateCategory(Category category)
        {
            context.Categories.Update(category);
        }

        public Category GetCategoryWithMovies(int id)
        {
            var category = context.Categories
                .Include(c => c.Movies)
                    .ThenInclude(m => m.Cinema)
                .FirstOrDefault(c => c.Id == id);

            return category;
        }
        /*public CategoryVM Movies()
        {
            var categoryVM = new CategoryVM
            {
                AvailableMovies = context.Movies.Select(m => new MovieVM
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList()
            };
            return (categoryVM);
        }*/
        /*public List<MovieVM> GetAvailableMovies()
        {
            var categoryVM = new CategoryVM
            {
                AvailableMovies = context.Movies.Select(m => new MovieVM
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList()
            };
            return (categoryVM.AvailableMovies);
        }*/
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
