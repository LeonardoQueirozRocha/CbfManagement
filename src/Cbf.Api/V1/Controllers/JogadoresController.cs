using AutoMapper;
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
    [Route("api/v{version:apiVersion}/jogadores")]
    public class JogadoresController : MainController
    {
        private readonly IJogadorService _jogadorService;
        private readonly IMapper _mapper;

        public JogadoresController(INotificador notificador,
                                   IJogadorService jogadorService,
                                   IMapper mapper) : base(notificador)
        {
            _jogadorService = jogadorService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status412PreconditionFailed, Type = typeof(IEnumerable<JogadorViewModel>))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IEnumerable<JogadorViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<JogadorViewModel>>(await _jogadorService.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<JogadorViewModel>> ObterPorId(Guid id)
        {
            var jogadorViewModel = _mapper.Map<JogadorViewModel>(await _jogadorService.ObterPorId(id));

            if (jogadorViewModel == null) return NotFound();

            return jogadorViewModel;
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<JogadorViewModel>> Adicionar(JogadorViewModel jogadorViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _jogadorService.Adicionar(_mapper.Map<Jogador>(jogadorViewModel));

            return CustomResponse(jogadorViewModel);
        }

        [HttpPut("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<JogadorViewModel>> Atualizar(Guid id, JogadorViewModel jogadorViewModel)
        {
            if (id != jogadorViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(jogadorViewModel);
            }

            var jogadorAtualizacao = _mapper.Map<JogadorViewModel>(await _jogadorService.ObterPorId(id));

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            jogadorAtualizacao.TimeId = jogadorViewModel.TimeId;
            jogadorAtualizacao.Nome = jogadorViewModel.Nome;
            jogadorAtualizacao.DataNascimento = jogadorViewModel.DataNascimento;
            jogadorAtualizacao.Pais = jogadorViewModel.Pais;
            jogadorAtualizacao.Salario = jogadorViewModel.Salario;
            jogadorViewModel.Posicao = jogadorViewModel.Posicao;

            await _jogadorService.Atualizar(_mapper.Map<Jogador>(jogadorAtualizacao));

            return CustomResponse(jogadorViewModel);
        }

        [HttpDelete("{id:guid}")]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<ActionResult<JogadorViewModel>> Excluir(Guid id)
        {
            var jogadorViewModel = _mapper.Map<JogadorViewModel>(await _jogadorService.ObterPorId(id));

            if (jogadorViewModel == null) return NotFound();

            await _jogadorService.Remover(id);

            return CustomResponse(jogadorViewModel);
        }
    }
}
