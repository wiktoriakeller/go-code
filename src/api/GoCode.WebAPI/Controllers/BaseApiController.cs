namespace GoCode.WebAPI.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected readonly IMediator _medaitor;
        protected readonly IMapper _mapper;

        public BaseApiController(IMediator mediator, IMapper mapper)
        {
            _medaitor = mediator;
            _mapper = mapper;
        }

        protected IActionResult GetStatusCode(IResponse response) => StatusCode((int)response.HttpStatusCode, response);
    }
}
