using Cbf.Business.Interfaces;
using Cbf.Business.Interfaces.Repositories;
using Cbf.Business.Interfaces.Services;
using Cbf.Business.Models;
using Cbf.Business.Models.Validations;

namespace Cbf.Business.Services
{
    public class PartidaService : BaseService, IPartidaService
    {
        private readonly IPartidaRepository _partidaRepository;

        public PartidaService(IPartidaRepository partidaRepository,
                              INotificador notificador) : base(notificador)
        {
            _partidaRepository = partidaRepository;
        }

        public async Task<IEnumerable<Partida>> ObterTodos()
        {
            return await _partidaRepository.ObterTodos();
        }

        public async Task<Partida> ObterPorId(Guid id)
        {
            return await _partidaRepository.ObterPorId(id);
        }

        public async Task<Partida> ObterPartidaTorneio(Guid id)
        {
            return await _partidaRepository.ObterPartidaTorneio(id);
        }

        public async Task Adicionar(Partida partida)
        {
            if (!ExecutarValidacao(new PartidaValidation(), partida)) return;

            await _partidaRepository.Adicionar(partida);
        }

        public async Task Atualizar(Partida partida)
        {
            if (!ExecutarValidacao(new PartidaValidation(), partida)) return;

            await _partidaRepository.Atualizar(partida);
        }

        public async Task Remover(Guid id)
        {
            await _partidaRepository.Remover(id);
        }

        public void Dispose()
        {
            _partidaRepository?.Dispose();
        }
    }
}
