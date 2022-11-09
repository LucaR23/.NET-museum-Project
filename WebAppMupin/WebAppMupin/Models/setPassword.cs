using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMupin.Models
{
    public class setPassword
    {
        public string utente { get; set; }
        public string Password { get; set; }
        public string repeatPassword { get; set; }
        public string errorMessage { get; set; }

    }
}