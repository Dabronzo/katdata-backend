using katdata.Features.Models;
using katdata.Services;
using Microsoft.AspNetCore.Mvc;

namespace katdata.Features.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            var user = await _userService.RegisterUserAsync(dto.Email, dto.Password, dto.Role);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user is { })
            {
                var mappedUser = new UserDTO()
                {
                    Email = user.Email,
                    Role = user.Role,
                    Id = id
                };
                return Ok(mappedUser);
            }
            return NotFound();
        }

        /*
       [HttpGet]
       public async Task<IActionResult> GetAllUsers()
       {
           var users = await _userService.GetAllUsersAsync();
           return Ok(users);
       }

       [HttpDelete("{id:guid}")]
       public async Task<IActionResult> DeleteUser(Guid id)
       {
           await _userService.DeleteUserAsync(id);
           return NoContent();
       }
       */
    }

    public record RegisterUserDto(string Email, string Password, UserRoles Role);
}
