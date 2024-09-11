using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsArena.Models.DTOModels;
using SportsArena.Services.Services.Interface;

namespace SportsArenaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IAdminSer _ser;
        public AdminController(IAdminSer ser)
        {
            _ser = ser;
        }

        [HttpGet("UserDetails")]
        public List<AdminModel> GetAllUsers()
        {
          return _ser.GetAllUsers();
        }

        [HttpGet("BuyerDetails")]

        public List<AdminModel> GetBuyers()
        {
           return _ser.GetBuyers();
        }

        [HttpGet("SellerDetails")]

        public List<AdminModel> GetSellers()
        {
           return _ser.GetSellers();
        }

        [HttpPut("UpdateStatus{userId}")]
        public ActionResult UpdateStatus(int userId, [FromBody] StatusUpdate request)
        {
            var res = _ser.UpdateStatus(userId, request.Status);
            if (!res)
            {
                return NotFound(new { Message = "No User found" });
            }
            return Ok(new { Message = "Successful" });
        }
    }
}
