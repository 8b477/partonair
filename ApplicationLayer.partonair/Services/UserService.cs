using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.Queries.Users;

using MediatR;

namespace ApplicationLayer.partonair.Services
{
    public class UserService(IMediator mediator) : IUserService
    {
        private readonly IMediator _mediator = mediator;

        public async Task<ICollection<UserViewDTO>> GetAllAsync()
        {
            return await _mediator.Send(new GetAllUserQuery()); 
        }

        public async Task<UserViewDTO> GetUserByIdAsync(Guid id)
        {
            return await _mediator.Send(new GetUserByIdQuery(id));
        }
    }    
}
