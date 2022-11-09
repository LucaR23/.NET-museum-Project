using MySql.Data.MySqlClient;

namespace InsertUser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Btn_invia_Click(object sender, EventArgs e)
        {
            string ConnStr = "server=127.0.0.1;port=3306;username=root;password=;database=museoinformaticatest";
            bool flag = true;
           
            if(textBoxNome.Text.Length < 2 || textBoxCognome.Text.Length <2)
            {
                MessageBox.Show(" Lunghezza minima 3 caratteri !! ");
                flag=false;
            }
            if (flag==true && (textBoxEmail.Text.Length < 3 || !(textBoxEmail.Text.Contains('@')))) 
            {
                MessageBox.Show("Username Non valido !!");
                flag = false;
            }
            else
            {
                string defaultPassword = "admin";
                MySqlConnection cnn = new MySqlConnection(ConnStr);
                MySqlCommand cmd = cnn.CreateCommand();
                string query1 = "SELECT * FROM utenti WHERE username=@us";
                cmd.CommandText=query1;
                cmd.Parameters.AddWithValue("@us",textBoxEmail.Text);
                cnn.Open();
                MySqlDataReader result=  cmd.ExecuteReader();
                if (result.Read())
                {
                    MessageBox.Show("Utente :" + textBoxEmail.Text + " Già inserito !");
                    flag = false;
                }
                cnn.Close();
                
                string query = "INSERT INTO `utenti` ( `username`, `DefaultPassword`, `Nome`, `Cognome`)  VALUES (@user, @pass,@nome,@cognome) ";
              //  string passwordHash = BCrypt.Net.BCrypt.HashPassword(defaultPassword);

                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@user", textBoxEmail.Text);
                cmd.Parameters.AddWithValue("@pass", defaultPassword);
                cmd.Parameters.AddWithValue("@nome", textBoxNome.Text);
                cmd.Parameters.AddWithValue("@cognome",textBoxCognome.Text);   
                try
                {
                    if (flag == true)
                    {
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        cnn.Close();
                        MessageBox.Show("Nuovo utente Inserito");
                    }
                }catch(MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           Form2 form2 = new Form2();
            form2.Show();    
        }
    }
}