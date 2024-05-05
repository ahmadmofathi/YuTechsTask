using NewsYuTechs.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.BL
{
    public class NewsManager : INewsManager
    {
        private readonly INewsRepo _newsRepo;
        public NewsManager(INewsRepo newsRepo)
        {
            _newsRepo = newsRepo;
        }
        public string AddNews(NewsAddDTO n)
        {

            News news = new News
            {
               NewsId= Guid.NewGuid().ToString(),
               Title = n.Title,
               NewsContent = n.NewsContent,
               Image= n.Image,
               PublicationDate = n.PublicationDate,
               CreationDate = n.CreationDate,
               AuthorId = n.AuthorId,
            };
            _newsRepo.Add(news);
            _newsRepo.SaveChanges();
            return "News " + news.NewsId + " is added";
        }

        public bool DeleteNews(string id)
        {
            News? news = _newsRepo.GetNewsById(id);
            if (news == null)
            {
                return false;
            }
            _newsRepo.Delete(news);
            _newsRepo.SaveChanges();
            return true;
        }

        public NewsDTO GetNewsById(string id)
        {
            News? n = _newsRepo.GetNewsById(id);


            return new NewsDTO
            {
                NewsId=n!.NewsId,
                Title = n!.Title,
                NewsContent = n!.NewsContent,
                Image = n!.Image,
                PublicationDate = n!.PublicationDate,
                CreationDate = n!.CreationDate,
                AuthorId = n!.AuthorId,
            };
        }

        public IEnumerable<NewsDTO> GetAllNews()
        {
            IEnumerable<News> newsFromDB = _newsRepo.GetAllNews();
            return newsFromDB.Select(n => new NewsDTO
            {
                NewsId = n.NewsId,
                Title = n.Title,
                NewsContent = n.NewsContent,
                Image = n.Image,
                PublicationDate = n.PublicationDate,
                CreationDate = n.CreationDate,
                AuthorId = n.AuthorId,
            });
        }

        public bool UpdateNews(NewsDTO n)
        {
            if (n.NewsId is null)
            {
                return false;
            }
            News? news = _newsRepo.GetNewsById(n.NewsId);
            if (news == null)
            {
                return false;
            }
            news.Title = n.Title;
            news.NewsContent = n.NewsContent;
            news.Image = n.Image;
            news.PublicationDate = n.PublicationDate;
            news.CreationDate = n.CreationDate;
            news.AuthorId = n.AuthorId;
            _newsRepo.Update(news);
            _newsRepo.SaveChanges();
            return true;
        }
    }
}
