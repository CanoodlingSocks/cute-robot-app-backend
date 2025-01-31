using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Robot
    {
        //This model should simply serve as a "blueprint" and info about each robot
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string RobotNickname { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public string Description { get; set; }

        //Image blobs
        public string Front_View { get; set; } 
        public string Left_View { get; set; } 
        public string Right_View { get; set; }
        public string Back_View { get; set; }

        //Navigation Prop
        public ICollection<RobotParts> RobotParts { get; set; }

    }
}
