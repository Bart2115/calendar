using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        public ApiBaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
