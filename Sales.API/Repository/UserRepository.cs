using Microsoft.IdentityModel.Tokens;
using Sales.API.Data;
using Sales.API.Helpers;
using Sales.API.Models;
using Sales.API.Models.DTOs;
using Sales.API.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sales.API.Repository
{
    public class UserRepository : IUser
    {
        private readonly DataContext _context;
        private readonly IEncrypPassword _helperEncry;
        private string secretKey;
        public UserRepository(DataContext context, IEncrypPassword helperEncry, IConfiguration config)
        {
            _context = context;
            _helperEncry = helperEncry;
            secretKey = config.GetValue<string>("ApiSettings:SecretKey");
        }
        public User GetUser(int userid)
        {
            return _context.Users.FirstOrDefault(c => c.Id == userid);
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(c => c.UserName).ToList();
        }

        public bool IsUniqueUser(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            return user == null ? true : false;
        }

        public async Task<ResponseUserDto> LoginUser(LoginUserDto loginUser)
        {
            var passwordEncryp = _helperEncry.Obtenermd5(loginUser.Password);
            var user = _context.Users.FirstOrDefault(
                u => u.UserName.ToLower() == loginUser.UserName.ToLower() 
                && u.Password == passwordEncryp);

            if (user == null) 
            {
                return new ResponseUserDto()
                {
                    Token = "",
                    User = null
                };
            }
            var token = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var codeToken = token.CreateToken(tokenDescription);
            ResponseUserDto loginUserDto = new ResponseUserDto()
            {
                Token = token.WriteToken(codeToken),
                User = user
            };

            return loginUserDto;

        }

        public async Task<User> UserRegister(UserRegisterDto userRegisterDto)
        {
            var passwordEncryp = _helperEncry.Obtenermd5(userRegisterDto.Password);

            User user = new User() 
            {
                UserName = userRegisterDto.UserName,
                Password = passwordEncryp,
                Name = userRegisterDto.Name,
                LastName = userRegisterDto.LastName,
                Role = userRegisterDto.Role,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
