

using AutoMapper;
using SportsArena.Models.DbModels;
using SportsArena.Models.DTOModels;
using SportsArena.Repository.Repositories.Interface;
using SportsArena.Services.Services.Interface.IUser;

namespace SportsArena.Services.Services.Class.User
{
    public class UserSer : IuserSer
    {
        IuserRep _rep;
        IMapper _mapper;
        public UserSer(IuserRep rep,IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }


        public SportsDto GetUserByID(int id )
        {
            return _rep.GetUserByID(id);
        }

        public UpdateProfileDto EditProfile(UpdateProfileDto data, int id)
        {
            return _rep.EditProfile(data,id);
        }


        public productDto AddProduct(productDto data,int userId)
        {
           
          return  _rep.AddProduct(data,userId);
        }

        public List<productDto> GetProducts()
        {
            return _rep.GetProducts();
        }

        public string DeleteProduct(int id)
        {
            return _rep.DeleteProduct(id);
        }


    }
}
