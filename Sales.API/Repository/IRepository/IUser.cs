using Sales.API.Models;
using Sales.API.Models.DTOs;
using System.Collections;

namespace Sales.API.Repository.IRepository
{
    public interface IUser
    {
        ICollection<User> GetUsers();
        User GetUser(int userid);
        bool IsUniqueUser(string userName);
        Task<ResponseUserDto> LoginUser(LoginUserDto loginUser);
        Task<User> UserRegister(UserRegisterDto userRegisterDto);
    }
}
