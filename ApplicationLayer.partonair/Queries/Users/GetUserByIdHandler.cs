using ApplicationLayer.partonair.DTOs;
using DomainLayer.partonair.Contracts;

using MediatR;

namespace ApplicationLayer.partonair.Queries.Users
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserViewDTO?>
    {
        private readonly IUser _userRepo;
        public GetUserByIdHandler(IUser userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserViewDTO?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepo.GetByIdAsync(request.Id);
            if (user == null) return null;

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
