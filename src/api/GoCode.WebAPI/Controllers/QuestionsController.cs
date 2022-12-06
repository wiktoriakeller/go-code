using GoCode.Application.Questions.Commands;
using GoCode.Application.Questions.Requests;
using Microsoft.AspNetCore.Authorization;

namespace GoCode.WebAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/v1/questions")]
    public class QuestionsController : BaseApiController
    {
        public QuestionsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Create question, only for admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequest request)
        {
            var command = _mapper.Map<CreateQuestionCommand>(request);
            var response = await _medaitor.Send(command);
            return GetStatusCode(response);
        }

        /// <summary>
        /// Delete question, only for admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion([FromRoute] int id)
        {
            var command = new DeleteQuestionCommand() { Id = id };
            var response = await _medaitor.Send(command);
            return GetStatusCode(response);
        }

        /// <summary>
        /// Update question, only for admin
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion([FromRoute] int id, [FromBody] UpdateQuestionRequest request)
        {
            var command = _mapper.Map<UpdateQuestionCommand>(request);
            command.Id = id;
            var response = await _medaitor.Send(command);
            return GetStatusCode(response);
        }
    }
}
