using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.DAL
{
    public class Admin : IdentityUser<string>
    {
        public string? Name { get; set;}
    }
}
