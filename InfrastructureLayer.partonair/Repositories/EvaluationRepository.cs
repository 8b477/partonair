using DomainLayer.partonair.Contracts;
using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

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

        public override async Task<Evaluation> GetByGuidAsync(Guid id)
        {
            var result = await _dbSet
                                    .Where(e => e.Id == id)
                                    .Include(e => e.Owner)
                                    .Include(e => e.Sender)
                                    .FirstOrDefaultAsync();

            return result ?? throw new InfrastructureLayerException(InfrastructureLayerErrorType.EntityIsNullException, $"Identifier : {id} - No match");
        }

    }
}
