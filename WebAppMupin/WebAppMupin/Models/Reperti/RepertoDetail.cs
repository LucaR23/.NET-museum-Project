using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMupin.Models.Reperti
{
    public class RepertoDetail
    {
        public string Id { get; set; }
        public string Note { get; set; }
        public string Url { get; set; }
        public string Tag { get; set; }
        public byte[] Immagine { get; set; }

        public string Insert(RepertoDetail rd)
        {
            string query = "INSERT INTO repertodetail (identificativoreperto,note,URL,tag,immagine) VALUES ('"
    + rd.Id + "','" + rd.Note + "','" + rd.Url + "','" + rd.Tag + "','" + rd.Immagine + "');";
            return query;
        }
    }
}