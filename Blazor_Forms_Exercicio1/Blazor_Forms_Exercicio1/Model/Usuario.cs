using System.ComponentModel.DataAnnotations;

namespace Blazor_Forms_Exercicio1.Model
{
    public class Usuario
    {
        [Required(ErrorMessage ="O campo nome é obrigatório")]
        [StringLength(10, ErrorMessage ="O tamanho do nome deve ter no máximo 10 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage ="O campo sobrenome é obrigatório")]
        [StringLength(10, ErrorMessage ="O tamanho do sobrenome deve ter no máximo 10 caracteres")]
        public string Sobrenome { get; set; }
        [Required]
        [Range(18,100,ErrorMessage ="O campo idade deve ter entre 18 e 100")]
        public int Idade { get; set; }
        [Required(ErrorMessage ="O campo login é obrigatório")]
        [MinLength(4, ErrorMessage ="O campo login deve ter no mínimo 4 caracters")]
        public string Login { get; set; }
        [Required(ErrorMessage = "O campo Email é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo Email está inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo perfil é obrigatório")]
        public string Perfil { get; set; }
        [Required(ErrorMessage = "O campo Password é obrigatório")]
        [MinLength(4, ErrorMessage = "O campo password deve ter no mínimo 4 caracters")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "O campo ConfirmaPassword é obrigatório")]
        [MinLength(4, ErrorMessage = "O campo ConfirmaPassword deve ter no mínimo 4 caracters")]
        [Compare(nameof(Password), ErrorMessage ="O campo Confirma Password e Password não conferem")]
        public string ConfirmaPassword { get; set; } = string.Empty;
    }
}
