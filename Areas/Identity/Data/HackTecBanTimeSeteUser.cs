using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackTecBanTimeSete.Token;
using Microsoft.AspNetCore.Identity;

namespace HackTecBanTimeSete.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the HackTecBanTimeSeteUser class
    public class HackTecBanTimeSeteUser : IdentityUser
    {
        public AuthDeUser Tipo { get; set; }




    }
}
