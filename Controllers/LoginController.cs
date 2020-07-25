using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackTecBanTimeSete.Areas.Identity.Data;
using HackTecBanTimeSete.DTO;
using HackTecBanTimeSete.Exceptions;
using HackTecBanTimeSete.Models;
using HackTecBanTimeSete.Repository;
using HackTecBanTimeSete.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HackTecBanTimeSete.Controllers
{
    public class LoginController : Controller
    {

        private readonly UserManager<HackTecBanTimeSeteUser> _userManager;
        private readonly SignInManager<HackTecBanTimeSeteUser> _signInManager;
        private readonly ILogger<LoginController> _logger;
        private readonly IUsuarioRepository _rep;

        public LoginController(UserManager<HackTecBanTimeSeteUser> userManager, SignInManager<HackTecBanTimeSeteUser> signInManager, ILogger<LoginController> logger, IUsuarioRepository rep)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _rep = rep;
        }

        /// <summary> Aqui vc faz o login no sistema e de quebra ainda pega um Bearer Token JWT, meu consagrado :P </summary>
        /// <param name="user"></param>
        /// <returns> O DTO (Data Transfer Object) de UsuarioLoginDTO</returns>
        /// <response code="200">Sucesso (GGWP)</response>
        [HttpPost]
        [AllowAnonymous]
        [Route("/api/v1/user/GetTokenLogin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<TokenUserDTO> GetTokenLogin([FromBody] UsuarioLoginDTO user)
        {
            try
            {
                /* FAZ O LOGIN */
                var userExists = await _signInManager.PasswordSignInAsync(user.Email, user.Password, true, false);

                TokenUserDTO token = null;
                if (userExists.Succeeded)
                {
                    /* Ele existe... logo loga... */
                    Usuario usuario = await _rep.GetUserByEmail(user.Email);
                    var tokenGerado = GetAuth.GenerateToken(usuario);
                    token = new TokenUserDTO()
                    {
                        TokenUrsao = tokenGerado,
                    };
                    return token;
                }
                else 
                {
                    throw new UsuarioNaoEncontradoException("usuario nao encontrado :P");
                }
            }
            catch (Exception e)
            {
                return new TokenUserDTO()
                {
                    TokenUrsao = "Puuuxa não foi dessa vez... Mas ligue novamente para 4002-8922 q vc será atendido pelo SBT! --- SACANAGEM... KKKKK na real a msg é essa mano: Ocorreu algum erro interno na aplicação, por favor tente novamente... Erro: " + e.Message,
                };
            }
        }


        private Exception Exception()
        {
            throw new NotImplementedException();
        }
    }
}
