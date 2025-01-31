using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class AdminAddNewRobotDTO
    {
        public string ModelName { get; set; }
        public string RobotNickname { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public string Description { get; set; }
        public string Front_View { get; set; }
        public string Left_View { get; set; }
        public string Right_View { get; set; }
        public string Back_View { get; set; }
    }
}
