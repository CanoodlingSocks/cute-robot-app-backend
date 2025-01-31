using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class UpdateNicknameDTO
    {
        public string UniqueRobotCode { get; set; }
        public string NewNickname { get; set; }
    }
}
