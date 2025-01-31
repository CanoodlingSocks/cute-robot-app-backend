using DAL;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Service.Metrics
{
    public class MetricsService : IMetricsService
    {
        public async Task<List<RobotMetrics>> GetMetricsByIdAsync(string uniqueRobotCode)
        {
            using (var context = new ChubbyBotDbContext())
            {
                return await context.RobotMetrics.Where(u => u.UniqueRobotCode == uniqueRobotCode).ToListAsync();
            }
        }
    }
}
