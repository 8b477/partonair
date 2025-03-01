using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using DomainLayer.partonair.Contracts;


namespace ApplicationLayer.partonair.Services
{
    public class EvaluationService(IUnitOfWork UOW) : IEvaluationService
    {
        private readonly IUnitOfWork _UOW = UOW;

        public Task<EvaluationViewDTO> CreateAsyncService(EvaluationCreateDTO eval)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsyncService(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<EvaluationViewDTO>> GetAllFilteredbyUserAsyncService()
        {
            throw new NotImplementedException();
        }

        public Task<EvaluationViewDTO> GetByGuidAsyncService(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EvaluationViewDTO> UpdateAsyncService(EvaluationUpdateDTO eval)
        {
            throw new NotImplementedException();
        }
    }
}
