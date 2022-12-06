using GoCode.Application.Form.Commands;
using GoCode.Application.Form.Requests;
using Microsoft.AspNetCore.Authorization;

namespace GoCode.WebAPI.Controllers
{
    [Authorize]
    [Route("api/v1/forms")]
    public class FormController : BaseApiController
    {
        public FormController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Check user answers for quiz
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> EvaluateForm([FromBody] EvaluateFormRequest request)
        {
            var command = _mapper.Map<EvaluateFormCommand>(request);
            var response = await _medaitor.Send(command);
            return GetStatusCode(response);
        }
    }
}
