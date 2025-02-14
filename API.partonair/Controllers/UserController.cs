using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using Microsoft.AspNetCore.Mvc;


namespace API.partonair.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {

        // <--------------------------------> TODO <-------------------------------->
        // Need review later when Authentication is setup for retrieve some data by token
        // <--------------------------------> **** <-------------------------------->



        #region DI

        private readonly IUserService _userService = userService;

        #endregion



        #region <-------------> CREATE <------------->

        [HttpPost]
        public async Task<IActionResult> AddAsync(UserCreateDTO u)
        {
            var user = await _userService.CreateAsyncService(u);

            return
                user is not null
                ? Ok(new { user })
                : BadRequest();
        }

        #endregion



        #region <-------------> GET <------------->

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
                var user = await _userService.GetByIdAsyncService(id);

                return Ok(new { user });        
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsyncService();

            return Ok(new { users });
        }

        [HttpGet("Name")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var users = await _userService.GetByNameAsyncService(name);

            return 
                users is null 
                ? NoContent()
                : Ok(new { users } );
        }

        [HttpGet("Role")]
        public async Task<IActionResult> GetByRoleAsync(string role)
        {
            var users = await _userService.GetByRoleAsyncService(role);

            return
                users is null
                ? NoContent()
                : Ok(new { users });
        }

        [HttpGet("Email")]
        public async Task<IActionResult> GetByEmailAsync(string email)
        {
            var user = await _userService.GetByEmailAsyncService(email);

            return
                user is null
                ? NoContent()
                : Ok(new { user });
        }

        #endregion



        #region <-------------> UPDATE <------------->

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UserUpdateNameOrMailOrPasswordDTO u)
        {
            var user = await _userService.UpdateService(id, u);

            return
                user is null
                ? BadRequest()
                : Ok(new { user });
        }

        [HttpPut("Role")]
        public async Task<IActionResult> Role(UserChangeRoleDTO user)
        {
            // Retrieve id by header (token)
            Guid id = new Guid("e7f614c8-6b60-4f91-8038-017bea60ccec"); // Temporary !!
            await _userService.ChangeRoleService(id,user);

            return Ok(new { Message = "Successful role update !" });
        }

        #endregion



        #region <-------------> DELETE <------------->

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteService(id);
            return NoContent();
        }

        #endregion
    }
}
