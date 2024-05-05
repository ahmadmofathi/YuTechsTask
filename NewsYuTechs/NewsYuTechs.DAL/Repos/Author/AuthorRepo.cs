using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.DAL
{
    public class AuthorRepo : IAuthorRepo
    {
        private readonly AppDbContext _context;

        public AuthorRepo(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Author> GetAllAuthors()
        {
            return _context.Set<Author>().ToList();
        }

        public Author? GetAuthorById(string authorId)
        {
            return _context.Set<Author>().Find(authorId);
        }
        public string Add(Author author)
        {
            if (author.AuthorId == null)
            {
                return "Not Found";
            }
            _context.Set<Author>().Add(author);

            return author.AuthorId;
        }

        public bool Delete(Author author)
        {
            _context.Set<Author>().Remove(author);
            return true;
        }
        public bool Update(Author author)
        {
            _context.Set<Author>().Update(author);
            return true;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
