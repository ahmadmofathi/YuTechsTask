using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.BL
{
    public class TokenDTO
    {
        public string? Token { get; set; }
        public DateTime ExpiryDate { get; set; }

        public string? Username { get; set; } 
        public string? User_id { get; set; }
    }
}
