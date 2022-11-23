using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                    return PartialView("InserisciReperto/_inserisciLibro", l);
                }
                if (ins == "periferiche")
                {
                    Periferica p = new Periferica();
                    return PartialView("InserisciReperto/_inserisciPeriferiche", p);
                }
                if (ins == "riviste")
                {
                    Rivista r = new Rivista();
                    return PartialView("InserisciReperto/_inserisciRivista", r);
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
            if (c.Identificativo == null || c.NomeModello==null)
            {
                return Json("compilare i campi necessari");
            }
            string query = getInsertstring("computer");
            //INSERT INTO computer( Identificativo,Nome_modello,anno,CPU,velocita_HZ,RAM,Hard_disk,sistema_operativo ) VALUES   *query generata*
            var id = c.Identificativo;
            var modello = c.NomeModello;
            var anno = c.Anno;
            var cpu = c.Cpu;
            var hz = c.VelocitaHz;
            var ram = c.Ram;
            var disco = c.HardDisk;
            var so = c.SistemaOperativo;
            query+=id+","+modello + "," + anno + "," + cpu + "," + hz + "," + ram + "," + disco + "," + so+" );";
            // trovare un modo di fare il binding 
            // begin transaction
            // fine e ritorno esito all'utente
            return Json(query);
        }
        public ActionResult InsertLibro(Libro l)
        {

          string query=  getInsertstring("libro");
           

            return View();
        }
        public ActionResult InsertPeriferica(Periferica p)
        {
            string query = getInsertstring("periferiche");
            return View();
        }
        public ActionResult InsertRivista(Rivista r)
        {
            string query = getInsertstring("riviste");
            return View();
        }
        public ActionResult InsertSoftware(Software s)
        {
            string query = getInsertstring("software");

            return View();
        }

        public ActionResult InsertDetail(RepertoDetail rd,string idReperto)
        {
            string query = getInsertstring("repertodetail");

            return Json(rd);
        }

        public string getInsertstring(string reperto)
        {
            MySqlConnection conn = UtilityDB.connection();
            List<string> colonne = UtilityDB.getTableColumn(conn, reperto);
             colonne.Remove("Id_catalogo");
            string query = UtilityReperti.generateQueryInsert(reperto, colonne);
            return query;
        }
    }
}