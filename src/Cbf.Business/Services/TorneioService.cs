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
        private readonly ITimeRepository _timeRepository;

        public TorneioService(ITorneioRepository torneioRepository,
                              ITimeRepository timeRepository,
                              INotificador notificador) : base(notificador)
        {
            _torneioRepository = torneioRepository;
            _timeRepository = timeRepository;
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

        public async Task AdicionarTime(Guid torneioId, Guid timeId)
        {
            if (!await ValidarCadastroTimeTorneio(torneioId, timeId)) return;

            var torneio = await _torneioRepository.ObterPorId(torneioId);
            var time = await _timeRepository.ObterPorId(timeId);

            torneio.Times.Add(time);
            await _torneioRepository.Atualizar(torneio);
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
            if (_torneioRepository.ObterTorneioTimes(id).Result.Times.Any())
            {
                Notificar("O torneio tem times cadastrados.");
                return;
            }

            await _torneioRepository.Remover(id);
        }

        public void Dispose()
        {
            _torneioRepository.Dispose();
        }

        private async Task<bool> ValidarCadastroTimeTorneio(Guid torneioId, Guid timeId)
        {
            var isValid = true;

            var torneio = await _torneioRepository.ObterPorId(torneioId);
            if (torneio == null)
            {
                Notificar("Torneio não encontrado.");
                isValid = false;
            }

            var time = await _timeRepository.ObterPorId(timeId);
            if (time == null)
            {
                Notificar("Time não encontrado.");
                isValid = false;
            }

            var timeTorneio = await _torneioRepository.Buscar(t => t.Times.Where(t => t.Id == timeId).Any());
            if (timeTorneio.Any())
            {
                Notificar("Time já cadastrado no torneio");
                isValid = false;
            }

            return isValid;
        }
    }
}
