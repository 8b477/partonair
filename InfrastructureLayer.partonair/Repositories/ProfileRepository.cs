using DomainLayer.partonair.Contracts;
using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;

using InfrastructureLayer.partonair.Persistence;

namespace InfrastructureLayer.partonair.Repositories
{
    public class ProfileRepository : GenericRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(AppDbContext ctx) : base(ctx)
        {
        }

    }
}
