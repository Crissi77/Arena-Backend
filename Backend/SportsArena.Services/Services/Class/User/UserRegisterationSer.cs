
using SportsArena.Models.DTOModels;
using SportsArena.Repository.Repositories.Interface.IUser;
using SportsArena.Services.Services.Interface.IUser;

namespace SportsArena.Services.Services.Class.User
{
    public class UserRegisterationSer:IUserRegisterationSer
    {
        IUserRegisterationRep _reg;
        public UserRegisterationSer(IUserRegisterationRep reg)
        {
            _reg = reg;
        }

        public string Register(SportsDto register)
        {            
           return _reg.Register(register);
        }

        public string Login(LoginModel login)
        {
          
           return _reg.Login(login);
            
        }
    }
}