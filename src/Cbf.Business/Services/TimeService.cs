using Cbf.Business.Interfaces;
using Cbf.Business.Interfaces.Repositories;
using Cbf.Business.Interfaces.Services;
using Cbf.Business.Models;
using Cbf.Business.Models.Validations;

namespace Cbf.Business.Services
{
    public class TimeService : BaseService, ITimeService
    {
        private readonly ITimeRepository _timeRepository;
        private readonly ITransferenciaRepository _transferenciaRepository;
        private readonly IJogadorRepository _jogadorRepository;

        public TimeService(ITimeRepository timeRepository,
                           ITransferenciaRepository transferenciaRepository,
                           IJogadorRepository jogadorRepository,
                           INotificador notificador) : base(notificador)
        {
            _timeRepository = timeRepository;
            _transferenciaRepository = transferenciaRepository;
            _jogadorRepository = jogadorRepository;
        }

        public async Task Adicionar(Time time)
        {
            if (!ExecutarValidacao(new TimeValidation(), time)) return;

            if (_timeRepository.Buscar(f => f.Nome == time.Nome).Result.Any())
            {
                Notificar("Já existe um time com este nome informado.");
                return;
            }

            await _timeRepository.Adicionar(time);
        }

        public async Task Atualizar(Time time)
        {
            if (_timeRepository.Buscar(t => t.Nome == time.Nome).Result.Any())
            {
                Notificar("Já existe um time com este nome cadastrado.");
                return;
            }

            await _timeRepository.Atualizar(time);
        }

        public async Task FazerTransferencia(Transferencia transferencia)
        {
            if (!ExecutarValidacao(new TransferenciaValidation(), transferencia)) return;

            if (!await ValidarTransferencia(transferencia)) return;

            await _transferenciaRepository.Adicionar(transferencia);

            var jogador = await _jogadorRepository.ObterPorId(transferencia.JogadorId);
            jogador.TimeId = transferencia.TimeDestinoId;
            await _jogadorRepository.Atualizar(jogador);
        }

        public async Task Remover(Guid id)
        {
            var jogadores = await _jogadorRepository.ObterJogadoresPorTime(id);

            if (jogadores.Any())
            {
                Notificar("Não é possível excluir esse time, pois tem jogadores cadastrados a ele.");
                return;
            }

            var transferencias = await _transferenciaRepository.ObterTransferenciasPorTimeOrigem(id);

            if (transferencias.Any())
            {
                transferencias.ToList().ForEach(t => _transferenciaRepository.Remover(t.Id));
            }

            await _timeRepository.Remover(id);
        }

        public void Dispose()
        {
            _timeRepository.Dispose();
            _transferenciaRepository.Dispose();
        }

        private async Task<bool> ValidarTransferencia(Transferencia transferencia)
        {
            var origem = await _timeRepository.ObterPorId(transferencia.TimeOrigemId);

            if (origem == null)
            {
                Notificar("Time de origem não encontrado.");
                return false;
            }

            var destino = await _timeRepository.ObterPorId(transferencia.TimeDestinoId);

            if (destino == null)
            {
                Notificar("Time de destino não encontrado.");
                return false;
            }

            var jogador = await _jogadorRepository.ObterPorId(transferencia.JogadorId);

            if (jogador == null)
            {
                Notificar("Jogador não encontrado.");
                return false;
            }

            return true;
        }
    }
}
