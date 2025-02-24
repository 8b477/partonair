using DomainLayer.partonair.Contracts;
using DomainLayer.partonair.Entities;

using InfrastructureLayer.partonair.Persistence;

namespace InfrastructureLayer.partonair.Repositories
{
    public class ContactRepository(AppDbContext ctx) : GenericRepository<Contact>(ctx), IContactRepository
    {
    }
}
