
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SportsArena.Database.SportsDb;
using SportsArena.Email.EmailServices;
using SportsArena.Models.DbModels;
using SportsArena.Models.DTOModels;
using SportsArena.Repository.Repositories.Interface.IUser;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace SportsArena.Repository.Repositories.Class.User
{
    public class UserRegisterationRep : IUserRegisterationRep
    {
        private readonly DataContext _context;
        private readonly ISendMailService _sendMailService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserRegisterationRep(DataContext context, IMapper mapper, ISendMailService sendMailService, IConfiguration con)
        {
            _context = context;
            _mapper = mapper;
            _sendMailService = sendMailService;
            _config = con;
        }

        private string GenerateRandomPassword(int length = 8)
        {
            const string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();
            return new string(Enumerable.Repeat(validChars, length)
                                        .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string Register(SportsDto register)
        {
            if (register == null)
            {
                return "Invaid details.";
            }
            string generatedPassword = GenerateRandomPassword();


            var details = _mapper.Map<UserDetails>(register);
            var address = _mapper.Map<UserAddress>(register);


            details.UserPassword = generatedPassword;

            details.CreatedAt = DateTime.Now;
            _context.userdetails.Add(details);
            _context.SaveChanges();

            address.UserId = details.UserId;

            _context.useraddress.Add(address);
            _context.SaveChanges();

            string body = $" Hii {register.FirstName} {register.LastName} welcome to SportsArena ,Your email is {register.Email} and Password is {generatedPassword} Now you can enjoy our services Keep your email and password safely..";
            var sent = _sendMailService.SendEmailAsync(register.Email, generatedPassword, body);
            if (sent != null)
            {
                return "Your password has been sent to your Email.";
            }

            return "Successfully registered";


        }


        public string Login(LoginModel login)
        {
            int roleid = _context.userdetails
                                  .Where(u => u.Email == login.Email && u.UserPassword == login.UserPassword)
                                  .Select(u => u.RoleId)
                                  .FirstOrDefault();

            int userId = _context.userdetails
                                  .Where(u => u.Email == login.Email && u.UserPassword == login.UserPassword)
                                  .Select(u => u.UserId)
                                  .FirstOrDefault();

            bool status = _context.userdetails .Where(s => s.UserId == userId) .Select(s => s.IsActive).FirstOrDefault();
 
            if (userId != 0 && status !=false)
            {
                
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

                
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, roleid.ToString())
        };

               
                var Sectoken = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Issuer"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

                return token;
            }

            return "Invalid";
        }



    }
}

