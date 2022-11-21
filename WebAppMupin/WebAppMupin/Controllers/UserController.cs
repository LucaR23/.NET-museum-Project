using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppMupin.Models;
using WebAppMupin.Models.Reperti;

namespace WebAppMupin.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
          List<HomeUser> list = getinfo();
            return View("HomeUser",list);
        }


        private List<HomeUser> getinfo()
        {
            MySqlConnection cnn = UtilityDB.connection();
            string query = "SELECT Nome,immagine FROM categoriereperti;";

            List<HomeUser> homeUsers = new List<HomeUser>();

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Connection = cnn;
            cnn.Open();
            MySqlDataReader apt = cmd.ExecuteReader();
            while (apt.Read())
            {
                homeUsers.Add(new HomeUser
                {
                    nome = apt["Nome"].ToString(),
                    image = (byte[])apt["immagine"]
                });
                

            }
            cnn.Close();
            return homeUsers;

        }


        public ActionResult logout()
        {
            Session["utente"] = null;
            return RedirectToAction("formLogin", "Login");
        }

        public ActionResult Reperti(string categoria)
        {
            MySqlConnection cnn = UtilityDB.connection();

            List<string> tabelle = UtilityReperti.getTableName(cnn);

            if (!tabelle.Contains(categoria))
            {
                return RedirectToAction("Index");
            }
            else
            {
                List<string> colonne = UtilityDB.getTableColumn(cnn, categoria);
                string query = UtilityReperti.generateQuerySelect(categoria, colonne);
                DataTable dt = UtilityDB.GetDataTable(query, cnn);

                 dt.Columns.Add("categoria");
                 DataRow dataRow = dt.NewRow();
                 dataRow["categoria"] = categoria;
                 dt.Rows.Add(dataRow);

                return View("Viewreperti", dt);
            }

        }

        public ActionResult Detail(string dt)
        {
            string query = "SELECT * FROM repertoDetail WHERE identificativoReperto= @id";
            MySqlConnection cnn = UtilityDB.connection();
           
            MySqlCommand cmd = new MySqlCommand(query, cnn);
            cmd.Parameters.AddWithValue("@id", dt);
            cnn.Open();
            MySqlDataReader dr = cmd.ExecuteReader();
            
            RepertoDetail repertoDetail = new RepertoDetail();
            while (dr.Read())
            {
                if (dr != null)
                {
                    repertoDetail.Id = dr["identificativoReperto"].ToString();
                    repertoDetail.Url = dr["URL"].ToString();
                    repertoDetail.Note = dr["note"].ToString();
                    repertoDetail.Tag = dr["tag"].ToString();
                    repertoDetail.Immagine = (byte[])dr["immagine"];
                }
            }
            cnn.Close();
            return PartialView("_DetailReperti", repertoDetail);
        }

        public ActionResult Update(string id)
        {
            return View();

        }

        public ActionResult Delete(string del,string tab)
        {
            MySqlConnection connection = UtilityDB.connection();

            string query = UtilityReperti.generateQueryDelete(tab,del);
            MySqlCommand cnn = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                cnn.ExecuteNonQuery();
                connection.Close();
                return Json(true);
            }catch(MySqlException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }              
        }

        public ActionResult Insert(string ins)
        {
            MySqlConnection conn = UtilityDB.connection();
            List<string> tab = UtilityReperti.getTableName(conn);
            if (tab.Contains(ins))
            {
                if (ins == "computer")
                {
                    Computer c = new Computer();
                    return PartialView("InserisciReperto/_inserisciComputer",c);
                }
                if (ins == "libri")
                {
                    Libro l = new Libro();
                    return PartialView("InserisciReperto/_inserisciLibro",l);
                }
                if (ins == "periferiche")
                {
                    Periferica p = new Periferica();
                    return PartialView("InserisciReperto/_inserisciPeriferiche",p);
                }
                if (ins == "riviste")
                {
                    Rivista r = new Rivista();
                    return PartialView("InserisciReperto/_inserisciRivista",r);
                }
                if (ins == "software")
                {
                    Software s  = new Software();
                    return PartialView("InserisciReperto/_inserisciSoftware",s);
                }
                if(ins == "repertodetail")
                {
                    RepertoDetail rd = new RepertoDetail();
                    return PartialView("InserisciReperto/_inserisciDetail",rd);
                }
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();    
        }

        public ActionResult New()
        {
            MySqlConnection cnn = UtilityDB.connection();
            List<string> tabelle = UtilityReperti.getTableName(cnn);
            tabelle.Remove("categoriereperti");
            tabelle.Remove("utenti");
            tabelle.Remove("repertodetail");
            return View("NewReperto",tabelle);
        }
    }
}