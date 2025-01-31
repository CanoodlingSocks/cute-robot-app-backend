
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public class PartFilterDTO
    {
        public string Type { get; set; } = "All";
        public string SortBy { get; set; } = "Name";
        public string SearchKeyWord { get; set; } = "";
    }
}
