using HackTecBanTimeSete.Areas.Identity.Data;
using HackTecBanTimeSete.Data;
using HackTecBanTimeSete.DTO;
using HackTecBanTimeSete.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackTecBanTimeSete.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetUserByEmail(string email);
        Task CadUsuario(UsuarioCadDTO user);
        Task DelUsuario(UsuarioDelDTO user);
    }


    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly HackTecBanTimeSeteContext _ctx;
        private readonly SignInManager<HackTecBanTimeSeteUser> _signInManager;
        private readonly UserManager<HackTecBanTimeSeteUser> _userManager;
        private readonly ILogger<UsuarioRepository> _logger;

        public UsuarioRepository(HackTecBanTimeSeteContext ctx, SignInManager<HackTecBanTimeSeteUser> signInManager, UserManager<HackTecBanTimeSeteUser> userManager, ILogger<UsuarioRepository> logger)
        {
            _ctx = ctx;
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public object Request { get; private set; }

        public async Task CadUsuario(UsuarioCadDTO user)
        {
            /* Cadastra no AspUsers */
            var usuarioIdt = new HackTecBanTimeSeteUser { UserName = user.Email, Email = user.Email, Tipo = user.Tipo };
            usuarioIdt.EmailConfirmed = true; // pra n ter q validar esse isiota aqui :P
            usuarioIdt.TwoFactorEnabled = false;

            var result = await _userManager.CreateAsync(usuarioIdt, user.Password);
            if (result.Succeeded)
            {

                /* Cadastra na Aplicacao */
                var usuarioNosso = new Usuario()
                {
                    Email = user.Email,
                    Tipo = user.Tipo,
                };

                _ctx.Usuarios.Add(usuarioNosso);
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task DelUsuario(UsuarioDelDTO user)
        {
         
            var usuario = await _ctx.Usuarios
                .Where(u => u.UsuarioId == user.UserId)
                .FirstOrDefaultAsync();
            
            _ctx.Usuarios.Remove(usuario);
            await _ctx.SaveChangesAsync();
        }

        public async Task<Usuario> GetUserByEmail(string email)
        {
            return await _ctx.Usuarios.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();
        }





    }
}
