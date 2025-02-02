using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.Queries.Users;

using MediatR;


namespace ApplicationLayer.partonair.Services
{
    public class UserService : IUserService
    {
        private readonly IMediator _mediator;

        public UserService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<UserViewDTO> GetUserByIdAsync(Guid id)
        {
            return await _mediator.Send(new GetUserByIdQuery(id));
        }
    }

    
}
