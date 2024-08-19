using ETickets.Models;

namespace ETickets.Repositry.IRepositry
{
    public interface IShoppingCartRepositry
    {
        List<ShoppingCart> GetAll();
        ShoppingCart GetOne(int Id);
        void Delete(ShoppingCart shoppingCart);
        void Update(ShoppingCart shoppingCart);
        void Create(ShoppingCart shoppingCart);
        void AddToCart(Movie movie, int quantity);
        void RemoveFromCart(Movie movie);
        List<ShoppingCart> GetShoppingCartItems();
        double GetShoppingCartTotal();
        void ClearCart();
        void Save();
    }
}
