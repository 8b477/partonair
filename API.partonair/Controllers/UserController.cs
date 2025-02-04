using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Exceptions;
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

        [HttpPost]
        public async Task<IActionResult> AddAsync(UserCreateDTO user)
        {
            try
            {
                var result = await _userService.CreateAsync(user);

                return Ok(result);
            }
            catch (ApplicationLayerException){ throw; }
            catch (Exception){ throw; }
        }

        #endregion



        #region <-------------> GET <------------->

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var user = await _userService.GetByIdAsyncService(id);

                return Ok(user);
            }
            catch (InfrastructureLayerException){ throw; }
            catch (Exception) { throw; }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var users = await _userService.GetAllAsync();

                return Ok(users);
            }
            catch (Exception){ throw; }
        }

        #endregion



        #region <-------------> UPDATE <------------->



        #endregion



        #region <-------------> DELETE <------------->



        #endregion
    }
}
