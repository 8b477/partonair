using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.Queries.Users;

using InfrastructureLayer.partonair.Exceptions;

using MediatR;

using Microsoft.AspNetCore.Mvc;


namespace API.partonair.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        // <--------------------------------> TODO <-------------------------------->
        // 
        // <--------------------------------> **** <-------------------------------->



        #region DI

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

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
        #endregion



        #region <-------------> UPDATE <------------->



        #endregion



        #region <-------------> DELETE <------------->



        #endregion
    }
}
