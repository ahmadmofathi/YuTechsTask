using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.DAL
{
    public class Author 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? AuthorId { get; set; }
        public string? AuthorName { get; set; }

    }
}
