using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ModelsDb;
using Services;
using Services.Filters;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace AdvertBoardAPI.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private UserService _userService { get; set; }

        public UserController()
        {
            _userService = new UserService();
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsersAsync(UserFilter userFilter)
        {
            var getUser = await _userService.GetUserAsync(userFilter);

            if (getUser != null)
            {
                return getUser;
            }

            return new BadRequestObjectResult(ModelState);
        }

        [HttpGet("GetUserByIdAsync")]
        public async Task<ActionResult<User>> GetUsersByIdAsync(Guid userId)
        {
            var getUser = await _userService.GetUserByIdAsync(userId);

            if (getUser != null)
            {
                return getUser;
            }

            return new BadRequestObjectResult(ModelState);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(User user)
        {
            await _userService.AddAsync(user);

            if (await _userService.GetUserByIdAsync((Guid)user.Id) != null)
            {
                return Ok();
            }

            return new BadRequestObjectResult(ModelState);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserAsync(Guid userId)
        {
            await _userService.DeleteAsync(userId);

            var getUser = await _userService.GetUserAsync(new UserFilter() { Id = userId });

            if (getUser.Count != 1)
            {
                return Ok();
            }

            return new BadRequestObjectResult(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(Guid userId, User user)
        {
            await _userService.UpdateAsync(userId, user);

            if (await _userService.GetUserByIdAsync(userId) == user)
            {
                return Ok();
            }

            return new BadRequestObjectResult(ModelState);
        }
    }
}
