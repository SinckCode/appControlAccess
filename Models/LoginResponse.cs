using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appControlAccess1.Models
{
    public class LoginResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int? id { get; set; }  // ID del guardia
    }
}

