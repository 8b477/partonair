﻿using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;

namespace ApplicationLayer.partonair.MediatR.Queries.Users
{
    public class GetByNameUserQueryHandler(IUserService userService) : IRequestHandler<GetByNameUserQuery, ICollection<UserViewDTO>>
    {
        private readonly IUserService _userService = userService;

        public async Task<ICollection<UserViewDTO>> Handle(GetByNameUserQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetByNameAsyncService(request.Name);
        }
    }
}
