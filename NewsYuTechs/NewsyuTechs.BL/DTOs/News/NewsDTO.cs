using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.BL
{
    public class NewsDTO
    {
        public string? NewsId { get; set; }
        public string? Title { get; set; }
        public string? NewsContent { get; set; }
        public string? Image { get; set; }
        public string? PublicationDate { get; set; }
        public string? CreationDate { get; set; }
        public string? AuthorId { get; set; }

    }
}
