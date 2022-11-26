using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMupin.Models.Reperti
{

    public class Rivista
    {
        public string Identificativo { get; set; }
        public string Titolo { get; set; }
        public string numeroRivista { get; set; }
        public string Anno { get; set; }
        public string casaEditrice { get; set; }
       // public RepertoDetail detail { get; set; }

        public string Insert(Rivista r)
        {
            string query = "INSERT INTO riviste (Identificativo,titolo,numero_rivista,anno,casa_editrice) VALUES ('"
           + r.Identificativo + "','" + r.Titolo + "','" + r.numeroRivista + "','" + r.Anno + "','" + r.casaEditrice + "');";
            return query;
        }
        public string Update(Rivista r)
        {
            string query = "UPDATE riviste SET titolo='" + r.Titolo + "'," + "numero_rivista='" + r.numeroRivista + "'," + "anno=' " + r.Anno + "'," + "+ casa_editrice='" + r.casaEditrice + 
            "' WHERE Identificativo= '" + r.Identificativo + "' ;";
            return query;
        }
    }
}