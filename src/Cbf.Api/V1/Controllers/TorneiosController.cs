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
    [Route("api/v{version:apiVersion}/torneios")]
    public class TorneiosController : MainController
    {
        private readonly ITorneioService _torneioService;
        private readonly IMapper _mapper;

        public TorneiosController(ITorneioService torneioService,
                                  IMapper mapper,
                                  INotificador notificador) : base(notificador)
        {
            _torneioService = torneioService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status412PreconditionFailed, Type = typeof(IEnumerable<TorneioViewModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IEnumerable<TorneioViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<TorneioViewModel>>(await _torneioService.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<TorneioViewModel>> ObterPorId(Guid id)
        {
            var torneio = _mapper.Map<TorneioViewModel>(await _torneioService.ObterPorId(id));

            if (torneio == null) return NotFound();

            return torneio;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<TorneioViewModel>> Adicionar(TorneioViewModel torneioViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _torneioService.Adicionar(_mapper.Map<Torneio>(torneioViewModel));

            return CustomResponse(torneioViewModel);
        }

        [HttpPost("{torneioId:guid}/{timeId:guid}")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<TorneioViewModel>> AdicionarTime(Guid torneioId, Guid timeId)
        {
            await _torneioService.AdicionarTime(torneioId, timeId);

            return CustomResponse();
        }

        [HttpPut("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<TorneioViewModel>> Atualizar(Guid id, TorneioViewModel torneioViewModel)
        {
            if (id != torneioViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(torneioViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _torneioService.Atualizar(_mapper.Map<Torneio>(torneioViewModel));

            return CustomResponse(torneioViewModel);
        }

        [HttpDelete("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<TorneioViewModel>> Excluir(Guid id)
        {
            var torneioViewModel = await _torneioService.ObterPorId(id);

            if (torneioViewModel == null) return NotFound();

            await _torneioService.Remover(id);

            return CustomResponse(torneioViewModel);
        }
    }
}
