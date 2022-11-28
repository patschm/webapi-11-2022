using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WithEF
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ShopContext _context;

        public BrandRepository(ShopContext context)
        {
            _context = context;
        }

        public void Delete(long id)
        {
            var victim = _context.Brands.Find(id);
            _context.Brands.Remove(victim);
        }

        public IEnumerable<Brand> GetAll()
        {
            return _context.Brands.ToList();
        }

        public long Insert(Brand b)
        {
            _context.Brands.Add(b);
            Save();
            return b.Id;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Update(long id, Brand b)
        {
            var victim = _context.Brands.Find(id);
            _context.Entry(victim!).CurrentValues.SetValues(b);

        }
    }
}