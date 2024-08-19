using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Data.Entities
{
    public class Perfil
    {
        #region Propriedades
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        #endregion

        #region Relacionamento
        public List<Usuario>? Usuarios { get; set; }
        #endregion

    }
}
