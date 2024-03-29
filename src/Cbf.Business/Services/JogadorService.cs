﻿using Cbf.Business.Interfaces;
using Cbf.Business.Interfaces.Repositories;
using Cbf.Business.Interfaces.Services;
using Cbf.Business.Models;
using Cbf.Business.Models.Validations;

namespace Cbf.Business.Services
{
    public class JogadorService : BaseService, IJogadorService
    {
        private readonly IJogadorRepository _jogadorRepository;

        public JogadorService(IJogadorRepository jogadorRepository,
                              INotificador notificador) : base(notificador)
        {
            _jogadorRepository = jogadorRepository;
        }

        public async Task<IEnumerable<Jogador>> ObterTodos()
        {
            return await _jogadorRepository.ObterTodos();
        }

        public async Task<Jogador> ObterPorId(Guid id)
        {
            return await _jogadorRepository.ObterJogadorTime(id);
        }

        public async Task Adicionar(Jogador jogador)
        {
            if (!ExecutarValidacao(new JogadorValidation(), jogador)) return;

            await _jogadorRepository.Adicionar(jogador);
        }

        public async Task Atualizar(Jogador jogador)
        {
            if (!ExecutarValidacao(new JogadorValidation(), jogador)) return;

            await _jogadorRepository.Atualizar(jogador);
        }

        public async Task Remover(Guid id)
        {
            await _jogadorRepository.Remover(id);
        }

        public void Dispose()
        {
            _jogadorRepository?.Dispose();
        }
    }
}
