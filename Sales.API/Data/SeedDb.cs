using Sales.API.Helpers;
using Sales.API.Models;

namespace Sales.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IEncrypPassword _helperEncry;
        public SeedDb(DataContext context, IEncrypPassword helperEncry)
        {
            _context = context;
            _helperEncry = helperEncry;
        }

        public async Task SeedDdAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriesAsync();
            await CheckProductsAsync();
            await CheckUsersAsync("admi@gmail.com","Christian","Gil","abcd1234","admin");
        }

        private async Task CheckUsersAsync(string UserName, string name, string lastName, string password, string role)
        {
            if (!_context.Users.Any())
            {
                var passwordEncryp = _helperEncry.Obtenermd5(password);
                _context.Users.Add(new User
                {
                    UserName = UserName,
                    Name = name,
                    LastName = lastName,
                    Password = passwordEncryp,
                    Role = role
                    
                });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category
                {
                    Name = "Ferreteria",
                    CreationDate = DateTime.Now,
                });
                await _context.SaveChangesAsync();
            }
        }
        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                var categori = _context.Categories.FirstOrDefault(c => c.Name == "Ferreteria");

                if (categori != null)
                {
                    _context.Products.Add(new Product
                    {
                        Name = "Martillo",
                        Description = "Martillo de ferreteria",
                        Price = 5000,
                        InitialAmount = 12,
                        CategoryId = categori.Id,
                        CreationDate = DateTime.Now,
                    });
                    await _context.SaveChangesAsync();
                }
                
            }
        }
    }
}
