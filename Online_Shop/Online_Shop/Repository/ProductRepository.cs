using Online_Shop.Data;
using Online_Shop.Interfaces.RepositoryInterfaces;
using Online_Shop.Models;

namespace Online_Shop.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return product;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                Product product = _context.Products.Find((int)id);
                product.Deleted = true;
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                List<Product> products = _context.Products.ToList();
                return products;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                Product product = _context.Products.Find((int)id);
                if (product.Deleted)
                    return null;
                return product;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            try
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return product;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
