

using SportsArena.Models.DbModels;
using SportsArena.Models.DTOModels;

namespace SportsArena.Services.Services.Interface.IUser
{
    public interface IuserSer
    {
        public SportsDto GetUserByID(int id);
        public productDto AddProduct(productDto data,int userid);
        public List<productDto> GetProducts();
        public UpdateProfileDto EditProfile(UpdateProfileDto data, int id);
        public string DeleteProduct(int id);
    }
}
