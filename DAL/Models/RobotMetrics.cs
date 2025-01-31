using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class RobotMetrics
    {
        [Key]
        public int Id { get; set; }
       
        [ForeignKey("UserRobot")]
        public string UniqueRobotCode { get; set; } //Ref to UserRobots
       
        public decimal BatteryLevel { get; set; }
        public decimal Temperature { get; set; }
        public decimal PersonalityChipStability { get; set; }
        public string InteractionLog { get; set; }
        public TimeSpan TaskCompletionTime { get; set; }
        public string SentimentAnalysis { get; set; }
        public DateTime Timestamp { get; set; } //Timestamp for when the metrics were recorded
        //public UserRobots UserRobot { get; set; } //Nav prop to UserRobot
    }
}
