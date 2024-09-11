

using SportsArena.Models.DTOModels;

namespace SportsArena.Services.Services.Interface
{
    public interface IAdminSer
    {
        public List<AdminModel> GetAllUsers();
        public List<AdminModel> GetSellers();
        public List<AdminModel> GetBuyers();
        public bool UpdateStatus(int userId, bool status);
    }
}
