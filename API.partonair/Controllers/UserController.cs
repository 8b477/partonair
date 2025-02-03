using ApplicationLayer.partonair.Interfaces;

using InfrastructureLayer.partonair.Exceptions;

using Microsoft.AspNetCore.Mvc;


namespace API.partonair.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {

        // <--------------------------------> TODO <-------------------------------->
        // 
        // <--------------------------------> **** <-------------------------------->



        #region DI

        private readonly IUserService _userService = userService;

        #endregion



        #region <-------------> CREATE <------------->



        #endregion



        #region <-------------> GET <------------->

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);

                return Ok(user);
            }
            catch (InfrastructureException)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var users = await _userService.GetAllAsync();

                return Ok(users);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*
        [HttpGet]
        public IActionResult GetAllAsync()
        {
            var users = Task.FromResult(_userService.GetAllAsync());
            return Ok();
        }
         */
        #endregion



        #region <-------------> UPDATE <------------->



        #endregion



        #region <-------------> DELETE <------------->



        #endregion
    }
}
