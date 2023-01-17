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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RDV
{
    public partial class patient : Form
    {
        Connexion cnx = new Connexion();
        public patient()
        {
            InitializeComponent();
            cnx.Conn();
        }
        private void loading()
        {
            if (cnx.conn.State == ConnectionState.Closed)
            {
                cnx.seConnecter();
            }
            MySqlCommand cmd = new MySqlCommand("select * from patient", cnx.conn);
            MySqlDataAdapter da;
            da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt;
            dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["idP"].ToString());
                listitem.SubItems.Add(dr["nom"].ToString());
                listitem.SubItems.Add(dr["prenom"].ToString());
                listitem.SubItems.Add(dr["adresse"].ToString());
                listitem.SubItems.Add(dr["telephone"].ToString());
                listitem.SubItems.Add(dr["dateN"].ToString());

                listView1.Items.Add(listitem);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            if (cnx.conn.State == ConnectionState.Closed)
            {
                cnx.seConnecter();
            }
            MySqlCommand cmd = new MySqlCommand("select * from patient", cnx.conn);
            MySqlDataAdapter da;
            da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt;
            dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["idP"].ToString());
                listitem.SubItems.Add(dr["nom"].ToString());
                listitem.SubItems.Add(dr["prenom"].ToString());
                listitem.SubItems.Add(dr["adresse"].ToString());
                listitem.SubItems.Add(dr["telephone"].ToString());
                listitem.SubItems.Add(dr["dateN"].ToString());

                listView1.Items.Add(listitem);
            }
        }

        private void ajouterB_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("insert into patient(nom,prenom,adresse,telephone,dateN) values('" + nomTF.Text + "','" + prenomTF.Text + "','" + adresseTF.Text + "','" + telephoneTF.Text + "','"+ this.dateTimePicker1.Value.ToString("yyyy/MM/dd") + "');", cnx.conn);
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                idTF.Text = listView1.SelectedItems[0].Text;
                nomTF.Text = listView1.SelectedItems[0].SubItems[1].Text;
                prenomTF.Text = listView1.SelectedItems[0].SubItems[2].Text;
                adresseTF.Text = listView1.SelectedItems[0].SubItems[3].Text;
                telephoneTF.Text = listView1.SelectedItems[0].SubItems[4].Text;
                

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("update patient set nom='" + nomTF.Text + "',prenom='" + prenomTF.Text + "',adresse='" + adresseTF.Text + "',telephone='" + telephoneTF.Text + "',dateN='"+ dateTimePicker1.Text + "' where idP = '" + idTF.Text + "';   ", cnx.conn);
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

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd = new MySqlCommand("delete from patient where idP = '" + idTF.Text + "'; ", cnx.conn);
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

        private void patient_Load(object sender, EventArgs e)
        {
            loading();
        }
    }
}
