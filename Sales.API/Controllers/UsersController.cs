using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.API.Models;
using Sales.API.Models.DTOs;
using Sales.API.Repository.IRepository;
using System.Net;

namespace Sales.API.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _user;
        protected ApiResponse _apiResponse;
        private readonly IMapper _mapper;

        public UsersController(IUser user, IMapper mapper )
        {
            _user = user;
            this._apiResponse = new();
            _mapper = mapper;              
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var listUsers = _user.GetUsers();
            var listUsersDto = new List<UserDto>();

            foreach ( var Users in listUsers) 
            {
                listUsersDto.Add(_mapper.Map<UserDto>(Users));
            }
            return Ok(listUsersDto);
        }

        [HttpGet("userId:int", Name = "GetUser")]
        public IActionResult GetUser(int userId)
        {
            var user = _user.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost("UserRegister")]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterDto userRegisterDto) 
        {
            if (userRegisterDto == null || !ModelState.IsValid) 
            {
                return BadRequest();
            }

            bool validateUserName = _user.IsUniqueUser(userRegisterDto.UserName);
            if (!validateUserName) 
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Ya existe un usuario con ese nombre");
                return BadRequest(_apiResponse);
            }

            var user = await _user.UserRegister(userRegisterDto);
            if (user == null) 
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Error en el registro");
                return BadRequest(_apiResponse);

            }
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            return BadRequest(_apiResponse);
        }

        [HttpPost("Login")]
        public async Task<IActionResult>Login([FromBody] LoginUserDto loginUserDto)
        {
            var responLogin = await _user.LoginUser(loginUserDto);

            if (responLogin.User == null || string.IsNullOrEmpty(responLogin.Token))
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages.Add("Usuario o password incorrectos");
                return BadRequest(_apiResponse);
            }
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.IsSuccess = true;
            _apiResponse.Result = responLogin;
            return BadRequest(_apiResponse);

        }
    }
}
