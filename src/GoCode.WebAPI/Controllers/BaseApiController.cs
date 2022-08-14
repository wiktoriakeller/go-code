using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoCode.WebAPI.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected readonly IMediator _medaitor;
        protected readonly IMapper _mapper;

        public BaseApiController(IMediator mediator, IMapper mapper)
        {
            _medaitor = mediator;
            _mapper = mapper;
        }
    }
}
