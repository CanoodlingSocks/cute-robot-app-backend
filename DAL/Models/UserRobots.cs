using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class UserRobots
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //Unique Keys will be assigned to every unit, the db does not need to auto-generate this
        public string UniqueRobotCode { get; set; } //Instead of an ID, the user will receive a unique code for their unit so this will serve as a PK
        public int UserId { get; set; } //FK ref to the Users Model
        public int RobotId { get; set; } //FK ref to the Robots Model
        public string CustomRobotNickName { get; set; } //Users can customize their robots nicknames 
       
        //Filter
        public bool IsBroken { get; set; }
        public bool IsActive { get; set; }
       
        //Info
        public DateTime? LastRepairDate { get; set; }
        public int RepairCount { get; set; }

        //Image rendering for each robot part
        public int? HeadId { get; set; } 
        public int? BodyId { get; set; } 
        public int? LeftArmId { get; set; } 
        public int? RightArmId { get; set; } 
        public int? LeftLegId { get; set; } 
        public int? RightLegId { get; set; }

        //Background Image
        public string Image_Background { get; set; }

        //Navigation Props
        public User User { get; set; }
        public Robot Robot { get; set; }
    }
}
