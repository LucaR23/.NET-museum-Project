using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using WebAppMupin.Models.Reperti;

namespace WebAppMupin.Controllers
{
    public class InsertController : Controller
    {

        public ActionResult New(string ins)
        {
            MySqlConnection conn = UtilityDB.connection();
            List<string> tab = UtilityReperti.getTableName(conn);
            if (tab.Contains(ins))
            {
                if (ins == "computer")
                {
                    Computer c = new Computer();
                    return PartialView("InserisciReperto/_inserisciComputer", c);
                }
                if (ins == "libri")
                {
                    Libro l = new Libro();
                    return PartialView("InserisciReperto/_inserisciLibri", l);
                }
                if (ins == "periferiche")
                {
                    Periferica p = new Periferica();
                    return PartialView("InserisciReperto/_inserisciPeriferiche", p);
                }
                if (ins == "riviste")
                {
                    Rivista r = new Rivista();
                    return PartialView("InserisciReperto/_inserisciRiviste", r);
                }
                if (ins == "software")
                {
                    Software s = new Software();
                    return PartialView("InserisciReperto/_inserisciSoftware", s);
                }
                if (ins == "repertodetail")
                {
                    RepertoDetail rd = new RepertoDetail();
                    return PartialView("InserisciReperto/_inserisciDetail", rd);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();      // -> not Used 
        }

        public ActionResult InsertComputer(Computer c)
        {
            if (c.Identificativo == null || c.NomeModello == null || c.Anno==null || c.SistemaOperativo == null || c.Cpu==null || c.VelocitaHz ==null)   // varifico che i campi siano compilati
            {
                return Json("compilare i campi necessari");
            }

            bool esiste = existReperto(c.Identificativo, "computer");
            if (esiste)
            {
                return Json("identificativo già presente");
            }

            string query = c.Insert(c); 
            bool inserisci = doInsert(query);
            if (inserisci)
                return Json("inserito nuovo reperto con identificativo " + c.Identificativo.ToString()) ;
            else
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }
        public ActionResult InsertLibro(Libro l)
        {
            if (l.Identificativo == null || l.ISBN == null || l.Titolo == null || l.autori== null || l.CasaEditrice == null || l.AnnoPubblicazione == null || l.numeroPagine==null)   
            {
                return Json("compilare i campi necessari");
            }

            bool esiste = existReperto(l.Identificativo, "libri");
            if (esiste)
            {
                return Json("identificativo già presente");
            }

            string query = l.Insert(l);
            bool inserisci = doInsert(query);
            if (inserisci)
                return Json("inserito nuovo reperto con identificativo " + l.Identificativo.ToString());
            else
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }
        public ActionResult InsertPeriferica(Periferica p)
        {
            if (p.Identificativo == null || p.nomeModello == null || p.Tipologia == null)   
            {
                return Json("compilare i campi necessari");
            }

            bool esiste = existReperto(p.Identificativo, "periferiche");
            if (esiste)
            {
                return Json("identificativo già presente");
            }

            string query = p.Insert(p);
            bool inserisci = doInsert(query);
            if (inserisci)
                return Json("inserito nuovo reperto con identificativo " + p.Identificativo.ToString());
            else
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }
        public ActionResult InsertRivista(Rivista r)
        {
            if (r.Identificativo == null || r.Titolo == null || r.numeroRivista == null || r.Anno == null || r.casaEditrice == null)   
            {
                return Json("compilare i campi necessari");
            }

            bool esiste = existReperto(r.Identificativo, "riviste");
            if (esiste)
            {
                return Json("identificativo già presente");
            }

            string query = r.Insert(r);
            bool inserisci = doInsert(query);
            if (inserisci)
                return Json("inserito nuovo reperto con identificativo " + r.Identificativo.ToString());
            else
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }
        public ActionResult InsertSoftware(Software s)
        {
            if (s.Identificativo == null || s.supporto == null || s.sistemaOperativo == null || s.Titolo == null || s.tipoSoftware == null)
            {
                return Json("compilare i campi necessari");
            }

            bool esiste = existReperto(s.Identificativo, "riviste");
            if (esiste)
            {
                return Json("identificativo già presente");
            }

            string query = s.Insert(s);
            bool inserisci = doInsert(query);
            if (inserisci)
                return Json("inserito nuovo reperto con identificativo " + s.Identificativo.ToString());
            else
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
        }
        public ActionResult InsertDetail(string id, string note, string url, string tag)
        {
            RepertoDetail rd = new RepertoDetail();
            rd.Id = id.ToString();
            rd.Note = note.ToString();
            rd.Url = url.ToString();
            rd.Tag = tag.ToString();

            string query = rd.Insert(rd);

            return Json(query);
        }

        public bool doInsert(string query)
        {
            MySqlConnection cnn = UtilityDB.connection();
            MySqlCommand cmd = new MySqlCommand(query, cnn);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;      // inserted
            }
            catch (Exception ex)
            {
                return false;   // error
            }
        }

        public bool existReperto(string id,string categoria)
        {
           MySqlConnection cnnn= UtilityDB.connection();
            List<string> colonne = UtilityDB.getTableColumn(cnnn, categoria);
            string query = UtilityReperti.queryGetRepertoById(categoria, id, colonne);
            MySqlCommand cmd= new MySqlCommand(query, cnnn);

            cnnn.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                cnnn.Close();
                return true;   // exixt  identifier
            }
            else
            {
                cnnn.Close(); 
                return false;    // not exist identifier
            }
           
        }
    }
}