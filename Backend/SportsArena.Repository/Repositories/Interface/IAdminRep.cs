

using SportsArena.Models.DTOModels;

namespace SportsArena.Repository.Repositories.Interface
{
    public interface IAdminRep
    {
        public List<AdminModel> GetAllUsers();
        public List<AdminModel> GetSellers();
        public List<AdminModel> GetBuyers();
        public bool UpdateStatus(int userId, bool status);


    }
}
