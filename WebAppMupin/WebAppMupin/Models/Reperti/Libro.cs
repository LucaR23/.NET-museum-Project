using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMupin.Models.Reperti
{
  
    public class Libro
    {
        public string Identificativo { get; set; }
        public string Titolo { get; set; }
        public string autori { get; set; }
        public string CasaEditrice { get; set; }
        public string AnnoPubblicazione { get; set; }
        public string numeroPagine { get; set; }
        public string ISBN { get; set; }


    }
}