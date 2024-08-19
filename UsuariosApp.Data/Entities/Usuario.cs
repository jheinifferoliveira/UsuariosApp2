using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Data.Entities
{
    public class Usuario
    {
        #region Propridades
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        #endregion

        #region Relacionamento
        public Perfil? Perfil { get; set; }
        #endregion

        #region Chave estrangeira
        public Guid PerfilId { get; set; }
        #endregion


    }
}
