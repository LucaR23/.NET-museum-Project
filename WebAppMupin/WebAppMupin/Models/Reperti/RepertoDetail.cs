using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMupin.Models.Reperti
{
    public class RepertoDetail
    {
        public string Note { get; set; }
        public string Url { get; set; }
        public string Tag { get; set; }
        public byte[] Immagine { get; set; }
    }
}