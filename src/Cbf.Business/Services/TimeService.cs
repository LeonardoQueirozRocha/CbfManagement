using Cbf.Business.Interfaces;
using Cbf.Business.Models;

namespace Cbf.Business.Services
{
    public class TimeService : ITimeService
    {
        private readonly ITimeRepository _timeRepository;
        private readonly ITransferenciaRepository _transferenciaRepository;
        private readonly IJogadorRepository _jogadorRepository;

        public TimeService(ITimeRepository timeRepository, 
                           ITransferenciaRepository transferenciaRepository,
                           IJogadorRepository jogadorRepository)
        {
            _timeRepository = timeRepository;
            _transferenciaRepository = transferenciaRepository;
            _jogadorRepository = jogadorRepository;
        }

        public async Task Adicionar(Time time)
        {
            if (_timeRepository.Buscar(t => t.Nome == time.Nome).Result.Any())
            {
                throw new Exception("Já existe um time com esse nome cadastrado.");
            }

            await _timeRepository.Adicionar(time);
        }

        public async Task Atualizar(Time time)
        {
            if (_timeRepository.Buscar(t => t.Nome == time.Nome).Result.Any())
            {
                throw new Exception("Já existe um time com esse nome cadastrado.");
            }

            await _timeRepository.Atualizar(time);
        }

        public async Task FazerTransferencia(string timeOrigem, string timeDestino, string jogador)
        {
            var origem = await _timeRepository.Buscar(t => t.Nome == timeOrigem);

            if (!origem.Any())
            {
                throw new Exception("Time de origem não encontrado.");
            }

            var destino = await _timeRepository.Buscar(t => t.Nome == timeDestino);

            if (!destino.Any())
            {
                throw new Exception("Time de destino não encontrado.");
            }

            var jogadorBase = await _jogadorRepository.Buscar(j => j.Nome == jogador);

            if (!jogadorBase.Any())
            {
                throw new Exception("Jogador não encontrado na base.");
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
