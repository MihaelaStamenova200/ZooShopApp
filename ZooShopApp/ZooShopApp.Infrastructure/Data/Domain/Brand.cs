using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooShopApp.Infrastructure.Data.Domain
{
    public class Brand
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
