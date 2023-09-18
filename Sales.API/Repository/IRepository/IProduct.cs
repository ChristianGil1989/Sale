using Sales.API.Models;
using System.Collections;

namespace Sales.API.Repository.IRepository
{
    public interface IProduct
    {
        ICollection<Product> GetProducts();
        Product GetProduct(int productid);
        Product GetProductName(string productName);
        ICollection<Product> GetProductsByPrice(decimal InitialPrice, decimal FinalPrice);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool DeleteProduct(Product product);
        bool SaveProduct();
        Task<List<Product>> GetProductsAndCatogory();
    }
}
