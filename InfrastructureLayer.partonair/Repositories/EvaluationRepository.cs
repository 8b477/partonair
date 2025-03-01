using DomainLayer.partonair.Contracts;
using DomainLayer.partonair.Entities;

using InfrastructureLayer.partonair.Persistence;


namespace InfrastructureLayer.partonair.Repositories
{
    public class EvaluationRepository(AppDbContext ctx) : GenericRepository<Evaluation>(ctx), IEvaluationRepository
    {
    }
}
