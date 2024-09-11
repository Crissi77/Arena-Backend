

using SportsArena.Models.DbModels;
using SportsArena.Models.DTOModels;

namespace SportsArena.Repository.Repositories.Interface
{
    public interface IuserRep
    {
        public  SportsDto GetUserByID(int id);
        public productDto AddProduct(productDto data,int userId);
        public List<productDto> GetProducts();
        public UpdateProfileDto EditProfile(UpdateProfileDto data, int id);
        public string DeleteProduct(int id);


    }
}
