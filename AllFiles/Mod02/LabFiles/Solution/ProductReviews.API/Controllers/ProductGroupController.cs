using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductReviews.Interfaces;
using ProductReviews.DAL.EntityFramework.Entities;
using ProductReviews.API.Models;
using ProductReviews.API.Extensions;

namespace ProductReviews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductGroupController : ControllerBase
    {
        private readonly IProductGroupRepository _repository;

        public ProductGroupController(IProductGroupRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ICollection<ProductGroupModel>> Get(int page = 1, int count = 10)
        {
            var data = await _repository.GetAsync(page, count);
            return data.Select(d=>d.ToModel()).ToList();
        }
        [HttpGet("{id}")]
        public async Task<ProductGroupModel> Get(int id)
        {
            var result =  await _repository.GetByIdAsync(id);
            return result.ToModel();
        }
        [HttpPost]
        public async Task<ProductGroup> Post([FromBody]ProductGroup productGroup)
        {
            return await _repository.AddAsync(productGroup);
        }
        [HttpPut("{id}")]
        public async Task<ProductGroup> Put(int id, [FromBody]ProductGroup productGroup)
        {
            productGroup.Id = id;
            return await _repository.UpdateAsync(productGroup);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }
    }
}