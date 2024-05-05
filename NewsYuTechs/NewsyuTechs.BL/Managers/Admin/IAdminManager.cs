using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.BL
{
    public interface IAdminManager
    {
        IEnumerable<AdminDTO> GetAllAdmins();
        AdminDTO GetAdminById(string id);
        string AddAdmin(AdminAddDTO admin);
        bool UpdateAdmin(AdminDTO admin);
        bool DeleteAdmin(string id);
    }
}
