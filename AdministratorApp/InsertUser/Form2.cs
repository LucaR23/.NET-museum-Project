using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsertUser
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            string ConnStr = "server=127.0.0.1;port=3306;username=root;password=;database=museoinformaticatest";
            string query = "SELECT username,Nome,Cognome FROM utenti WHERE Abilitato=1 AND password!=DefaultPassword";
            MySqlConnection cnn = new MySqlConnection(ConnStr);
            MySqlDataAdapter da = new MySqlDataAdapter(query, cnn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridUtenti.DataSource = dt;


            string utenti = "SELECT username FROM utenti WHERE Abilitato=1 AND password!=DefaultPassword;";
            cnn.Open();
            MySqlCommand cmd = new MySqlCommand(utenti, cnn);
            MySqlDataReader read = cmd.ExecuteReader();
            while (read.Read())
            {
                checkedListUtenti.Items.Add(read.GetValue("username"));
            }
            cnn.Close();

        }

        private void btn_elimina_Click(object sender, EventArgs e)
        {
            string connStr = " server = 127.0.0.1; port = 3306; username = root; password =; database = museoinformaticatest";
            MySqlConnection cnn = new MySqlConnection(connStr);
            for (int i = 0; i < checkedListUtenti.Items.Count; i++)
            {
                if (checkedListUtenti.GetItemChecked(i) == true)
                {
                    //   MessageBox.Show("This is the value of ceckhed Item " + checkedListBox1.Items[i].ToString());
                    string queryUpdate = "UPDATE utenti SET Abilitato =0 WHERE username=@us;";

                    MySqlCommand mySqlCommand = new MySqlCommand(queryUpdate, cnn);
                    mySqlCommand.Parameters.AddWithValue("@us", checkedListUtenti.Items[i].ToString());
                    try
                    {
                        cnn.Open();
                        mySqlCommand.ExecuteNonQuery();
                        cnn.Close();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }

                }

            }
            this.Close();
            Form2 main = new Form2();
            main.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = " server = 127.0.0.1; port = 3306; username = root; password =; database =museoinformaticatest";
            MySqlConnection cnn = new MySqlConnection(connStr);
            for (int i = 0; i < checkedListUtenti.Items.Count; i++)
            {
                if (checkedListUtenti.GetItemChecked(i) == true)
                {

                    string queryUpdate = "UPDATE utenti SET password= NULL WHERE username=@us;";

                    MySqlCommand mySqlCommand = new MySqlCommand(queryUpdate, cnn);
                    mySqlCommand.Parameters.AddWithValue("@us", checkedListUtenti.Items[i].ToString());
                    try
                    {
                        cnn.Open();
                        mySqlCommand.ExecuteNonQuery();
                        cnn.Close();
                    }
                     
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                    MessageBox.Show("Password Resettata per l'utente" + checkedListUtenti.Items[i].ToString());
                }
            }
          
        }

    }
}
