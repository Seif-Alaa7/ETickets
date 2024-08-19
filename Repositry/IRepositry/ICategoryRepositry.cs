using ETickets.Models;
using ETickets.ViewModels;

namespace ETickets.Repositry.IRepositry
{
    public interface ICategoryRepositry
    {
        List<Category> GetCategories();
        Category GetCategory(int Id);
        void DeleteCategory(Category Category);
        void UpdateCategory(Category Category);
        void CreateCategory(Category Category);
        Category GetCategoryWithMovies(int id);
        /*CategoryVM Movies();*/
        /*List<MovieVM> GetAvailableMovies();*/
        void Save();
    }
}
