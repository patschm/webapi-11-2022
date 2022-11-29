using Microsoft.AspNetCore.Mvc;
using ProductReviews.DAL.EntityFramework.Entities;
using ProductReviews.Interfaces;
using ProductReviews.Models;

namespace ProductApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BrandController : ControllerBase
{
    private readonly ILogger<BrandController> _logger;
    private readonly IUnitOfWork _uow;

    public BrandController(IUnitOfWork uow, ILogger<BrandController> logger)
    {
        _logger = logger;
        _uow = uow;
    }

    [HttpGet(Name = "GetBrands")]
    public async Task<IEnumerable<BrandModel>> Get(int page = 1, int count = 10)
    {

        var brands = await _uow.BrandRepository.GetAsync(page, count);
        return brands.Select(b => b.ToModel()).ToList();
    }
}
