using MySql.Data.MySqlClient;
using Renci.SshNet.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebAppMupin
{
    public static class UtilityLogin
    {

        public static bool tryLogin(string Username,string Password)
        {
           MySqlConnection cnn=  UtilityDB.connection();

            string query = "SELECT password FROM utenti WHERE  username=@us AND Abilitato=1;";
            MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@us", Username);

                cnn.Open();
                MySqlDataReader read = cmd.ExecuteReader();
                read.Read();
                // procedura di verifica password
    
                string passBD = (string)read["password"];
                bool verified = BCrypt.Net.BCrypt.Verify(Password, passBD);
                if (verified)
                {
                    cnn.Close();
                    return true;   // logged 
                }
                else
                {
                    cnn.Close();
                    return false;   // passsword don't match 
                }
               
        }

        public static int isSetted(string username,string password)
        {
            MySqlConnection cnn = UtilityDB.connection();
            string query = "SELECT password,DefaultPassword FROM utenti WHERE username=@us AND Abilitato=1";
            MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@us", username);

            cnn.Open();
            MySqlDataReader read = cmd.ExecuteReader();
            read.Read();
            if (read["password"] != DBNull.Value)
            {
                return 2;   // already setted
            }
            else
            {
                if(password == (string)read["DefaultPassword"])
                {
                    return 1;  // set password
                }
                else
                {
                    return 0;  // not correct
                }
            }
         
        }

        public static bool verifiyUser(string username)  
        {
            MySqlConnection cnn = UtilityDB.connection();
            string query = "SELECT ID FROM utenti WHERE username= @us AND Abilitato=1;";
            MySqlCommand cmd= cnn.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@us", username);
            try
            {
                cnn.Open();
                MySqlDataReader read= cmd.ExecuteReader();
                if( read.Read())
                {
                    return true; // user exist
                }
                else
                {
                    return false;  // user doesn't exist 
                }
            }catch(MySqlException ex)
            {
                return false;
            }
           
        }

        public static bool verifyAdmin(string username)
        {
            MySqlConnection cnn = UtilityDB.connection();
            string queryVerify = "SELECT username FROM utenti WHERE DefaultPassword = password;";
            MySqlCommand mySqlCommand = cnn.CreateCommand();
            mySqlCommand.CommandText = queryVerify;
            cnn.Open();
            MySqlDataReader reader= mySqlCommand.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                if ((string)reader["username"] == username)
                    return true; // is administrator
                else
                    return false;// not an administrator
            }
            else
            {
                reader.Close();
                return false;   // not found 
            }
        }

        public static bool insertPassword(string password,string username)
        {
            MySqlConnection cnn = UtilityDB.connection();
            string queryInsert = "UPDATE `utenti` SET `password`= @pass WHERE `username`= @us;";
            string HashPass = BCrypt.Net.BCrypt.HashPassword(password);
            bool esito=false;
            MySqlCommand cmd = cnn.CreateCommand();
            cmd.CommandText = queryInsert;
            cmd.Parameters.AddWithValue("@pass",HashPass);
            cmd.Parameters.AddWithValue("@us", username);
            try
            {
                cnn.Open();            
            }catch(MySqlException e)
            {
                esito= false;     // error
            }
            cmd.ExecuteNonQuery();
            cnn.Close();
            esito = true;    // password inserted
            return esito;


        }

    }
}