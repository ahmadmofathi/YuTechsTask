using NewsYuTechs.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.BL
{
    public class AuthorManager : IAuthorManager
    {
        private readonly IAuthorRepo _authorRepo;
        public AuthorManager(IAuthorRepo authorRepo)
        {
            _authorRepo = authorRepo;
        }
        public string AddAuthor(AuthorAddDTO a)
        {

            Author author = new Author
            {
                AuthorId = Guid.NewGuid().ToString(),
                AuthorName = a.AuthorName,
            };
            _authorRepo.Add(author);
            _authorRepo.SaveChanges();
            return "Author " + author.AuthorId + " is added";
        }

        public bool DeleteAuthor(string id)
        {
            Author? author = _authorRepo.GetAuthorById(id);
            if (author == null)
            {
                return false;
            }
            _authorRepo.Delete(author);
            _authorRepo.SaveChanges();
            return true;
        }

        public AuthorDTO GetAuthorById(string id)
        {
            Author? author = _authorRepo.GetAuthorById(id);
            return new AuthorDTO
            {
                AuthorId = author!.AuthorId,
                AuthorName = author!.AuthorName,
            };
        }

        public IEnumerable<AuthorDTO> GetAllAuthors()
        {
            IEnumerable<Author> authorFromDB = _authorRepo.GetAllAuthors();
            return authorFromDB.Select(a => new AuthorDTO
            {
                AuthorName = a.AuthorName,
                AuthorId = a.AuthorId
            });
        }

        public bool UpdateAuthor(AuthorDTO a)
        {
            if (a.AuthorId is null)
            {
                return false;
            }
            Author? author = _authorRepo.GetAuthorById(a.AuthorId);
            if (author == null)
            {
                return false;
            }
            author.AuthorId = a.AuthorId;
            author.AuthorName = a.AuthorName;
            _authorRepo.Update(author);
            _authorRepo.SaveChanges();
            return true;
        }
    }
}
