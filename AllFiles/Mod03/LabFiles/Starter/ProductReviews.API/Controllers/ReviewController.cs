using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductReviews.Interfaces;
using ProductReviews.DAL.EntityFramework.Entities;

namespace ProductReviews.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _repository;

        public ReviewController(IReviewRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ICollection<Review>> Get(int page = 1, int count = 10)
        {
            return await _repository.GetAsync(page, count);
        }
        [HttpGet("{id}")]
        public async Task<Review> Get(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Review review)
        {
            var result  = await _repository.AddAsync(review);
            return CreatedAtAction(nameof(Get), new { id= result.Id});
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Review review)
        {
            review.Id = id;
            var result = await _repository.UpdateAsync(review);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return Accepted();
        }
    }
}