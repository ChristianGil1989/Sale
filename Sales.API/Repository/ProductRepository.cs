using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.API.Models;
using Sales.API.Repository.IRepository;

namespace Sales.API.Repository
{
    public class ProductRepository : IProduct
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateProduct(Product product)
        {
            product.CreationDate = DateTime.Now;
            _context.Products.Add(product);
            return SaveProduct();
        }

        public bool DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            return SaveProduct();
        }

        public Product GetProduct(int productid)
        {
            return _context.Products.FirstOrDefault(c => c.Id == productid);
        }

        public Product GetProductName(string productName)
        {
            return _context.Products.FirstOrDefault(c => c.Name == productName);
        }

        public ICollection<Product> GetProducts()
        {
            return _context.Products
                   .Include(c => c.Category)                
                   .OrderBy(c => c.Name).ToList();
        }

        public ICollection<Product> GetProductsByPrice(decimal initialPrice, decimal finalPrice)
        {
            var products = new List<Product>();

            products = (from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        where p.Price >= initialPrice && p.Price <= finalPrice
                        select p).ToList();

            if (products.Count > 0) 
            {
                return products;
            }

            return null;        
        }
        public bool SaveProduct()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateProduct(Product product)
        {
            product.CreationDate = DateTime.Now;
            _context.Products.Update(product);
            return SaveProduct();
        }
    }
}
