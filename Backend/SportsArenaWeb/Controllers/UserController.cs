using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsArena.Models.DbModels;
using SportsArena.Models.DTOModels;
using SportsArena.Services.Services.Interface.IUser;
using System.Security.Claims;

namespace SportsArenaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IuserSer _ser;
        public UserController(IuserSer ser)
        {
            _ser = ser;
        }

        [HttpGet("Profile")]
        [Authorize]
        public ActionResult<SportsDto> GetUserByID(int id)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null || !int.TryParse(userId, out int currentUserId))
            {
                return Unauthorized();
            }


            if (currentUserId != id)
            {
                return Forbid();
            }

            var userDto = _ser.GetUserByID(id);
            return Ok(userDto);
        }


        [HttpPost("EditProfile")]
        [Authorize]

        public IActionResult EditProfile(UpdateProfileDto data)
        {
            if(data == null)
            {
                return BadRequest("Profile data  cannot be null.");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userId, out int parsedUserId))
            {
                return Unauthorized("User is not authenticated.");
            }


            var result = _ser.EditProfile(data,parsedUserId);
            return Ok(result);

        }



        [HttpPost("AddProduct")]
        [Authorize]
        public IActionResult AddProduct(productDto user)
        {
            if (user == null)
            {
                return BadRequest("Product data cannot be null.");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userId, out int parsedUserId))
            {
                return Unauthorized("User is not authenticated.");
            }


            var result = _ser.AddProduct(user, parsedUserId);
            return Ok(result);
            
        }

        [HttpGet("GetProducts")]
        public List<productDto> GetProducts()
        {
            return _ser.GetProducts();
        }

        [HttpDelete("DelProduct/{productId}")]
       [Authorize]
        public ActionResult DeleteStatus(int productId)
        {
            string res = _ser.DeleteProduct(productId);
            if (res != null)
            {
                return Ok(new { message = "Product deleted successfully" });
            }
            return BadRequest(new { message = "Error deleting product" });
        }




    }
}



