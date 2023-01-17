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
    public partial class Facture : Form
    {
        Connexion cnx = new Connexion();
        public Facture()
        {
            InitializeComponent();
            cnx.Conn();
            loading();
        }
        private void loading()
        {
            
            if (cnx.conn.State == ConnectionState.Closed)
            {
                cnx.seConnecter();
            }
            MySqlCommand cmd = new MySqlCommand("select * from facture", cnx.conn);
            MySqlDataAdapter da;
            da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt;
            dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["idF"].ToString());
                listitem.SubItems.Add(dr["idR"].ToString());
                listitem.SubItems.Add(dr["dateF"].ToString());
                listitem.SubItems.Add(dr["montant"].ToString());
                listitem.SubItems.Add(dr["etat"].ToString());



                listView1.Items.Add(listitem);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            loading();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string etat = "";
            if (radioButton1.Checked)
            {
                etat = radioButton1.Text;
            }
            if(radioButton2.Checked)
            {
                etat = radioButton2.Text;
            }
            MySqlCommand cmd = new MySqlCommand("insert into facture(idR,montant,etat) values('" + textBox1.Text + "','" + textBox2.Text + "','" + etat + "');", cnx.conn);
            try
            {
                if (cnx.conn.State == ConnectionState.Closed)
                {
                    cnx.seConnecter();
                }
                cmd.ExecuteNonQuery();
                MessageBox.Show("done!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                cnx.seDeconnecter();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("delete from facture where idF = '" + idfTF.Text + "'; ", cnx.conn);
            try
            {
                if (cnx.conn.State == ConnectionState.Closed)
                {
                    cnx.seConnecter();
                }
                cmd.ExecuteNonQuery();
                MessageBox.Show("patient supprimé!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                idfTF.Text = listView1.SelectedItems[0].Text;
                textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox2.Text = listView1.SelectedItems[0].SubItems[3].Text;
                
                


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string etat = "";
            if (radioButton1.Checked)
            {
                etat = radioButton1.Text;
            }
            if (radioButton2.Checked)
            {
                etat = radioButton2.Text;
            }
            MySqlCommand cmd = new MySqlCommand("update facture set idR='" + textBox1.Text + "',montant='" + textBox2.Text + "',etat='" +etat+"' where idF = '" + idfTF.Text + "';   ", cnx.conn);
            try
            {
                if (cnx.conn.State == ConnectionState.Closed)
                {
                    cnx.seConnecter();
                }
                cmd.ExecuteNonQuery();
                MessageBox.Show("modifié avec succes!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
