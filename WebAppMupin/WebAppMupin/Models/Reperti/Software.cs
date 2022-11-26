using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMupin.Models.Reperti
{
    public class Software
    {
        public string Identificativo { get; set; }
        public string Titolo { get; set; }
        public string sistemaOperativo { get; set; }
        public string tipoSoftware { get; set; }
        public string supporto { get; set; }
       // public RepertoDetail detail { get; set; }

        public string Insert(Software s)
        {
            string query = "INSERT INTO software (Identificativo,titolo,sistema_operativo,tipo_software,supporto) VALUES ('"
        + s.Identificativo + "','" + s.Titolo + "','" + s.sistemaOperativo + "','" + s.tipoSoftware + "','" + s.supporto + "');";
            return query;
        }

        public string Update(Software s)
        {
            string query = "UPDATE software SET titolo='" + s.Titolo + "'," + "sistema_operativo='" + s.sistemaOperativo + "'," + "tipo_software=' " + s.tipoSoftware + "'," + "+ supporto='" + s.supporto 
                + "' WHERE Identificativo= '" + s.Identificativo + "' ;";
            return query;
        }
    }
}