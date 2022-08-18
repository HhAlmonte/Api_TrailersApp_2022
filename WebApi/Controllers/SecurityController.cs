using Core.Entities;
using Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dto_s;
using WebApi.Errors;

namespace WebApi.Controllers
{
    public class SecurityController : BaseController
    {
        private readonly UserManager<UserEntities> _userManager;
        private readonly SignInManager<UserEntities> _signInManager;
        private readonly IPasswordHasher<UserEntities> _passwordHasher;
        private readonly ITokenService _tokenService;

        public SecurityController(UserManager<UserEntities> userManager,
                                  SignInManager<UserEntities> signInManager,
                                  IPasswordHasher<UserEntities> passwordHasher,
                                  ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login(loginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UserDto>> registrar(RegisterDto registrarDto)
        {
            var user = new UserEntities(registrarDto.Name,
                                        registrarDto.LastName)
            {
                Email = registrarDto.Email,
                UserName = registrarDto.UserName
            };

            var result = await _userManager.CreateAsync(user, registrarDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(400));
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPut("actualizar/{id}")]
        public async Task<ActionResult<UserDto>> actualizar(string id, RegisterDto registerDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if(user == null)
            {
                return NotFound(new CodeErrorResponse(404, "El usuario no existe"));
            }

            user.Name = registerDto.Name;
            user.LastName = registerDto.LastName;
            user.UserName = registerDto.Image;
            user.Image = registerDto.Image;

            if (!string.IsNullOrEmpty(registerDto.Email))
            {
                user.Email = registerDto.Email;
            }

            if (!string.IsNullOrEmpty(registerDto.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);
            }
            
            var resultado = await _userManager.UpdateAsync(user);

            if (!resultado.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(400, $"no se pudo actualizar el usuario ${user.UserName}"));
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                Image =  user.Image,
                Token = _tokenService.CreateToken(user)
            };
        }

    }
}
