using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooShopApp.Infrastructure.Data.Domain;

namespace ZooShopApp.Core.Contracts
{
    public interface IFavoriteService
    {
        void Add(string userId, int productId);
        void Remove(string userId, int productId);
        bool IsFavorite(string userId, int productId);
        List<Product> GetUserFavorites(string userId);
    }
}
