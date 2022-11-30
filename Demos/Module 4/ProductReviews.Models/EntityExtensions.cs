using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductReviews.DAL.EntityFramework.Entities;

namespace ProductReviews.Models;

public static class EntityExtensions
{
    public static BrandModel ToModel(this Brand entity)
    {
        return new BrandModel { Id = entity.Id, Name = entity.Name };
    }
    public static ProductModel ToModel(this Product entity)
    {
        return new ProductModel { 
            Id = entity.Id, 
            Name = entity.Name, 
            BrandId=entity.BrandId, 
            Image=entity.Image,
             ProductGroupId=entity.ProductGroupId };
    }
     public static ProductGroupModel ToModel(this ProductGroup entity)
    {
        return new ProductGroupModel { 
            Id = entity.Id, 
            Name = entity.Name, 
            Image=entity.Image };
    }
    public static ReviewModel ToModel(this Review entity)
    {
        return new ReviewModel { 
            Id = entity.Id, 
            Author = entity.Author,
            Email=entity.Email,
            ProductId = entity.ProductId,
            Score=entity.Score,
            Text= entity.Text };
    }
}
