using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebAppMupin
{
    public class UtilityDB
    {
        public static MySqlConnection connection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(WebConfigurationManager.AppSettings["ConnStr"]);
                return conn;
            }catch(MySqlException ex)
            {
                return null;
            }

        }

        public static DataTable GetDataTable(string query,MySqlConnection cnn)
        {
            MySqlCommand cmd = new MySqlCommand(query,cnn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            cnn.Open();
            da.SelectCommand = cmd;
            da.Fill(dt); 
            return dt;
        }


        public static List<string> getTableColumn(MySqlConnection cnn,string tabella)
        { 
            string query = "SELECT `COLUMN_NAME` AS 'colonne' FROM `INFORMATION_SCHEMA`.`COLUMNS` WHERE `TABLE_SCHEMA`='museoinformaticatest' AND `TABLE_NAME`= @table;";
            MySqlCommand cmd= new MySqlCommand(query,cnn);
            cmd.Parameters.AddWithValue("@table",tabella);
            cnn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            List<string> colonne = new List<string>();

            while (reader.Read())
            {
                colonne.Add((string)reader["colonne"]);
            }
            cnn.Close();
            return colonne;
        }
   
    }
}