using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZooShopApp.Core.Contracts;

namespace ZooShopApp.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        public IActionResult Add(int productId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _favoriteService.Add(userId, productId);

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Remove(int productId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _favoriteService.Remove(userId, productId);

            return RedirectToAction("Index", "Product");
        }

        public IActionResult MyFavorites()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var products = _favoriteService.GetUserFavorites(userId);

            return View(products);
        }
    }
}
