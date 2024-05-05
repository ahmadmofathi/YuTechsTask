using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsYuTechs.DAL
{
    public class AdminRepo : IAdminRepo
    {
        private readonly AppDbContext _context;

        public AdminRepo(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Admin> GetAllAdmins()
        {
            return _context.Set<Admin>().ToList();
        }

        public Admin? GetAdminById(string adminId)
        {
            return _context.Set<Admin>().Find(adminId);
        }
        public string Add(Admin admin)
        {
            if (admin.Id == null)
            {
                return "Not Found";
            }
            _context.Set<Admin>().Add(admin);

            return admin.Id;
        }

        public bool Delete(Admin admin)
        {
            _context.Set<Admin>().Remove(admin);
            return true;
        }
        public bool Update(Admin admin)
        {
            _context.Set<Admin>().Update(admin);
            return true;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

    }
}
