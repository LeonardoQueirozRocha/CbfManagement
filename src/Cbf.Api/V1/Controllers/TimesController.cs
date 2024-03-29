﻿using AutoMapper;
using Cbf.Api.Controllers;
using Cbf.Api.ViewModels;
using Cbf.Business.Interfaces;
using Cbf.Business.Interfaces.Repositories;
using Cbf.Business.Interfaces.Services;
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
        private readonly IMapper _mapper;

        public TimesController(ITimeService timeService,
                               IMapper mapper,
                               INotificador notificador) : base(notificador)
        {
            _timeService = timeService;            
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status412PreconditionFailed, Type = typeof(IEnumerable<TimeViewModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IEnumerable<TimeViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<TimeViewModel>>(await _timeService.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<TimeViewModel>> ObterPorId(Guid id)
        {
            var time = _mapper.Map<TimeViewModel>(await _timeService.ObterPorId(id));

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

        [HttpPost("transferencias")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
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

            await _timeService.Atualizar(_mapper.Map<Time>(timeViewModel));

            return CustomResponse(timeViewModel);
        }

        [HttpDelete("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<TimeViewModel>> Excluir(Guid id)
        {
            var timeViewModel = _mapper.Map<TimeViewModel>(await _timeService.ObterPorId(id));

            if (timeViewModel == null) return NotFound();

            await _timeService.Remover(id);

            return CustomResponse(timeViewModel);
        }
    }
}
