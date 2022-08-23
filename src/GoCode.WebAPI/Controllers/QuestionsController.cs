using GoCode.Application.Questions.Commands;
using GoCode.Application.Questions.Requests;

namespace GoCode.WebAPI.Controllers
{
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
    }
}
