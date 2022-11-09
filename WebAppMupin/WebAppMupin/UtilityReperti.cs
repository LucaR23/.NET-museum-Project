using Microsoft.CodeAnalysis.CSharp.Syntax;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;

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
                tables.Add(read.GetString(1));
            }
           return tables;
        }
   
      
    }
}