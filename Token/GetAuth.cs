using HackTecBanTimeSete.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HackTecBanTimeSete.Token
{
    public class GetAuth
    {


        public static string GenerateToken(Usuario user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            //chave secreta, geralmente se coloca em arquivo de configuração
            var key = Encoding.ASCII.GetBytes("ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=DATTEBAYO");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email.ToString()),
                    new Claim(ClaimTypes.Role, RoleFactory(user.Tipo))
                }),
                Expires = DateTime.UtcNow.AddHours(10),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static string RoleFactory(AuthDeUser roleNumber)
        {
            switch (roleNumber)
            {
                case AuthDeUser.Agente:
                    return "Agente";

                case AuthDeUser.Agricultor:
                    return "Agricultor";

                case AuthDeUser.Banco:
                    return "Banco";
             
                case AuthDeUser.Admin:
                    return "Admin";

                default:
                    throw new Exception();
            }
        }


    }
}
