

using SportsArena.Models.DTOModels;

namespace SportsArena.Services.Services.Interface.IUser
{
    public interface IUserRegisterationSer
    {
        public string Register(SportsDto register);
        public string Login(LoginModel login);
    }
}
