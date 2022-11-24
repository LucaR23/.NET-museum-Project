using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMupin.Models.Reperti
{
  
    public class Periferica
    {
        public string Identificativo { get; set; }
        public string nomeModello { get; set; }
        public string Tipologia { get; set; }
        //public RepertoDetail detail { get; set; }

        public string Insert(Periferica p)
        {
            string query = "INSERT INTO periferiche (Identificativo,nome_modello,tipologia) VALUES ('"
           + p.Identificativo + "','" + p.nomeModello + "','" + p.Tipologia + "');";
            return query;
        }
    }
}