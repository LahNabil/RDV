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

namespace RDV
{
    public partial class Login : Form
    {
        Connexion cnx = new Connexion();
        
        public Login()
        {
            InitializeComponent();
            cnx.Conn();
            this.StartPosition = FormStartPosition.CenterScreen;
            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("select * from users where id='" + textBox1.Text + "'and password = '" + textBox2.Text + "';", cnx.conn);
            try
            {
                if (cnx.conn.State == System.Data.ConnectionState.Closed)
                {
                    cnx.seConnecter();
                }
                cmd.ExecuteNonQuery();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Form1 menu = new Form1();

                    menu.Show();
                    this.Hide();

                    
                }
                else
                {
                    MessageBox.Show("id or password wrong!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
