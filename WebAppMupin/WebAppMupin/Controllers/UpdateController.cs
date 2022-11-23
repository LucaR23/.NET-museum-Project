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
    public class UpdateController : Controller
    {
        // GET: Update
        /*   public ActionResult Index()
           {
               return View();
           }*/

        public ActionResult GetReperto(string upd, string tab)
        {
            MySqlConnection conn = UtilityDB.connection();
            List<string> tabelle = UtilityReperti.getTableName(conn);
       
            if (tabelle.Contains(upd))
            {
                string query = queryReperto(upd, tab);
                if (tab == "computer")
                {
                    Computer c = new Computer();
                  
                }
                if (tab == "libri")
                {
                    Libro l = new Libro();

                }
                if (tab == "periferiche")
                {
                    Periferica p = new Periferica();
                  
                }
                if (tab == "riviste")
                {
                    Rivista r = new Rivista();
                 
                }
                if (tab == "software")
                {
                    Software s = new Software();
                }
                if (tab == "repertodetail")
                {
                    RepertoDetail rd = new RepertoDetail();
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();      // -> not Used 
        }

    

        // piccolo metodo di utility
        public string queryReperto(string upd, string tab)
        {
            MySqlConnection cnnn = UtilityDB.connection();
            List<string> proprieta = UtilityDB.getTableColumn(cnnn, tab);
            string query = UtilityReperti.queryGetRepertoById(tab, upd, proprieta);
            return query;

        }
    }
}