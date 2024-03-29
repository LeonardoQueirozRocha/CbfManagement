﻿using System.ComponentModel.DataAnnotations;

namespace Cbf.Api.ViewModels
{
    public class TimeViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(255, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Localidade { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Tecnico { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime Fundacao { get; set; }

        public string Estadio { get; set; }

        public IEnumerable<JogadorViewModel> Jogadores { get; set; }
    }
}
