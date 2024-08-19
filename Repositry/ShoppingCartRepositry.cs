using ETickets.Data;
using ETickets.Models;
using ETickets.Repositry.IRepositry;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repositry
{
    public class ShoppingCartRepositry : IShoppingCartRepositry
    {
        private readonly ApplicationDbContext context;

        public ShoppingCartRepositry(ApplicationDbContext context )
        {
            this.context = context;
        }
        public void Create(ShoppingCart shoppingCart)
        {
            context.shoppings.Add(shoppingCart);
        }

        public void Delete(ShoppingCart shoppingCart)
        {
            context.shoppings.Remove(shoppingCart);
        }

        public List<ShoppingCart> GetAll()
        {
            return context.shoppings.ToList();
        }

        public ShoppingCart GetOne(int Id)
        {
            return context.shoppings.Find(Id);
        }

        public void Update(ShoppingCart shoppingCart)
        {
            context.shoppings.Update(shoppingCart);
        }

        public void AddToCart(Movie movie, int quantity)
        {
            var shoppingItem = context.shoppings.SingleOrDefault(
            s => s.Movie.Id == movie.Id);
            if (shoppingItem == null)
            {
                shoppingItem = new ShoppingCart
                {
                    Movie = movie,
                    Quantity = quantity
                };
                Create(shoppingItem);
            }
            else
            {
                shoppingItem.Quantity += quantity;
            }
        }

        public void RemoveFromCart(Movie movie)
        {
            var shoppingItem = context.shoppings.SingleOrDefault(
            s => s.Movie.Id == movie.Id);

            if (shoppingItem != null)
            {
                Delete(shoppingItem);
            }
        }

        public List<ShoppingCart> GetShoppingCartItems()
        {
            return context.shoppings.Include(s => s.Movie).ToList();
        }

        public double GetShoppingCartTotal()
        {
            return context.shoppings.Select(c => c.Movie.Price * c.Quantity).Sum();
        }

        public void ClearCart()
        {
            var cartItems = context.shoppings.ToList();
            context.shoppings.RemoveRange(cartItems);
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}
