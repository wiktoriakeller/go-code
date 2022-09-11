using GoCode.Application.Form.Commands;
using GoCode.Application.Form.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace GoCode.WebAPI.Controllers
{
    [Authorize]
    [Route("api/v1/form")]
    [EnableCors("mobile")]
    public class FormController : BaseApiController
    {
        public FormController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost("evaluate")]
        public async Task<IActionResult> EvaluateForm([FromBody] EvaluateFormRequest request)
        {
            var command = _mapper.Map<EvaluateFormCommand>(request);
            var response = await _medaitor.Send(command);
            return GetStatusCode(response);
        }
    }
}
