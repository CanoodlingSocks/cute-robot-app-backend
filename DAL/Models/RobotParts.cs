using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class RobotParts //Junction table between Robot and Parts
    {
        [Key]
        public int RobotPartId { get; set; }
        public int RobotId { get; set; } //FK ref to Robot
        public int PartId { get; set; } //FK ref to Parts

        //Navigation Props
        public virtual Robot Robot { get; set; }
        public virtual Parts Part { get; set; }
    }
}
