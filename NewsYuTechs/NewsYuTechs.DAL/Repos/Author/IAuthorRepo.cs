using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.DAL
{
    public interface IAuthorRepo
    {
        IEnumerable<Author> GetAllAuthors();
        Author? GetAuthorById(string authorId);
        string Add(Author author);
        bool Delete(Author author);
        bool Update(Author author);
        int SaveChanges();
    }
}
