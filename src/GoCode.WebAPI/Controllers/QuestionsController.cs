using GoCode.Application.Questions.Commands;
using GoCode.Application.Questions.Requests;
using Microsoft.AspNetCore.Authorization;

namespace GoCode.WebAPI.Controllers
{
    [Authorize]
    [Route("api/v1/questions")]
    public class QuestionsController : BaseApiController
    {
        public QuestionsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest request)
        {
            var command = _mapper.Map<CreateQuestionCommand>(request);
            var response = await _medaitor.Send(command);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] int id)
        {
            var command = new DeleteQuestionCommand() { Id = id };
            var response = await _medaitor.Send(command);
            return StatusCode((int)response.HttpStatusCode, response);
        }
    }
}
