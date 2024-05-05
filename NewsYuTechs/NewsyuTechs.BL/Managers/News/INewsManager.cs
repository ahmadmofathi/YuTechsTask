using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.BL
{
    public interface INewsManager
    {
        IEnumerable<NewsDTO> GetAllNews();
        NewsDTO GetNewsById(string id);
        string AddNews(NewsAddDTO news);
        bool UpdateNews(NewsDTO news);
        bool DeleteNews(string id);
    }
}
