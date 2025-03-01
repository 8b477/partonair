using ApplicationLayer.partonair.DTOs;


namespace ApplicationLayer.partonair.Interfaces
{
    public interface IEvaluationService
    {
        Task<EvaluationViewDTO> CreateAsyncService(EvaluationCreateDTO eval);
        Task DeleteAsyncService(Guid id);
        Task<EvaluationViewDTO> UpdateAsyncService(EvaluationUpdateDTO eval);


        Task<EvaluationViewDTO> GetByGuidAsyncService(Guid id);
        Task<ICollection<EvaluationViewDTO>> GetAllFilteredbyUserAsyncService();
    }
}
