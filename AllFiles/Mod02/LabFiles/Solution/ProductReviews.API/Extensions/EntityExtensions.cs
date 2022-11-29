using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductReviews.API.Models;
using ProductReviews.DAL.EntityFramework.Entities;

namespace ProductReviews.API.Extensions
{
    public static class EntityExtensions
    {
        public static ProductGroupModel ToModel(this ProductGroup d)
        {
            return new ProductGroupModel {Id = d.Id, Name=d.Name, Image = d.Image};
        }
    }
}