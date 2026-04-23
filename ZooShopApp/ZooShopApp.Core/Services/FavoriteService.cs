using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooShopApp.Core.Contracts;
using ZooShopApp.Infrastructure.Data.Domain;
using ZooShopApp.Infrastructure.Data;

namespace ZooShopApp.Core.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _context;

        public FavoriteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(string userId, int productId)
        {
            if (_context.FavoriteProducts.Any(x => x.UserId == userId && x.ProductId == productId))
                return;

            _context.FavoriteProducts.Add(new FavoriteProduct
            {
                UserId = userId,
                ProductId = productId
            });

            _context.SaveChanges();
        }

        public void Remove(string userId, int productId)
        {
            var fav = _context.FavoriteProducts
                .FirstOrDefault(x => x.UserId == userId && x.ProductId == productId);

            if (fav != null)
            {
                _context.FavoriteProducts.Remove(fav);
                _context.SaveChanges();
            }
        }

        public bool IsFavorite(string userId, int productId)
        {
            return _context.FavoriteProducts
                .Any(x => x.UserId == userId && x.ProductId == productId);
        }

        public List<Product> GetUserFavorites(string userId)
        {
            return _context.FavoriteProducts
                .Where(x => x.UserId == userId)
                .Select(x => x.Product)
                .ToList();
        }
    }
}
