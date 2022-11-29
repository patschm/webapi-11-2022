using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviews.API.Models
{
    public class ProductGroupModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
    }
}