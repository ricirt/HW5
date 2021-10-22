using System.Threading.Tasks;
using Bugs_N_Roses.Application.Models.UserModels;
using Bugs_N_Roses.Application.Services.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bugs_N_Roses.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUsers()
        {
            var users=_userService.GetAll();
            return Ok(users);
        }
        
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUser(int userId)
        {
            var user=_userService.GetById(userId);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddUser([FromBody]UserCreateDTO userCreateDto)
        {
            var user=_userService.Add(userCreateDto);
            if (user)
            {
                return NoContent();
            }
            return BadRequest();
        }
        
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(int userId)
        {
            var user=_userService.Delete(userId);
            if (user)
            {
                return NoContent();
            }
            return BadRequest();
        }
        
        [HttpPut("{userId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUser([FromBody]UserUpdateDTO userUpdateDto, int userId)
        {
            var user=_userService.Update(userUpdateDto,userId);
            if (user)
            {
                return NoContent();
            }
            return BadRequest();
        }
        
    }
}