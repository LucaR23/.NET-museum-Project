using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
                repertoDetail.Id= dr["identificativoReperto"].ToString();
                repertoDetail.Url = dr["URL"].ToString();
                repertoDetail.Note = dr["note"].ToString();
                repertoDetail.Tag = dr["tag"].ToString();
                repertoDetail.Immagine = (byte[])dr["immagine"];
            }
            cnn.Close();
            return View("DetailReperti", repertoDetail);
        }

        public ActionResult Update(string id)
        {
            return View();

        }

        public ActionResult Delete(string id)
        {
    return View(id);
        }
    }
}