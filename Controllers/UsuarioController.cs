using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HackTecBanTimeSete.Models;
using Microsoft.AspNetCore.Authorization;
using HackTecBanTimeSete.Repository;
using HackTecBanTimeSete.DTO;

namespace HackTecBanTimeSete.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioRepository _repUser;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository repUser)
        {
            _logger = logger;
            _repUser = repUser;
        }


        public IActionResult Index()
        {
            return View();
        }




        [HttpGet]
        [Route("/api/v1/user/GetUsuarioPadrao")]
        [Authorize(Roles = "Agricultor,Agente")]
        public IActionResult GetUsuarioPadrao()
        {
            return Ok("Está logado como padrão: " + User.Identity.Name);
        }


        /// <response code="401">Não falo com bandeirantes... Vc não está autorizado à essa requisição...</response>
        [HttpGet]
        [Route("/api/v1/user/GetUsuarioBanco")]
        [Authorize(Roles = "Banco")]
        public IActionResult GetUsuarioBanco()
        {
            return Ok("Está logado como banco: " + User.Identity.Name);
        }


        [HttpPost]
        [Route("/api/v1/user/CadUsuario")]
        public async Task CadUsuario([FromBody] UsuarioCadDTO user)
        {
            await _repUser.CadUsuario(user);
        }


        [HttpDelete]
        [Route("/api/v1/user/DelUsuario")]
        [Authorize(Roles = "Admin")]
        public async Task DelUsuario([FromBody] UsuarioDelDTO user)
        {
            await _repUser.DelUsuario(user);
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
