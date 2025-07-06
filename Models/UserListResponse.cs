using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appControlAccess1.Models
{
    public class UserListResponse
    {
        public bool success { get; set; }
        public List<User> data { get; set; }
    }
}

