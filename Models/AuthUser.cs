using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace appControlAccess1.Models
{
    public class AuthUser
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }  // Se envía al backend y se hashea allá
        public string rol { get; set; }       // 'guardia' o 'admin'
        public string created_at { get; set; }  // (opcional solo para mostrar)
    }
}

