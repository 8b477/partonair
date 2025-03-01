using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Commands.Evaluations;
using ApplicationLayer.partonair.MediatR.Queries.Evaluations;

using MediatR;

using Microsoft.AspNetCore.Mvc;


namespace API.partonair.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByGuidAsync(Guid id)
        {
            var evaluation = await _mediator.Send(new GetByGuidEvaluationQuery(id));
            
            return 
                  evaluation is null 
                ? NoContent()
                : Ok(new {evaluation} );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var evaluations = await _mediator.Send(new GetAllEvaluationFilteredbyUserQuery());

            return
                  evaluations is null 
                ? NoContent() 
                : Ok(new { evaluations } );
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(EvaluationCreateDTO eval)
        {
            var evaluationCreated = await _mediator.Send(new AddEvaluationCommand(eval));

            return Ok(evaluationCreated);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateAsync(EvaluationUpdateDTO eval)
        {
            var evaluationCreated = await _mediator.Send(new UpdateEvaluationCommand(eval));

            return Ok(evaluationCreated);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _mediator.Send(new DeleteEvaluationCommand(id));

            return NoContent();
        }
    }
}
