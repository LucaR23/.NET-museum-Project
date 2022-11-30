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
        public ActionResult Index()
        {
            return View("UpdateReperto");
        }

        public ActionResult GetReperto(string upd, string tab)
        {
            MySqlConnection conn = UtilityDB.connection();
            List<string> tabelle = UtilityReperti.getTableName(conn);
       
            if (tabelle.Contains(tab))
            {
                string query = queryReperto(upd, tab);
                MySqlCommand cmd= new MySqlCommand(query, conn);
                conn.Open();
                MySqlDataReader rd= cmd.ExecuteReader();
                if (tab == "computer")
                {
                    Computer c = new Computer();
                    while (rd.Read())
                    {
                        c.Identificativo = rd["Identificativo"].ToString();
                        c.NomeModello = rd["Nome_modello"].ToString();
                        c.Anno = rd["anno"].ToString();
                        c.Cpu = rd["CPU"].ToString();
                        c.VelocitaHz= rd["velocita_HZ"].ToString() ;
                        c.Ram = rd["RAM"].ToString();
                        if (rd["Hard_disk"] != DBNull.Value)
                        {
                            c.HardDisk = rd["Hard_disk"].ToString();
                        }
                        if (rd["sistema_operativo"]!= DBNull.Value)
                        {
                            c.SistemaOperativo = rd["sistema_operativo"].ToString();
                        }
                    }
                    return PartialView("InserisciReperto/_inserisciComputer", c);
                }
                if (tab == "libri")
                {
                    Libro l = new Libro();
                    while (rd.Read())
                    {
                        l.Identificativo = rd["Identificativo"].ToString();
                        l.Titolo = rd["titolo"].ToString();
                        l.autori = rd["autori"].ToString();
                        l.CasaEditrice=rd["casa_editrice"].ToString();
                        l.AnnoPubblicazione = rd["anno_pubblicazione"].ToString();
                        l.numeroPagine = rd["numero_pagine"].ToString();
                        l.ISBN = rd["ISBN"].ToString();
                    }
                    return PartialView("InserisciReperto/_inserisciLibri", l);
                }

                if (tab == "periferiche")
                {
                    Periferica p = new Periferica();
                    while (rd.Read())
                    {
                        p.Identificativo = rd["Identificativo"].ToString();
                        p.nomeModello= rd["nome_modello"].ToString() ;
                        p.Tipologia = rd["tipologia"].ToString();
                    }
                      return PartialView("InserisciReperto/_inserisciPeriferiche", p);
                }

                if (tab == "riviste")
                {
                    Rivista r = new Rivista();
                    while (rd.Read())
                    {
                        r.Identificativo = rd["Identificativo"].ToString();
                        r.Titolo = rd["titolo"].ToString();
                        r.numeroRivista = rd["numero_rivista"].ToString();
                        r.Anno = rd["anno"].ToString();
                        r.casaEditrice = rd["casa_editrice"].ToString();
                    }
                    return PartialView("InserisciReperto/_inserisciRiviste", r);
                }

                if (tab == "software")
                {
                    Software s = new Software();
                    while (rd.Read())
                    {
                        s.Identificativo = rd["Identificativo"].ToString();
                        s.Titolo = rd["sistema_operativo"].ToString();
                        s.sistemaOperativo = rd["sistema_operativo"].ToString();
                        s.tipoSoftware = rd["tipo_software"].ToString();
                        s.supporto = rd["supporto"].ToString();
                    }
                    return PartialView("InserisciReperto/_inserisciSoftware", s);
                }
                conn.Close();
            }         
            else
            {  
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();      // -> not Used 
        }
        // very scholastic method there are better way using reflection or other mapper helper



        public ActionResult GetRepertoDetail(string upd)
        {
            RepertoDetail red = new RepertoDetail();
            MySqlConnection con = UtilityDB.connection();
            string que = "SELECT * FROM repertodetail WHERE identificativoReperto= @id";
            MySqlCommand command = new MySqlCommand(que, con);
            con.Open();
            command.Parameters.AddWithValue("@id", upd);
            MySqlDataReader read = command.ExecuteReader();
            if (!read.HasRows)
            {
                con.Close();
                return PartialView("InserisciReperto/_inserisciDetail", red);
            }
            while (read.Read())
            {
                if (read["note"] != DBNull.Value)
                {
                    red.Note = read["note"].ToString();
                }
                if (read["URL"] != DBNull.Value)
                {
                    red.Url = read["URL"].ToString();
                }
                if (read["tag"] != DBNull.Value)
                {
                    red.Tag = read["tag"].ToString();
                }
            }
            con.Close();
            return PartialView("InserisciReperto/_inserisciDetail", red);
        }

        //  metodi di update
        public ActionResult UpdateComputer(Computer c)
        {
           string query= c.Update(c);
            bool a = doUpdate(query);
            if (a == true)
                return Json("Aggiornato con successo");
            else  
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        public ActionResult UpdateLibro(Libro l)
        {
            string query = l.Update(l);
            bool a = doUpdate(query);
            if (a == true)
                return Json("Aggiornato con successo");
            else
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult UpdatePeriferica(Periferica p)
        {
            string query = p.Update(p);
            bool a = doUpdate(query);
            if (a == true)
                return Json("Aggiornato con successo");
            else
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult UpdateRivista(Rivista r)
        {
            string query = r.Update(r);
            bool a = doUpdate(query);
            if (a == true)
                return Json("Aggiornato con successo");
            else
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult UpdateSoftware(Software s)
        {
            string query = s.Update(s);
            bool a = doUpdate(query);
            if (a == true)
                return Json("Aggiornato con successo");
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult UpdateDetail(RepertoDetail rd)
        {
            return Json("Aggiornato con successo");
     
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // piccolo metodo di utility
        public string queryReperto(string upd, string tab)
        {
            MySqlConnection cnnn = UtilityDB.connection();
            List<string> proprieta = UtilityDB.getTableColumn(cnnn, tab);
            string query = UtilityReperti.queryGetRepertoById(tab, upd, proprieta);
            return query;

        }

        public bool doUpdate(string query)
        {
            MySqlConnection cnn = UtilityDB.connection();
            MySqlCommand cmd = new MySqlCommand(query, cnn);
            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                return true;      // updated
            }
            catch (Exception ex)
            {
                return false;   // error
            }
        }

    }
}