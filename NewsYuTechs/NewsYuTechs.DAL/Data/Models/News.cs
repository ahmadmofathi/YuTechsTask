using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.DAL
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? NewsId {  get; set; }
        public string? Title { get; set; }
        public string? NewsContent {  get; set; }
        public string? Image {  get; set; }
        public string? PublicationDate { get; set; }
        public string? CreationDate { get;set; }
        [ForeignKey("AuthorId")]
        public string? AuthorId { get; set; }
        public virtual Author? Author { get; set; }

    }
}
