using Microsoft.AspNetCore.Mvc;
using Service.Admin;
using Service.DTO;

namespace project_nebula_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("AddPart")]
        public async Task<IActionResult> AddNewPartAsync([FromBody] AdminAddNewPartDTO partDTO)
        {
            try
            {
                var newAddedPart = await _adminService.AddNewPartAsync(partDTO);
                return Ok(newAddedPart);
            }
            catch (Exception ex)
            {
                return BadRequest("Backend Error adding new part: " + ex.Message);
            }
        }

        [HttpPost("AddRobot")]
        public async Task<IActionResult> AddNewRobotAsync([FromBody] AdminAddNewRobotDTO robotDTO)
        {
            try
            {
                var newAddedRobot = await _adminService.AddNewRobotAsync(robotDTO);
                return Ok(newAddedRobot);
            }
            catch (Exception ex)
            {
                return BadRequest("Backend Error adding new robot: " + ex.Message);
            }
        }

        [HttpGet("GetParts")]
        public async Task<IActionResult> GetParts([FromQuery] PartFilterDTO filterOptions)
        {
            try
            {
                var parts = await _adminService.GetPartsAsync(filterOptions);
                return Ok(parts);
            }
            catch (Exception ex)
            {
                return BadRequest("Backend Error retrieving parts: " + ex.Message);
            }
        }

        [HttpDelete("DeletePart/{partId}")]
        public async Task<IActionResult> DeletePartAsync(int partId)
        {
            try
            {
                bool deleted = await _adminService.DeletePartAsync(partId);

                if (deleted)
                {
                    return Ok("Part was deleted");
                }
                else
                {
                    return NotFound("Part was not found");
                }
            } 
            catch (Exception ex)
            {
                return BadRequest("Backend Error deleting part: " + ex.Message);
            }
        }
    }
}
