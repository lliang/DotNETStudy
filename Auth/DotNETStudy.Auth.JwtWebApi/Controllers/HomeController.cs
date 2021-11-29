using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using DotNETStudy.Auth.JwtWebApi.Models;
using DotNETStudy.Auth.JwtWebApi.Services;

namespace DotNETStudy.Auth.JwtWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepositoryService _userRepositoryService;
        private readonly IConfiguration _configuration;
        public HomeController(ITokenService tokenService, IUserRepositoryService userRepositoryService, IConfiguration config)
        {
            _tokenService = tokenService;
            _userRepositoryService = userRepositoryService;
            _configuration = config;
        }

        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public ActionResult Login(UserModel userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return RedirectToAction("Error");
            }

            IActionResult response = Unauthorized();
            var user = _userRepositoryService.GetUser(userModel);

            if (user != null)
            {
                var generatedToken = _tokenService.BuildToken(_configuration["Jwt:Key"].ToString(), _configuration["Jwt:Issuer"].ToString(), user);
                return Ok(new
                {
                    token = generatedToken,
                    user = user.UserName
                });

            }
            return RedirectToAction("Error");
        }

        [Authorize]
        [Route("Secret")]
        [HttpPost]
        public ActionResult SecretFunction()
        {
            return Ok("Alright, you are authorized user");
        }
    }
}
