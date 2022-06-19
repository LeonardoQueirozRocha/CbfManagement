using AutoMapper;
using Cbf.Api.Controllers;
using Cbf.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cbf.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/times")]
    public class TimesController : MainController
    {
        private readonly ITimeService _timeService;
        private readonly ITimeRepository _timeRepository;
        private readonly IMapper _mapper;

        public TimesController(ITimeService timeService,
                               ITimeRepository timeRepository,
                               IMapper mapper)
        {
            _timeService = timeService;
            _timeRepository = timeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok();
        }
    }
}
