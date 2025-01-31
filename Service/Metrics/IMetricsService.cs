using DAL.Models;

namespace Service.Metrics
{
    public interface IMetricsService 
    {
        public Task<List<RobotMetrics>> GetMetricsByIdAsync(string uniqueRobotCode);
    }
}
