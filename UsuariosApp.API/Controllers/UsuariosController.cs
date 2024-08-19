using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsuariosApp.API.Models.Requests;
using UsuariosApp.API.Models.Responses;
using UsuariosApp.API.Security;
using UsuariosApp.Data.Entities;
using UsuariosApp.Data.Helpers;
using UsuariosApp.Data.Repositories;

namespace UsuariosApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [HttpPost]
        [Route("criar")]
        public IActionResult Criar(CriarUsuarioRequest request)
        {
            try
            {
                var usuarioRepository = new UsuarioRepository();
                var perfilRepository=new PerfilRepository();

                if (usuarioRepository.GetByEmail(request.Email) !=null )
                {
                    return StatusCode(400, new { message = " O email informado já está cadastrado. Tente outro." });
                }

                var usuario=new Usuario();
                usuario.Id=Guid.NewGuid();
                usuario.Nome=request.Nome;
                usuario.Email = request.Email;
                usuario.Senha= SHA256CryptoHelper.Encrypt(request.Senha);

                var perfil = perfilRepository.GetByNome("USER");

                usuario.PerfilId = perfil.Id;

                usuarioRepository.Add(usuario);

                var response=new CriarUsuarioResponse();
                response.Id=usuario.Id;
                response.Nome = usuario.Nome;
                response.Email = usuario.Email;
                response.DataHoraCadastro=DateTime.Now;

                return StatusCode(201,response);
            }
            catch(Exception e)
            {
              return StatusCode(500 , new { e.Message });
            }

        }

        [HttpPost]
        [Route("autenticar")]
        public IActionResult Autenticar(AutenticarUsuarioRequest request)
        {
            try
            {
                var usuarioRepository=new UsuarioRepository();
                var usuario = usuarioRepository.GetByEmailAndNome(request.Email,SHA256CryptoHelper.Encrypt( request.Senha));

                if(usuario == null)
                {
                    return StatusCode(401, new { message = "Acesso negado. Usuário não encontrado." });
                }
                else
                {
                    var response = new AutenticarUsuarioResponse();
                    response.Id= usuario.Id;
                    response.Nome= usuario.Nome;
                    response.Email = usuario.Email;
                    response.DataHoraAcesso=DateTime.Now;
                    response.AcessToken = JwtBearerSecurity.CreateToken(usuario);
                    response.DataHoraExpiracao=DateTime.Now.AddHours(JwtBearerSecurity.ExpirationInHours);

                    return StatusCode(200,response);
                }

            }
            catch(Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet]
        [Route("obter-dados")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
