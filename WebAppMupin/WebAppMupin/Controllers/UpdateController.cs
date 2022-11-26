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
        public ActionResult GetReperto(string upd, string tab)
        {
            MySqlConnection conn = UtilityDB.connection();
            List<string> tabelle = UtilityReperti.getTableName(conn);
       
            if (tabelle.Contains(upd))
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
                    //
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
                    //
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
                    //
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
                    //
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
                    //
                }
                if (tab == "repertodetail")
                {
                    RepertoDetail red = new RepertoDetail();
                    while (rd.Read())
                    {
                        if (rd["note"] != DBNull.Value)
                        {
                            red.Note = rd["note"].ToString();
                        }
                        if (rd["URL"]!= DBNull.Value)
                        {
                            red.Url = rd["URL"].ToString();
                        }
                        if (rd["tag"]!= DBNull.Value)
                        {
                            red.Tag = rd["tag"].ToString();
                        }
                    }
                    //
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