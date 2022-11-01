using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Models;
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
        public async Task<List<User>> GetUsersAsync(UserFilter userFilter)
        {
            return await _userService.GetUserAsync(userFilter);
        }

        [HttpGet("GetUserByIdAsync")]
        public async Task<User> GetUsersByIdAsync(Guid userId)
        {
            return await _userService.GetUserByIdAsync(userId);
        }

        [HttpPost]
        public async Task AddUserAsync(User user)
        {
            await _userService.AddAsync(user);
        }

        [HttpDelete]
        public async Task DeleteUserAsync(Guid userId)
        {
            await _userService.DeleteAsync(userId);
        }

        [HttpPut]
        public async Task UpdateUserAsync(Guid userId, User user)
        {
            await _userService.UpdateAsync(userId, user);
        }
    }
}
