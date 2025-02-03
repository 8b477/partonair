using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Contracts;

using MediatR;

namespace ApplicationLayer.partonair.Queries.Users
{
    public class GetUserByIdHandler(IUnitOfWork userRepo) : IRequestHandler<GetUserByIdQuery, UserViewDTO?>
    {
        private readonly IUnitOfWork _UOW = userRepo;

        public async Task<UserViewDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _UOW.Users.GetByIdAsync(request.Id);

            return new UserViewDTO
            (
                user.Id,user.UserName,
                user.Email,
                user.IsPublic,
                 user.UserCreatedAt,
                 user.LastConnection,
                 user.Role.ToString(),
                 user.FK_Profile,
                 null
            );
        }
    }
    
}
