using Microsoft.EntityFrameworkCore;

namespace WithEF;
class Program
{
    const string constr = @"Server=.\sqlexpress;Database=ProductCatalog;Trusted_Connection=True;";
    static void Main(string[] args)
    {
        DbContextOptionsBuilder bld = new DbContextOptionsBuilder();
        bld.UseSqlServer(constr);
        //bld.LogTo(Console.WriteLine);
        ShopContext ctx = new ShopContext(bld.Options);

        Brand bn = new Brand { Name = "Kijkshop", WebSite = "www.kijkshop.nl" };
        ctx.Brands.Add(bn);
        ctx.SaveChanges();

        Console.ReadLine();
        bn.Name = "V&D";
        for (int i = 0; i < 3; i++)
        {
            try
            {
                ctx.SaveChanges();
                break;
            }
            catch (DbUpdateConcurrencyException dbo)
            {
                dbo.Entries[0].GetDatabaseValues();
            }
        }
        foreach (Brand b in ctx.Brands)
        {
            System.Console.WriteLine($"[{b.Id}] {b.Name}, {b.WebSite}");
        }

    }
}
