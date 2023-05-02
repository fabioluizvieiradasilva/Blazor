﻿using System.ComponentModel.DataAnnotations;

namespace Catalogo_Blazor.Shared.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }
        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [MaxLength(100)]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(200)]
        public string? Descricao { get; set; }
        public ICollection<Produto>? Produtos { get; set; }
    }
}
