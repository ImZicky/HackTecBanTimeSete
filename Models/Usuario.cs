using HackTecBanTimeSete.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackTecBanTimeSete.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; }
        public AuthDeUser Tipo { get; set; }


    }
}
