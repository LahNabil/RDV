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
    public partial class RendezVous : Form
    {
        Connexion cnx = new Connexion();
        public RendezVous()
        {
            InitializeComponent();
            cnx.Conn();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if (cnx.conn.State == ConnectionState.Closed)
            {
                cnx.seConnecter();
            }
            MySqlCommand cmd = new MySqlCommand("select * from rendezvous", cnx.conn);
            MySqlDataAdapter da;
            da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt;
            dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["idR"].ToString());
                listitem.SubItems.Add(dr["dateR"].ToString());
                listitem.SubItems.Add(dr["idP"].ToString());
                listitem.SubItems.Add(dr["traitement"].ToString());
                listitem.SubItems.Add(dr["commentaire"].ToString());
                

                listView1.Items.Add(listitem);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                idrTF.Text = listView1.SelectedItems[0].Text;
                
                idpTF.Text = listView1.SelectedItems[0].SubItems[2].Text;
                traitementTF.Text = listView1.SelectedItems[0].SubItems[3].Text;
                commentaireTF.Text = listView1.SelectedItems[0].SubItems[4].Text;
               

            }
        }
        private void loading()
        {
            if (cnx.conn.State == ConnectionState.Closed)
            {
                cnx.seConnecter();
            }
            MySqlCommand cmd = new MySqlCommand("select * from rendezvous", cnx.conn);
            MySqlDataAdapter da;
            da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt;
            dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["idR"].ToString());
                listitem.SubItems.Add(dr["dateR"].ToString());
                listitem.SubItems.Add(dr["idP"].ToString());
                listitem.SubItems.Add(dr["traitement"].ToString());
                listitem.SubItems.Add(dr["commentaire"].ToString());


                listView1.Items.Add(listitem);
            }
        }

        private void RendezVous_Load(object sender, EventArgs e)
        {
            loading();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("insert into rendezvous(dateR,idP,traitement,commentaire) values('" + this.dateTimePicker1.Value.ToString("yyyy/MM/dd hh:mm:ss") + "','" + idpTF.Text + "','" + traitementTF.Text + "','" + commentaireTF.Text + "');", cnx.conn);
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
            MySqlCommand cmd = new MySqlCommand("delete from rendezvous where idR = '" + idrTF.Text + "'; ", cnx.conn);
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

        private void button4_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("update rendezvous set dateR='" + dateTimePicker1.Text + "',idP='" + idpTF.Text + "',traitement='" + traitementTF.Text + "',commentaire='" + commentaireTF.Text + "' where idR = '" + idrTF.Text + "';   ", cnx.conn);
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
