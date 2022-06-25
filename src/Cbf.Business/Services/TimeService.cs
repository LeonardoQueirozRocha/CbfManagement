using Cbf.Business.Interfaces;
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

        public async Task FazerTransferencia(string timeOrigem, string timeDestino, string jogador)
        {
            var origem = await _timeRepository.Buscar(t => t.Nome == timeOrigem);

            if (!origem.Any())
            {
                Notificar("Time de origem não encontrado.");
                return;
            }

            var destino = await _timeRepository.Buscar(t => t.Nome == timeDestino);

            if (!destino.Any())
            {
                Notificar("Time de destino não encontrado.");
                return;
            }

            var jogadorBase = await _jogadorRepository.Buscar(j => j.Nome == jogador);

            if (!jogadorBase.Any())
            {
                Notificar("Jogador não encontrado.");
                return;
            }

            var transferencia = new Transferencia
            {
                TimeOrigemId = origem.FirstOrDefault().Id,
                TimeDestinoId = destino.FirstOrDefault().Id,
                JogadorId = jogadorBase.FirstOrDefault().Id
            };

            await _transferenciaRepository.Adicionar(transferencia);
        }

        public async Task Remover(Guid id)
        {
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
    }
}
