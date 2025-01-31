using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Metrics;

namespace project_nebula_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IMetricsService _metricsService;

        public MetricsController(IMetricsService metricsService)
        {
            _metricsService = metricsService;
        }

        [HttpGet("{uniqueRobotCode}")]
        public async Task<ActionResult<List<RobotMetrics>>> GetMetricsByIdAsync(string uniqueRobotCode)
        {
            try
            {
                var metrics = await _metricsService.GetMetricsByIdAsync(uniqueRobotCode);

                if (metrics == null || metrics.Count == 0)
                {
                    return NotFound($"Metrics not found for robot with code {uniqueRobotCode}");
                }

                return Ok(metrics);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
