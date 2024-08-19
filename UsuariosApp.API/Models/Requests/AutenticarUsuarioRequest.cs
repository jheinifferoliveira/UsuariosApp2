using System.ComponentModel.DataAnnotations;

namespace UsuariosApp.API.Models.Requests
{
    public class AutenticarUsuarioRequest
    {
        [EmailAddress(ErrorMessage ="Por favor, digite um endereço de email válido.")]
        [Required(ErrorMessage ="Por favor, digite o email do usuário.")]
        public string? Email{ get; set; }

        [MinLength(8,ErrorMessage ="Por favor, informe a senha com no mínimo {1} caracteres.")]
        [Required(ErrorMessage ="Por favor, informe a senha do usuário.")]
        public string? Senha { get; set; }
    }
}
