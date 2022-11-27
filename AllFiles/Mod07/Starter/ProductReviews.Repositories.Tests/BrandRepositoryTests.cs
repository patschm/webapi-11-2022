using Xunit;
using ProductReviews.Repositories.EntityFramework;
using System.Threading.Tasks;
using ProductReviews.DAL.EntityFramework.Entities;


namespace ProductReviews.Repositories.Tests;

public class BrandRepositoryTests : TestBase
{   
    [Fact]
    public async Task TestPagingAsync()
    {
        var repo = new BrandRepository(CreateContext());
        var result = await repo.GetAsync(1, 10);

        Assert.NotNull(result);
        Assert.True(result.Count == 10);
    }
    [Fact]
    public async Task TestGetByIdAsync()
    {
        var repo = new BrandRepository(CreateContext());
        var result = await repo.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.IsType<Brand>(result);
    }

    [Fact]
    public async Task TestInsertAsync()
    {
        var repo = new BrandRepository(CreateContext());
        var tmp = new Brand { Name = "Test"};
        var result = await repo.AddAsync(tmp);

        Assert.NotNull(result);
        Assert.True(result.Id > 0);
    }
    [Fact]
    public async Task TestUpdateAsync()
    {
        var repo = new BrandRepository(CreateContext());
        var tmp = await repo.GetByIdAsync(1);
        tmp.Name = "Test";
        var result = await repo.UpdateAsync(tmp);

        Assert.NotNull(result);
        Assert.True(result.Name == "Test");
    }
    [Fact]
    public async Task TestDeleteAsync()
    {
        var repo = new BrandRepository(CreateContext());
        await repo.DeleteAsync(1);
        var result = await repo.GetByIdAsync(1);

        Assert.Null(result);
    }
    [Fact]
    public async Task TestProductsAsync()
    {
        var repo = new BrandRepository(CreateContext());
        var tmp = await repo.GetByIdAsync(1);
        var result = await repo.GetProductsAsync(tmp.Id, 1, 5);

        Assert.NotNull(result);
        Assert.True(result.Count == 5);
    }

}