﻿using Cbf.Business.Interfaces;
using Cbf.Business.Models;
using Cbf.Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace Cbf.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador) => _notificador = notificador;

        protected void Notificar(ValidationResult validationResult) => validationResult.Errors.ForEach(error => Notificar(error.ErrorMessage));

        protected void Notificar(string mensagem) => _notificador.Handle(new Notificacao(mensagem));

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
