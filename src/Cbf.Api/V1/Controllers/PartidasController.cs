using AutoMapper;
using Cbf.Api.Controllers;
using Cbf.Api.ViewModels;
using Cbf.Business.Interfaces;
using Cbf.Business.Interfaces.Services;
using Cbf.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Cbf.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/partidas")]
    public class PartidasController : MainController
    {
        private readonly IPartidaService _partidaService;
        private readonly IMapper _mapper;

        public PartidasController(IPartidaService partidaService, 
                                  IMapper mapper, 
                                  INotificador notificador) : base(notificador)
        {
            _partidaService = partidaService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status412PreconditionFailed, Type = typeof(IEnumerable<PartidaViewModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IEnumerable<PartidaViewModel>> ObterPartidasTorneios()
        {
            return _mapper.Map<IEnumerable<PartidaViewModel>>(await _partidaService.ObterPartidasTorneios());
        }

        [HttpGet("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<PartidaViewModel>> ObterPartidaTorneio(Guid id)
        {
            var partida = _mapper.Map<PartidaViewModel>(await _partidaService.ObterPartidaTorneio(id));

            if (partida == null) return NotFound();

            return partida;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<PartidaViewModel>> Adicionar(PartidaViewModel partidaViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _partidaService.Adicionar(_mapper.Map<Partida>(partidaViewModel));

            return CustomResponse(partidaViewModel);
        }

        [HttpPut("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<PartidaViewModel>> Atualizar(Guid id, PartidaViewModel partidaViewModel)
        {
            if (id != partidaViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(partidaViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _partidaService.Atualizar(_mapper.Map<Partida>(partidaViewModel));

            return CustomResponse(partidaViewModel);
        }

        [HttpDelete("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<PartidaViewModel>> Excluir(Guid id)
        {
            var partidaViewModel = await _partidaService.ObterPartidaTorneio(id);

            if (partidaViewModel == null) return NotFound();

            await _partidaService.Remover(id);

            return CustomResponse(partidaViewModel);
        }
    }
}
