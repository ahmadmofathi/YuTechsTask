using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsYuTechs.BL;
using NewsYuTechs.DAL;

namespace NewsYuTechs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsManager _newsManager;

        public NewsController(INewsManager newsManager)
        {
            _newsManager = newsManager;
        }
        [HttpGet]
        public IActionResult GetAllNewss()
        {
            var Newss = _newsManager.GetAllNews();
            return Ok(Newss);
        }

        [HttpGet("{id}")]
        public IActionResult GetNewsById(string id)
        {
            var News = _newsManager.GetNewsById(id);
            if (News == null)
            {
                return BadRequest("News not found");
            }
            return Ok(News);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddNews([FromForm] NewsAddDTO news, IFormFile picture)
        {
            // Checking if picture is provided
            if (picture == null || picture.Length == 0)
            {
                return BadRequest("Please provide an image.");
            }

            // Checking Extension
            var extension = Path.GetExtension(picture.FileName);
            var allowedExtensions = new string[] { ".png", ".jpg" };
            if (!allowedExtensions.Contains(extension, StringComparer.InvariantCultureIgnoreCase))
            {
                return BadRequest("Invalid file extension. Allowed extensions are .png and .jpg.");
            }

            // Checking Size
            const long maxFileSize = 6_000_000; // approx. 6 MB
            if (picture.Length > maxFileSize)
            {
                return BadRequest($"File size exceeds the maximum allowed size of {maxFileSize} bytes.");
            }

            // Generate new file name
            var newFileName = $"{Guid.NewGuid()}{extension}";
            var imagesPath = Path.Combine(Environment.CurrentDirectory, "Uploads", "StaticContent");
            var fullFilePath = Path.Combine(imagesPath, newFileName);

            // Save picture to disk
            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            }

            news.Image = $"{Request.Scheme}://{Request.Host}/FileManager/GetImage?ImageName={newFileName}";

            // Validate publication date
            if (!DateTime.TryParse(news.PublicationDate, out DateTime publicationDate))
            {
                return BadRequest("Invalid publication date format. Please provide a valid date in the format 'yyyy/MM/dd'.");
            }

            // Validate publication date range (between today and 7 days from now)
            var today = DateTime.Today;
            var maxPublicationDate = today.AddDays(7);
            if (publicationDate < today || publicationDate > maxPublicationDate)
            {
                return BadRequest("Publication date must be between today and a week from today.");
            }

            var a = _newsManager.AddNews(news);
            return Ok(a);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(string id)
        {
            NewsDTO? News = _newsManager.GetNewsById(id);
            if (News == null)
            {
                return NotFound("User not found");
            }
            _newsManager.DeleteNews(id);
            return Ok("News " + id + " has been deleted successfully");
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]

        public async Task<ActionResult<News>> UpdateNewsAsync(string id, [FromForm] NewsDTO news, IFormFile picture)
        {
            if (id != news.NewsId)
            {
                return BadRequest("Invalid news ID.");
            }

            // Get the previous image file name
            var previousFileName = Path.GetFileName(new Uri(news.Image!).LocalPath);

            // Checking if picture is provided
            if (picture == null || picture.Length == 0)
            {
                return BadRequest("Please provide an image.");
            }

            // Checking Extension
            var extension = Path.GetExtension(picture.FileName);
            var allowedExtensions = new string[] { ".png", ".jpg" };
            if (!allowedExtensions.Contains(extension, StringComparer.InvariantCultureIgnoreCase))
            {
                return BadRequest("Invalid file extension. Allowed extensions are .png and .jpg.");
            }

            // Checking Size
            const long maxFileSize = 6_000_000; // approx. 6 MB
            if (picture.Length > maxFileSize)
            {
                return BadRequest($"File size exceeds the maximum allowed size of {maxFileSize} bytes.");
            }

            // Generate new file name
            var newFileName = $"{Guid.NewGuid()}{extension}";
            var imagesPath = Path.Combine(Environment.CurrentDirectory, "Uploads", "StaticContent");
            var fullFilePath = Path.Combine(imagesPath, newFileName);

            // Save picture to disk
            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                await picture.CopyToAsync(stream);
            }

            // Delete the previous image file
            var previousFilePath = Path.Combine(imagesPath, previousFileName);
            if (System.IO.File.Exists(previousFilePath))
            {
                System.IO.File.Delete(previousFilePath);
            }

            news.Image = $"{Request.Scheme}://{Request.Host}/FileManager/GetImage?ImageName={newFileName}";

            // Validate publication date
            if (!DateTime.TryParse(news.PublicationDate, out DateTime publicationDate))
            {
                return BadRequest("Invalid publication date format. Please provide a valid date in the format 'yyyy/MM/dd'.");
            }

            // Validate publication date range (between today and 7 days from now)
            var today = DateTime.Today;
            var maxPublicationDate = today.AddDays(7);
            if (publicationDate < today || publicationDate > maxPublicationDate)
            {
                return BadRequest("Publication date must be between today and a week from today.");
            }

            _newsManager.UpdateNews(news);
            return NoContent();
        }

    }
}
