using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
            try
            {
                cnn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                RepertoDetail repertoDetail = new RepertoDetail();
                if (!dr.HasRows)
                {
                    return Json("Non sono presenti dettagli");
                }
                    while (dr.Read())
                    {
                    repertoDetail.Id = dr["identificativoReperto"].ToString();
                    if (dr["URL"] != DBNull.Value)
                    {
                        repertoDetail.Url = dr["URL"].ToString();
                    }
                    if(dr["note"] != DBNull.Value)
                    {
                        repertoDetail.Note = dr["note"].ToString();
                    }
                    if(dr["tag"] != DBNull.Value)
                    {
                        repertoDetail.Tag = dr["tag"].ToString();
                    }
                    if(dr["immagine"] != DBNull.Value)
                    {
                        repertoDetail.Immagine = (byte[])dr["immagine"];
                    }
                    dr.Close();
                    return PartialView("_DetailReperti", repertoDetail);
                    }
                 
            }
            catch (MySqlException ex)
            {
                return Json("Non sono presenti dettagli");
            }
            finally
            {
                cnn.Close();
            }
            return View();  // -> not used
        }

        public ActionResult Delete(string del,string tab)
        {
            MySqlConnection connection = UtilityDB.connection();
            // check if field has detail
            bool hasDetail = UtilityReperti.checkDetail(del, connection);
            if (hasDetail)
            {
                string qu = "DELETE FROM repertodetail WHERE identificativoReperto = @ide";
                MySqlCommand sc = new MySqlCommand(qu,connection);
                sc.Parameters.AddWithValue("@ide", del);
                //connection.Open();
                sc.ExecuteNonQuery();
                connection.Close();
            }
            string query = UtilityReperti.generateQueryDelete(tab,del);
            MySqlCommand cnn = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                cnn.ExecuteNonQuery();     
                connection.Close();
                return Json("Eliminato");
            }catch(MySqlException ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }              
        }
    }
}