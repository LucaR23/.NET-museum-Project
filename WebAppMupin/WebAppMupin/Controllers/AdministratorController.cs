using MySql.Data.MySqlClient;
using Renci.SshNet.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using WebAppMupin.Models;

namespace WebAppMupin.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult Index()
        {
            MySqlConnection cnn = UtilityDB.connection();
            string query = "SELECT Nome,Cognome,username,Abilitato FROM utenti WHERE password IS NULL OR DefaultPassword!=password;";
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
            string query = "UPDATE `utenti` SET `password`=NULL,`Abilitato`=0  WHERE `username`=@us";
            MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@us", username);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
            return RedirectToAction("Index", "administrator");

        }

        public ActionResult Enable(string username)
        {
            MySqlConnection cnn = UtilityDB.connection();
            string query = "UPDATE `utenti` SET `Abilitato`=1 WHERE `username`=@us";
            MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@us", username);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
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
            cnn.Close();
            return RedirectToAction("Index", "administrator");
        }

        public ActionResult New()
        {
            NewUser newUser = new NewUser();
            newUser.DefaultPassword = "admin";
            newUser.message = "Inserisci i dati necessari";   
            return PartialView("_NewUser", newUser);
        }

        public ActionResult Insert(NewUser n)
        {
            if (n.UserName == null || n.Nome == null || n.Cognome == null)
            {
                return Json("Compila tutti i campi");
            }
            bool exist = UtilityLogin.verifiyUser(n.UserName);
            if (exist)
            {
                return Json("Utente già registrato");
            }
            
            // controllare di non inserire due username uguali
            else
            {
                MySqlConnection cnnn = UtilityDB.connection();
                string query = "INSERT INTO `utenti`(`username`,`DefaultPassword`, `Nome`, `Cognome`) VALUES (@us,@def,@nome,@cgn)";
                MySqlCommand cmd = new MySqlCommand(query, cnnn);
                cmd.Parameters.AddWithValue("@us", n.UserName);
                cmd.Parameters.AddWithValue("@def","admin");
                cmd.Parameters.AddWithValue("@nome", n.Nome);
                cmd.Parameters.AddWithValue("@cgn", n.Cognome);
                try
                {
                    cnnn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch(MySqlException ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
                cnnn.Close();
                //n.message = "nuovo Utente inserito";
                return Json("Nuovo Utente inserito");
            }
        }
    }
}