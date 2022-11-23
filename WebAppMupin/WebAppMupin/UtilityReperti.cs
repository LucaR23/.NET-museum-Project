using Microsoft.CodeAnalysis.CSharp.Syntax;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using WebAppMupin.Models.Reperti;

namespace WebAppMupin
{
    public static  class UtilityReperti
    {
     
        public static string generateQuerySelect(string categoria, List<string> categorias)
        {
            string query = "SELECT ";
            foreach(string cate in categorias)
            {
                if(cate != "Id_catalogo")
                {
                    query += cate+",";
                }
            }
            string querydef=  query.Remove(query.Length - 1);
            querydef += " FROM " + categoria;
            return querydef;
        }

        public static List<string> getTableName(MySqlConnection cnn)
        {
            string query = "SHOW TABLES;";
            MySqlCommand cmd = new MySqlCommand(query, cnn);
            List<string> tables = new List<string>();
            cnn.Open();
            MySqlDataReader read= cmd.ExecuteReader();
            while (read.Read())
            {
                tables.Add(read.GetString(0));
            }
            cnn.Close();
           return tables;
        }

        public static string queryGetRepertoById(string tabella,string id, List<string> valori)
        {
            string query = "SELECT ";
            foreach (string cate in valori)
            {
                if (cate != "Id_catalogo")
                {
                    query += cate + ",";
                }
            }
            string querydef = query.Remove(query.Length - 1);
            querydef += " FROM " + tabella+ " WHERE Identificativo = '"+id.ToString()+"';";
            return querydef;
        }

        public static string generateQueryDelete(string tabella,string id)
        {
            string query = "DELETE FROM " + tabella.ToString() + " WHERE Identificativo= '"+id.ToString()+"';";
            return query;
        }
   
        public static string generateQueryUpdate(string tab,string id)
        {
            string query = "";
            return query;
        }

        public static string generateQueryInsert(string tab,List<string> campi)
        {
            // INSERT INTO `periferiche`(`Id_catalogo`, `Identificativo`, `nome_modello`, `tipologia`) VALUES('[value-1]', '[value-2]', '[value-3]', '[value-4]')
            string query = "INSERT INTO " + tab.ToString()+"( ";
            foreach(string field in campi)
            {
                query += field+",";
            }
           string queryMod= query.Remove(query.Length - 1);

            queryMod += " ) VALUES ( ";

            return queryMod;
        }


    }
}