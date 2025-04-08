using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Task.application.Dtos;
using Task.core.Models;
using Task.repository.Data.Repos;

namespace Task.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepo userRepo;


        public AuthController(UserManager<AppUser> userManager,IUserRepo userRepo)
        {
            _userManager = userManager;
            this.userRepo = userRepo;   
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterDto userFromRequest)
        {
            if (userFromRequest == null)
            {
                return BadRequest("User data is null");
            }
            var result = await userRepo.RegisterAsync(userFromRequest);
            if (result)
            {
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest("User registration failed");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("User data is null");
            }
            var result = await userRepo.LoginAsync(loginDto);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }


        }


    }
}
