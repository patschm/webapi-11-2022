using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviews.DAL.EntityFramework.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public int? BrandId { get; set; }
        public int? ProductGroupId { get; set; }
        public Brand? Brand { get; set; }
        public ProductGroup? ProductGroup { get; set; }
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
    }
}