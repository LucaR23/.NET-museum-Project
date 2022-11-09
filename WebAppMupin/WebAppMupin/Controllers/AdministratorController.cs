using MySql.Data.MySqlClient;
using Renci.SshNet.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace WebAppMupin.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult Index()
        {
            MySqlConnection cnn = UtilityDB.connection();
            string query = "SELECT Nome,Cognome,username FROM utenti WHERE Abilitato=1 AND DefaultPassword!=password;";
            DataTable dt = UtilityDB.GetDataTable(query, cnn);
            return View("HomeAdministrator",dt);
        }

        public ActionResult logout()
        {
            return RedirectToAction("formLogin", "Login");
        }

        
        public ActionResult Reset(string username)
        {
            MySqlConnection cnn = UtilityDB.connection();
            string query = "UPDATE `utenti` SET `password`=NULL WHERE `username`=@us";
            MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@us", username);

            cnn.Open();
            cmd.ExecuteNonQuery();

            return RedirectToAction("Index", "administrator");

        }

        public ActionResult DeleteUser(string username)
        {
            MySqlConnection cnn = UtilityDB.connection();
            string query = "UPDATE `utenti` SET `Abilitato`=0 WHERE `username`=@us";
            MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@us", username);

            cnn.Open();
            cmd.ExecuteNonQuery();

            return RedirectToAction("Index", "administrator");
        }
    }
}