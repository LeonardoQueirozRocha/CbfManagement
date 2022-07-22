using Cbf.Business.Interfaces;
using Cbf.Business.Interfaces.Repositories;
using Cbf.Business.Interfaces.Services;
using Cbf.Business.Models;
using Cbf.Business.Models.Validations;

namespace Cbf.Business.Services
{
    public class TorneioService : BaseService, ITorneioService
    {
        private readonly ITorneioRepository _torneioRepository;

        public TorneioService(ITorneioRepository torneioRepository,
                              INotificador notificador) : base(notificador)
        {
            _torneioRepository = torneioRepository;
        }

        public async Task<IEnumerable<Torneio>> ObterTodos()
        {
            return await _torneioRepository.ObterTodos();
        }

        public async Task<Torneio> ObterPorId(Guid id)
        {
            return await _torneioRepository.ObterPorId(id);
        }

        public async Task Adicionar(Torneio torneio)
        {
            if (!ExecutarValidacao(new TorneioValidation(), torneio)) return;

            if (_torneioRepository.Buscar(t => t.Nome == torneio.Nome).Result.Any())
            {
                Notificar("Já existe um torneio com este nome informado.");
                return;
            }

            await _torneioRepository.Adicionar(torneio);
        }

        public async Task Atualizar(Torneio torneio)
        {
            if (_torneioRepository.Buscar(t => t.Nome == torneio.Nome).Result.Any())
            {
                Notificar("Já existe um torneio com este nome cadastrado.");
                return;
            }

            await _torneioRepository.Atualizar(torneio);
        }

        public async Task Remover(Guid id)
        {
            await _torneioRepository.Remover(id);
        }

        public void Dispose()
        {
            _torneioRepository.Dispose();
        }
    }
}
