using ETickets.Data.Enum;
using ETickets.Repositry;
using ETickets.Repositry.IRepositry;
using Microsoft.AspNetCore.Mvc;

namespace ETickets.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartRepositry shoppingCart;
        private readonly IMovieRepositry movieRepositry;

        public ShoppingCartController(IShoppingCartRepositry shoppingCart , IMovieRepositry movieRepositry)
        {
            this.shoppingCart = shoppingCart;
            this.movieRepositry = movieRepositry;
        }
        public IActionResult Index()
        {
            var items = shoppingCart.GetShoppingCartItems();
            return View(items);
        }
        public IActionResult AddToCart(int id)
        {
            var movie = movieRepositry.GetMovie(id);

            if (movie == null)
            {
                return NotFound("Movie not found.");
            }

            if (movie.MovieStatus == MovieStatus.Expired || movie.MovieStatus == MovieStatus.UpComing)
            {
                TempData["Message"] = "Cannot add ticket to cart for this movie.";
                return RedirectToAction("Index", "Home");
            }

            shoppingCart.AddToCart(movie, 1);
            shoppingCart.Save();
            TempData["Message"] = "Add ticket to cart is success!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var movie = movieRepositry.GetMovie(id);
            if (movie != null)
            {
                shoppingCart.RemoveFromCart(movie);
            }
            shoppingCart.Save();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ClearCart()
        {
            shoppingCart.ClearCart();
            shoppingCart.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Checkout()
        {
            shoppingCart.ClearCart();
            shoppingCart.Save();
            TempData["MessageCheckout"] = "Payment successful !";
            return RedirectToAction("Index" , "Home");
        }
    }
}
