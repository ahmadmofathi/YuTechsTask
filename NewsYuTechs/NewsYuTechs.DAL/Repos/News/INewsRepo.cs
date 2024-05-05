using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.DAL
{
    public interface INewsRepo
    {
        IEnumerable<News> GetAllNews();
        News? GetNewsById(string newsId);
        string Add(News news);
        bool Delete(News news);
        bool Update(News news);
        int SaveChanges();
    }
}
