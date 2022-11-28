using Microsoft.EntityFrameworkCore;

namespace WithEF;

public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Brand> Brands => Set<Brand>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.HasDefaultSchema("Core");

       modelBuilder.Entity<Brand>(mb=>{
        mb.Property(x=>x.TimeStamp).IsRowVersion().IsConcurrencyToken();
        //mb.HasKey(p=>new {p.Name, p.WebSite});
       });
    }
}
