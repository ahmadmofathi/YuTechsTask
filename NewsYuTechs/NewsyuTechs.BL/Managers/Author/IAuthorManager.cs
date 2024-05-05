using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.BL
{
    public interface IAuthorManager
    {
        IEnumerable<AuthorDTO> GetAllAuthors();
        AuthorDTO GetAuthorById(string id);
        string AddAuthor(AuthorAddDTO author);
        bool UpdateAuthor(AuthorDTO author);
        bool DeleteAuthor(string id);
    }
}
