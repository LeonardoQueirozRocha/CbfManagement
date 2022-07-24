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
        private readonly ITimeRepository _timeRepository;
        private readonly ITorneioRepository _torneioRepository;

        public PartidaService(IPartidaRepository partidaRepository,
                              ITimeRepository timeRepository,
                              ITorneioRepository torneioRepository,
                              INotificador notificador) : base(notificador)
        {
            _partidaRepository = partidaRepository;
            _timeRepository = timeRepository;
            _torneioRepository = torneioRepository;
        }

        public async Task<IEnumerable<Partida>> ObterPartidasTorneios()
        {
            return await _partidaRepository.ObterPartidasTorneios();
        }

        public async Task<Partida> ObterPartidaTorneio(Guid id)
        {
            return await _partidaRepository.ObterPartidaTorneio(id);
        }

        public async Task Adicionar(Partida partida)
        {
            if (!ExecutarValidacao(new PartidaValidation(), partida)) return;

            if (!await ValidarPartida(partida)) return;

            await _partidaRepository.Adicionar(partida);
        }

        public async Task Atualizar(Partida partida)
        {
            if (!ExecutarValidacao(new PartidaValidation(), partida)) return;

            if (!await ValidarPartida(partida)) return;

            await _partidaRepository.Atualizar(partida);
        }

        public async Task Remover(Guid id)
        {
            await _partidaRepository.Remover(id);
        }

        public void Dispose()
        {
            _partidaRepository?.Dispose();
            _timeRepository?.Dispose();
            _torneioRepository?.Dispose();
        }

        private async Task<bool> ValidarPartida(Partida partida)
        {
            var isValid = true;

            var timeCasa = await _timeRepository.ObterPorId(partida.TimeCasaId);
            if (timeCasa == null)
            {
                Notificar("Time da casa não encontrado");
                isValid = false;
            }

            var timeVisitante = await _timeRepository.ObterPorId(partida.TimeVisitanteId);
            if (timeVisitante == null)
            {
                Notificar("Time visitante não encontrado");
                isValid = false;
            }

            var torneio = await _torneioRepository.ObterPorId(partida.TorneioId);
            if (torneio == null)
            {
                Notificar("Torneio não encontrado");
                isValid = false;
            }

            return isValid;
        }
    }
}
