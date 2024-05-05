using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NewsYuTechs.DAL;

namespace NewsYuTechs.BL
{
    public class AdminManager : IAdminManager
    {
        private readonly IAdminRepo _adminRepo;


        public AdminManager(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;

        }
        public string AddAdmin(AdminAddDTO a)
        {

            Admin admin = new Admin
            {
                Name = a.Name,
                UserName = a.Username,
            };
            _adminRepo.Add(admin);
            _adminRepo.SaveChanges();
            return "Admin "+admin.Id+" is added";
        }

        public bool DeleteAdmin(string id)
        {
            Admin? admin = _adminRepo.GetAdminById(id);
            if (admin == null)
            {
                return false;
            }
            _adminRepo.Delete(admin);
            _adminRepo.SaveChanges();
            return true;
        }

        public AdminDTO GetAdminById(string id)
        {
            Admin? admin = _adminRepo.GetAdminById(id);


            return new AdminDTO
            {
                Id =admin!.Id,
                Name = admin!.Name,
                Username = admin!.UserName,
            };
        }

        public IEnumerable<AdminDTO> GetAllAdmins()
        {
            IEnumerable<Admin> adminFromDB = _adminRepo.GetAllAdmins();
            return adminFromDB.Select(a => new AdminDTO
            {
                Id = a.Id,
                Name = a.Name,
                Username = a.UserName,
            });
        }

        public bool UpdateAdmin(AdminDTO a)
        {
            if (a.Id is null)
            {
                return false;
            }
            Admin? admin = _adminRepo.GetAdminById(a.Id);
            if (admin == null)
            {
                return false;
            }
            admin.Name = a.Name;
            admin.UserName = a.Username;
            _adminRepo.Update(admin);
            _adminRepo.SaveChanges();
            return true;
        }
    }
}
