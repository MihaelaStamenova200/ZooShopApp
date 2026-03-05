using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZooShopApp.Core.Contracts;
using ZooShopApp.Infrastructure.Data;
using ZooShopApp.Infrastructure.Data.Domain;

namespace ZooShopApp.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;

        }

        public Category GetCategoryById(int categoryId)
        {
            return _context.Categories.Find(categoryId);

        }

        public List<Category> GetCategories()
        {
            List<Category> brands = _context.Categories.ToList();
            return brands;
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _context.Products
                .Where(x => x.CategoryId == categoryId)
                .ToList();
        }
    }
}
