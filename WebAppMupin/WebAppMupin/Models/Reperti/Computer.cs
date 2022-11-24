using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMupin.Models.Reperti
{

    public class Computer
    {
        public string Identificativo { get; set; }
        public string NomeModello { get; set; }
        public string Anno { get; set; }
        public string Cpu { get; set; }
        public string VelocitaHz { get; set; }
        public string Ram { get; set; }
        public string HardDisk { get; set; }    
        public string SistemaOperativo { get; set; }
      
        public string Insert(Computer c)
        {
            string query = "INSERT INTO computer (Identificativo,Nome_modello,anno,CPU,velocita_HZ,RAM,Hard_disk,sistema_operativo) VALUES ('"
            + c.Identificativo+ "','" + c.NomeModello+ "','" + c.Anno+ "','" + c.Cpu+ "','" + c.VelocitaHz+ "','" + c.Ram+ "','" + c.HardDisk+ "','" + c.SistemaOperativo + "');";
                return query;
        }

        public string Update(Computer c)
        {
            string query = "UPDATE computer SET Nome_modello='"  + c.NomeModello + "'," +"anno='" + c.Anno + "'," + "CPU=' "+ c.Cpu + "'," +"+ velocita_HZ='"+ c.VelocitaHz + "',"+"RAM='"
                + c.Ram + "'," + "Hard_disk='" + c.HardDisk + "'," +"sistema_operativo='" + c.SistemaOperativo + "'WHERE Identificativo= '"+c.Identificativo +"' ;";
            return query;
        }
    }
}