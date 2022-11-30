using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviews.Models
{
    public class ReviewModel
    {
       public int Id { get; set; }
        public string? Author { get; set; }
        public string? Email { get; set; }
        public string? Text { get; set; }
        public int Score { get; set; }
        public int ProductId { get; set; }
    }
}