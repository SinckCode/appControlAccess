using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appControlAccess1.Models
{
    public class AccessLog
    {
        public int Id { get; set; }
        public int? User_Id { get; set; }
        public string rfid_uid { get; set; }
        public string result { get; set; }
        public string notes { get; set; }
        public string? AssignedSpace { get; set; }
        public string Created_At { get; set; }
    }
}

