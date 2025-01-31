using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_nebula_backend.JWTAuthentication;
using Service.DTO;
using Service.User;

namespace project_nebula_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        public IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration, ITokenService tokenService)
        {
            _userService = userService;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAllUsers();

                if (users != null)
                {
                    return Ok(users);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var user = _userService.GetUserById(userId);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            } 
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDTO login)
        {
            try
            {
                if (login.Username != null && login.Password != null)
                {
                    var result = _userService.UserLogin(login);
                    if (result != null)
                    {
                        var createdToken = _tokenService.CreateJWTToken(result);
                        return Ok(new { token = createdToken, user = result });
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{userId}/UserRobots")]
        public IActionResult ListUserRobots(int userId)
        {
            try
            {
                var userRobots = _userService.ListUserRobots(userId);

                if (userRobots != null)
                {
                    return Ok(userRobots);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("UserRobots/{uniqueRobotCode}")]
        public async Task<IActionResult> GetUserRobotImagesAsync(string uniqueRobotCode)
        {
            try
            {
                var userRobotData = await _userService.GetUserRobotImagesAsync(uniqueRobotCode);

                if (userRobotData == null)
                {
                    return NotFound();
                }

                return Ok(userRobotData); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }
    }
}
