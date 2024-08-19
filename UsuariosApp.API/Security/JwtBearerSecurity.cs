﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApp.Data.Entities;

namespace UsuariosApp.API.Security
{
    public class JwtBearerSecurity
    {
        #region Propriedades

        public static string SecretKey => "963DC579-BA5D-4A8D-81DD-19CD6991CEE4";
        public static int ExpirationInHours => 1;

        #endregion

        public static string CreateToken(Usuario usuario)
        {
            //capturando a chave para assinar o token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);

            //gerando o conteúdo do token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //identificação do usuário autenticado
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Email)
                }),

                //data de expiração do token
                Expires = DateTime.UtcNow.AddHours(ExpirationInHours),

                //criptografando a chave para assinatura do token
                SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //gerando e retornando o token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
