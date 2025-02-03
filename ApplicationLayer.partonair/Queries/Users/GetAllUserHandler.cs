using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Contracts;

using MediatR;

namespace ApplicationLayer.partonair.Queries.Users
{
    public class GetAllUserHandler(IUnitOfWork UOW) : IRequestHandler<GetAllUserQuery, ICollection<UserViewDTO>>
    {
        private readonly IUnitOfWork _UOW = UOW;
        public async Task<ICollection<UserViewDTO>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _UOW.Users.GetAllAsync();

            var result = users.Select(user => new UserViewDTO(
                                                            user.Id,
                                                            user.UserName,
                                                            user.Email,
                                                            user.IsPublic,
                                                            user.UserCreatedAt,
                                                            user.LastConnection,
                                                            user.Role.ToString(),
                                                            user.FK_Profile
                                                            )).ToList();

            return result;
        }
    }
}
