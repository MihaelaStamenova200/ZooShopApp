using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ZooShopApp.Core.Contracts;
using ZooShopApp.Models;
using ZooShopApp.Models.Product;

namespace ZooShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public IActionResult Index()
        {
            
            
                var products = _productService.GetProducts()
                    .Take(3)
                    .Select(p => new ProductIndexVM
                    {
                        Id = p.Id,
                        ProductName = p.Name,
                        Picture = p.Picture,
                        Price = p.Price,
                        Discount = p.Discount
                    })
                    .ToList();

                return View(products);
            
        }

        public IActionResult Contacts()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
