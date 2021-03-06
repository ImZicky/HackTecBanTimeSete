﻿using HackTecBanTimeSete.Token;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HackTecBanTimeSete.DTO
{
    public class UsuarioCadDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public AuthDeUser Tipo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "A {0} deve ter {2} caracteres  e no maximo {1}.", MinimumLength = 6)]
        public string Password { get; set; }

    }
}
