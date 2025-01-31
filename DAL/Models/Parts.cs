using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Parts
    {
        [Key]
        public int PartId { get; set; }
        public string PartName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } //Type of part (Head, Body, LeftLeg, RightArm etc)

        //Image Urls
        public string CloseUpImage { get; set; } 
        public string PartImage { get; set; }

        //Specify Image Order
        public int Order { get; set; }
    }
}
