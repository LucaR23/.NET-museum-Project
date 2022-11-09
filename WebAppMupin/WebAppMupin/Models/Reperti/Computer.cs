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
    }
}