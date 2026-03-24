using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZooShopApp.Core.Contracts;
using ZooShopApp.Infrastructure.Data.Domain;
using ZooShopApp.Models.Brand;
using ZooShopApp.Models.Category;
using ZooShopApp.Models.Product;

namespace ZooShopApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IBrandService _brandService;

        public ProductController(IProductService productService, ICategoryService categoryService, IBrandService brandService)
        {
            this._productService = productService;
            this._categoryService = categoryService;
            this._brandService = brandService;
        }

        [AllowAnonymous]
        public ActionResult Index(string searchStringCategoryName, string searchStringBrandName)
        {
            List<ProductIndexVM> products = _productService.GetProducts(searchStringCategoryName, searchStringBrandName)
                .Select(product => new ProductIndexVM
                {
                    Id = product.Id,
                    ProductName = product.Name,
                    BrandId = product.BrandId,
                    BrandName = product.Brand.Name,
                    CategoryId = product.CategoryId,
                    CategoryName = product.Category.Name,
                    Picture = product.Picture,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    Discount = product.Discount
                }).ToList();

            return this.View(products);
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            Product item = _productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }

            ProductDetailsVM product = new ProductDetailsVM()
            {
                Id = item.Id,
                ProductName = item.Name,
                BrandId = item.BrandId,
                BrandName = item.Brand.Name,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.Name,
                Picture = item.Picture,
                Description = item.Description,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            };

            return View(product);
        }
        // GET: ProductController/Create
        public ActionResult Create()
        {
            var product = new ProductCreateVM();

            product.Brands = _brandService.GetBrands()
                .Select(x => new BrandPairVM()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            product.Categories = _categoryService.GetCategories()
                .Select(x => new CategoryPairVM()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] ProductCreateVM product)
        {
            if (ModelState.IsValid)
            {
                var createdId = _productService.Create(product.ProductName, product.BrandId,
                    product.CategoryId, product.Picture, product.Description,
                    product.Quantity, product.Price, product.Discount);
                if (createdId)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            ProductEditVM updatedProduct = new ProductEditVM()
            {
                Id = product.Id,
                ProductName = product.Name,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Picture = product.Picture,
                Description = product.Description,
                Quantity = product.Quantity,
                Price = product.Price,
                Discount = product.Discount
            };
            updatedProduct.Brands = _brandService.GetBrands()
                    .Select(b => new BrandPairVM { Id = b.Id, Name = b.Name }).ToList();
            updatedProduct.Categories = _categoryService.GetCategories()
                    .Select(c => new CategoryPairVM { Id = c.Id, Name = c.Name }).ToList();

            return View(updatedProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductEditVM product)
        {
            if (ModelState.IsValid)
            {
                var updated = _productService.Update(id, product.ProductName, product.BrandId,
                    product.CategoryId, product.Picture, product.Description, product.Quantity, product.Price, product.Discount);
                if (updated)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(product);
        }
        public ActionResult Delete(int id)
        {
            Product item = _productService.GetProductById(id);
            if (item == null)
            {
                return NotFound();
            }

            ProductDeleteVM product = new ProductDeleteVM()
            {
                Id = item.Id,
                ProductName = item.Name,
                BrandId = item.BrandId,
                BrandName = item.Brand.Name,
                CategoryId = item.CategoryId,
                CategoryName = item.Category.Name,
                Picture = item.Picture,
                Description = item.Description,
                Quantity = item.Quantity,
                Price = item.Price,
                Discount = item.Discount
            };
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var deleted = _productService.RemoveById(id);

            if (deleted)
            {
                return this.RedirectToAction("Success");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}

