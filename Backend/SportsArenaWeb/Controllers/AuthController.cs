using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SportsArena.Models.DTOModels;
using SportsArena.Repository.Repositories.Interface.IUser;
using SportsArena.Services.Services.Interface.IUser;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace SportsArenaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IUserRegisterationSer _ser;
        private IConfiguration _config;
        public AuthController(IUserRegisterationSer ser,IConfiguration config)
        {
           _ser = ser;
            _config = config;
        }

       
        [HttpPost]
        [Route("register")]

        public string Register(SportsDto register)
        {
            return _ser.Register(register);
        }

        [HttpPost("Login")]

        public ActionResult Login(LoginModel login)
        {
            var res = _ser.Login(login);

            Response obj = new Response
            {
                Success = true,
                Message = "Success",
                Data = res
            };
            return Ok(obj);
        }
    }
}