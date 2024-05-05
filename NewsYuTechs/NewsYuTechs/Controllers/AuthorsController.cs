using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsYuTechs.BL;
using NewsYuTechs.DAL;

namespace NewsYuTechs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorManager _authorManager;

        public AuthorsController(IAuthorManager authorManager)
        {
            _authorManager = authorManager;
        }
        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            var Authors = _authorManager.GetAllAuthors();
            return Ok(Authors);
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(string id)
        {
            var Author = _authorManager.GetAuthorById(id);
            if (Author == null)
            {
                return BadRequest("Author not found");
            }
            return Ok(Author);
        }

        [HttpPost]
        public IActionResult AddAuthor(AuthorAddDTO author)
        {
            var a=_authorManager.AddAuthor(author);
            return Ok(a);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(string id)
        {
            AuthorDTO? Author = _authorManager.GetAuthorById(id);
            if (Author == null)
            {
                return NotFound("User not found");
            }
            _authorManager.DeleteAuthor(id);
            return Ok("Author " + id + " has been deleted successfully");
        }

        [HttpPut("{id}")]
        public ActionResult<Author> UpdateAuthor(string id, AuthorDTO Author)
        {

            if (id != Author.AuthorId)
            {
                return BadRequest();
            }
            _authorManager.UpdateAuthor(Author);
            return NoContent();
        }
    }
}
