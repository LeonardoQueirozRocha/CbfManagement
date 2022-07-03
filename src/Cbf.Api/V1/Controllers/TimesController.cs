﻿using AutoMapper;
using Cbf.Api.Controllers;
using Cbf.Api.ViewModels;
using Cbf.Business.Interfaces;
using Cbf.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
                               IMapper mapper,
                               INotificador notificador) : base(notificador)
        {
            _timeService = timeService;
            _timeRepository = timeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status412PreconditionFailed, Type = typeof(IEnumerable<TimeViewModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IEnumerable<TimeViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<TimeViewModel>>(await _timeRepository.ObterTimesJogadores());
        }

        [HttpGet("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<TimeViewModel>> ObterPorId(Guid id)
        {
            var time = await ObterTimeJogadores(id);

            if (time == null) return NotFound();

            return time;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<TimeViewModel>> Adicionar(TimeViewModel timeViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _timeService.Adicionar(_mapper.Map<Time>(timeViewModel));

            return CustomResponse(timeViewModel);
        }

        [HttpPost("transferencia")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<TransferenciaViewModel>> Transferencia(TransferenciaViewModel transferenciaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _timeService.FazerTransferencia(_mapper.Map<Transferencia>(transferenciaViewModel));

            return CustomResponse(transferenciaViewModel);
        }

        [HttpPut("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<TimeViewModel>> Atualizar(Guid id, TimeViewModel timeViewModel)
        {
            if (id != timeViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(timeViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _timeRepository.Atualizar(_mapper.Map<Time>(timeViewModel));

            return CustomResponse(timeViewModel);
        }

        [HttpDelete("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<TimeViewModel>> Excluir(Guid id)
        {
            var timeViewModel = await ObterTimeJogadores(id);

            if (timeViewModel == null) return NotFound();

            await _timeService.Remover(id);

            return CustomResponse(timeViewModel);
        }

        private async Task<TimeViewModel> ObterTimeJogadores(Guid id)
        {
            return _mapper.Map<TimeViewModel>(await _timeRepository.ObterTimeJogadores(id));
        }
    }
}
