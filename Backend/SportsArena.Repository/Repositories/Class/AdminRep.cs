

using AutoMapper;
using SportsArena.Database.SportsDb;
using SportsArena.Models.DbModels;
using SportsArena.Models.DTOModels;
using SportsArena.Repository.Repositories.Interface;

namespace SportsArena.Repository.Repositories.Class
{
    public class AdminRep:IAdminRep
    {
        DataContext _context;
       
        public AdminRep(DataContext context)
        {
            _context = context;
          
        }

        public List<AdminModel> GetAllUsers()
        {
          
            var data =_context.userdetails.Select(u => new AdminModel
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                RoleId = u.RoleId,
                IsActive = u.IsActive
            }).ToList();

            return data;

        }

        public List<AdminModel> GetBuyers( )
        {
            int roleid = 2;
            
            var sellers = _context.userdetails.Where(s => s.RoleId == roleid)
                            .Select(u => new AdminModel
                            {
                                UserId = u.UserId,
                                FirstName = u.FirstName,
                                LastName = u.LastName,
                                Email = u.Email,
                                RoleId = u.RoleId,
                                IsActive = u.IsActive

                            }).ToList();
            return sellers;
        }

        public List<AdminModel> GetSellers()
        {
            int roleid = 3;

            var buyers = _context.userdetails.Where(b => b.RoleId == roleid)
                         .Select(a => new AdminModel
                         {
                             UserId= a.UserId,
                             FirstName = a.FirstName,
                             LastName = a.LastName,
                             Email = a.Email,
                             RoleId = a.RoleId,
                             IsActive = a.IsActive

                         }).ToList();
            return buyers;
        }

        public bool UpdateStatus(int userId, bool status)
        {
            var id = _context.userdetails.Find(userId);
            if (id == null)
            {
                return false;
            }
            id.IsActive = status;
            _context.SaveChanges();
            return true;
        }


    }
}
