using ApplicationLayer.partonair.DTOs;


namespace ApplicationLayer.partonair.Interfaces
{
    public interface IEvaluationService
    {
        Task<EvaluationViewDTO> CreateAsyncService(Guid idOwner, EvaluationCreateDTO eval);
        Task DeleteAsyncService(Guid id);
        Task<EvaluationViewDTO> UpdateAsyncService(Guid idEval, EvaluationUpdateDTO eval);


        Task<EvaluationViewDTO> GetByGuidAsyncService(Guid id);
        Task<ICollection<EvaluationViewDTO>> GetAllGrouppByUserAsyncService();
    }
}
