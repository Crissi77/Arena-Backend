

using SportsArena.Models.DTOModels;
using SportsArena.Repository.Repositories.Interface;
using SportsArena.Services.Services.Interface;

namespace SportsArena.Services.Services.Class
{
    public class AdminSer : IAdminSer
    {
        IAdminRep _reg;
        public AdminSer(IAdminRep reg)
        {
            _reg = reg;
        }

        public List<AdminModel> GetAllUsers()
        {
           return _reg.GetAllUsers();
        }

        public List<AdminModel> GetSellers()
        {
          return _reg.GetSellers();
        }

        public List<AdminModel> GetBuyers()
        {
           return _reg.GetBuyers();
        }

        public bool UpdateStatus(int userId, bool status)
        {
            return _reg.UpdateStatus(userId, status);
        }
    }
}
