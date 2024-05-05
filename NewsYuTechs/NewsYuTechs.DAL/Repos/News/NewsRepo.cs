using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.DAL
{
    public class NewsRepo : INewsRepo
    {
        private readonly AppDbContext _context;

        public NewsRepo(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<News> GetAllNews()
        {
            return _context.Set<News>().ToList();
        }

        public News? GetNewsById(string newsId)
        {
            return _context.Set<News>().Find(newsId);
        }
        public string Add(News news)
        {
            if (news.NewsId == null)
            {
                return "Not Found";
            }
            _context.Set<News>().Add(news);

            return news.NewsId;
        }

        public bool Delete(News news)
        {
            _context.Set<News>().Remove(news);
            return true;
        }
        public bool Update(News news)
        {
            _context.Set<News>().Update(news);
            return true;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
