using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class PartDTO
    {
        public int Id { get; set; }
        public string PartName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string CloseUpImage { get; set; }
        public string PartImage { get; set; }
        public int Order { get; set; }
    }
}
