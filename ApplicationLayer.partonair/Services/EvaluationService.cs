using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.Mappers;

using DomainLayer.partonair.Contracts;
using DomainLayer.partonair.Entities;


namespace ApplicationLayer.partonair.Services
{
    public class EvaluationService(IUnitOfWork UOW) : IEvaluationService
    {
        private readonly IUnitOfWork _UOW = UOW;


        #region COMMANDS
        public async Task<EvaluationViewDTO> CreateAsyncService(Guid idOwner, EvaluationCreateDTO eval)
        {
            try
            {
                await _UOW.BeginTransactionAsync();

                var existingOwner = await _UOW.Users.GetByGuidAsync(idOwner);
                var existingSender = await _UOW.Users.GetByGuidAsync(eval.Id_Sender);

                Evaluation entity = eval.ToEntity(existingOwner, existingSender);

                var created = await _UOW.Evaluations.CreateAsync(entity);

                await _UOW.SaveChangesAsync();
                await _UOW.CommitTransactionAsync();

                return created.ToView();
            }
            catch
            {
                await _UOW.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task<EvaluationViewDTO> UpdateAsyncService(Guid idEval, EvaluationUpdateDTO eval)
        {
            try
            {
                await _UOW.BeginTransactionAsync();

                var existingEvaluation = await _UOW.Evaluations.GetByGuidAsync(idEval);

                Evaluation evalToUpdate = CompareAndUpdateValueNotNull(existingEvaluation, eval);

                var updated = await _UOW.Evaluations.Update(evalToUpdate);

                await _UOW.SaveChangesAsync();
                await _UOW.CommitTransactionAsync();

                return updated.ToView();
            }
            catch
            {
                await _UOW.RollbackTransactionAsync();
                throw;
            }
        }

        public async Task DeleteAsyncService(Guid id)
        {
            try
            {
                await _UOW.BeginTransactionAsync();

                var existingEval = await _UOW.Evaluations.GetByGuidAsync(id);

                await _UOW.Evaluations.Delete(existingEval.Id);
                await _UOW.SaveChangesAsync();
                await _UOW.CommitTransactionAsync();

                return;
            }
            catch
            {
                await _UOW.RollbackTransactionAsync();
                throw;
            }
        }

        #endregion



        #region QUERIES

        public async Task<ICollection<EvaluationViewDTO>> GetAllGrouppByUserAsyncService()
        {
            var result = await _UOW.Evaluations.GetAllAsync();

            return result
                        .Select(e => e.ToView())
                        .ToList();
        }

        public async Task<EvaluationViewDTO> GetByGuidAsyncService(Guid id)
        {
            var result = await _UOW.Evaluations.GetByGuidAsync(id);

            return result.ToView();
        }

        #endregion



        #region PRIVATE METHODS
        private static Evaluation CompareAndUpdateValueNotNull(Evaluation existingEval, EvaluationUpdateDTO newEval)
        {
            existingEval.EvaluationCommentary = newEval.Commentary ?? existingEval.EvaluationCommentary;
            existingEval.EvaluationValue = newEval.Value ?? existingEval.EvaluationValue;
            existingEval.EvaluationUpdatedAt = DateTime.Now;

            return existingEval;
        }
        #endregion


    }
}
