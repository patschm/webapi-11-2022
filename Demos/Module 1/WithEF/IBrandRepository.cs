namespace WithEF;

    public interface IBrandRepository
    {
        IEnumerable<Brand> GetAll();
        long Insert(Brand b );
        void Update(long id, Brand b);
        void Delete(long id);
        int Save();
        
    }
