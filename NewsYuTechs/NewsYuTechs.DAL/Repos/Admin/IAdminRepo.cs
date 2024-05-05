using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.DAL
{
    public interface IAdminRepo
    {
        IEnumerable<Admin> GetAllAdmins();
        Admin? GetAdminById(string adminId);
        string Add(Admin admin);
        bool Delete(Admin admin);
        bool Update(Admin admin);
        int SaveChanges();
    }
}
