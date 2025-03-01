using DomainLayer.partonair.Contracts;
using DomainLayer.partonair.Entities;

using InfrastructureLayer.partonair.Persistence;

using Microsoft.EntityFrameworkCore;


namespace InfrastructureLayer.partonair.Repositories
{
    public class EvaluationRepository(AppDbContext ctx) : GenericRepository<Evaluation>(ctx), IEvaluationRepository
    {
        public async override Task<ICollection<Evaluation>> GetAllAsync()
        {
            var result = await _dbSet
                                    .OrderBy(e => e.Owner)
                                    .Include(e =>e.Owner)
                                    .Include(e => e.Sender)
                                    .ToListAsync();

            return result ?? [];
        }
    }
}
