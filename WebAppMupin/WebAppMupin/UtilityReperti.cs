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

        public static string queryGetRepertoById(string tabella,string id, List<string> colonneTabella)
        {
            string query = "SELECT ";
            foreach (string cate in colonneTabella)
            {
                if (cate != "Id_catalogo")
                {
                    query += cate + ",";
                }
            }
            string querydef = query.Remove(query.Length - 1);
            querydef += " FROM " + tabella+ " WHERE Identificativo = '"+id.ToString().Replace("'","\"")+"';";
            return querydef;
        }

        public static string generateQueryDelete(string tabella, string id)
        {
            string query = "DELETE FROM " + tabella.ToString() + " WHERE Identificativo= '" + id.ToString().Replace("'", "\"") + "';";
            return query;
        }

        public static bool checkDetail(string id, MySqlConnection cno)
        {
            string qui = "SELECT * FROM repertodetail WHERE identificativoReperto = @idf";
            MySqlCommand mySql = new MySqlCommand(qui, cno);
            mySql.Parameters.AddWithValue("@idf", id);
            cno.Open();
            MySqlDataReader rd = mySql.ExecuteReader();
            if (!rd.HasRows)
            {
                rd.Close();
                return false;
            }              // hasn't detail
            else
            {
                rd.Close();
                return true;   // has detail
            }
        }
    }
}