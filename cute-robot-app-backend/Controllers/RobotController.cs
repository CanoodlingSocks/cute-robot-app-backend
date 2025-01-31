using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Robot;

namespace project_nebula_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobotController : ControllerBase
    {
        private readonly IRobotService _robotService;

        public RobotController(IRobotService robotService)
        {
            _robotService = robotService;
        }

        [HttpGet("GetAllUserRobots")]
        public IActionResult GetAllUserRobots()
        {
            try
            {
                var userRobots = _robotService.GetAllUserRobots();
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{uniqueRobotCode}")]
        public IActionResult GetRobotByID(string uniqueRobotCode)
        {
            try
            {
                var robot = _robotService.GetRobotByID(uniqueRobotCode);
                if (robot != null)
                {
                    return Ok(robot);
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

        [HttpPut]
        public async Task<IActionResult> UpdateNicknameAsync(UpdateNicknameDTO changeNicknameDto)
        {
            try
            {
                var success = await _robotService.UpdateNicknameAsync(changeNicknameDto);
                if (success)
                {
                    return Ok("Nickname was changed successfully!");
                }
                return NotFound("Robot was not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update-bg-img")]
        public async Task<IActionResult> UpdateBackgroundImageAsync(string uniqueRobotCode, string imageUrl)
        {
            try
            {
                var updateResult = await _robotService.UpdateBackgroundImageAsync(uniqueRobotCode, imageUrl);

                if (updateResult)
                {
                    return Ok("Background image updated successfully!" );
                }

                return NotFound("Robot was not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

