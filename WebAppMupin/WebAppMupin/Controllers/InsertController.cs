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
        // GET: Insert
        /* public ActionResult Index()
         {
             return View();
         }*/
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
            if (c == null)
            {
                return Json("compilare i campi necessari");
            }
            MySqlConnection conn = UtilityDB.connection();
            List<string> colonne = UtilityDB.getTableColumn(conn, "computer");
            colonne.Remove("`Id_catalogo");
            string query = UtilityReperti.generateQueryInsert("computer", colonne);
            // passo ad una procedura che mi crea la query 
            // trovare un modo di fare il binding 
            // begin transaction
            // fine e ritorno esito all'utente
            return Json(c);
        }
        public ActionResult InsertLibro(Libro l)
        {

            MySqlConnection conn = UtilityDB.connection();
            List<string> colonne = UtilityDB.getTableColumn(conn, "libri");
            colonne.Remove("`Id_catalogo");
            string query  = UtilityReperti.generateQueryInsert("libri",colonne);

            return View();
        }
        public ActionResult InsertPeriferica(Periferica p)
        {
            MySqlConnection conn = UtilityDB.connection();
            List<string> colonne = UtilityDB.getTableColumn(conn, "periferiche");
            colonne.Remove("`Id_catalogo");
            string query = UtilityReperti.generateQueryInsert("periferiche", colonne);
            return View();
        }
        public ActionResult InsertRivista(Rivista r)
        {
            MySqlConnection conn = UtilityDB.connection();
            List<string> colonne = UtilityDB.getTableColumn(conn, "riviste");

            return View();
        }
        public ActionResult InsertSoftware(Software s)
        {
            MySqlConnection conn = UtilityDB.connection();
            List<string> colonne = UtilityDB.getTableColumn(conn, "software");

            return View();
        }

        public ActionResult InsertDetail(RepertoDetail rd)
        {

            return Json(rd);
        }
    }
}