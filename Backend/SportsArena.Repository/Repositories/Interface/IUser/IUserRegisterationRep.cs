using SportsArena.Models.DbModels;
using SportsArena.Models.DTOModels;


namespace SportsArena.Repository.Repositories.Interface.IUser
{
    public interface IUserRegisterationRep
    {
        public string Register(SportsDto register );
        public string Login(LoginModel login);
       
    }


}



