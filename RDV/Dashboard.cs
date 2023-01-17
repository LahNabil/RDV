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
using System.Windows.Forms.DataVisualization.Charting;

namespace RDV
{
    public partial class Dashboard : Form
    {
        Connexion cnx = new Connexion();
        public Dashboard()
        {
            InitializeComponent();
            cnx.Conn();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
        private void modifyLabel()
        {
            if (cnx.conn.State == ConnectionState.Closed)
            {
                cnx.seConnecter();
            }
            MySqlCommand cmd = new MySqlCommand("SELECT SUM(montant) FROM facture;", cnx.conn);
            object result = cmd.ExecuteScalar();
            label3.Text = result.ToString();
        }
        private void loadChart1()
        {
            if (cnx.conn.State == ConnectionState.Closed)
            {
                cnx.seConnecter();
            }
            MySqlCommand cmd = new MySqlCommand("SELECT traitement, COUNT(*) as 'nombre' FROM rendezvous GROUP BY traitement", cnx.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            chart1.DataSource = data;
            try
            {


                
                chart1.Series.Clear();
                chart1.Series.Add("Traitement");
                chart1.Series["Traitement"].ChartType = SeriesChartType.Pie;
                chart1.Series["Traitement"].XValueMember = "traitement";
                chart1.Series["Traitement"].YValueMembers = "nombre";
                chart1.Series["Traitement"]["PieLabelStyle"] = "Disabled";
                chart1.DataBind();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadChart1();
        }
        private void loadChart2()
        {
            if (cnx.conn.State == ConnectionState.Closed)
            {
                cnx.seConnecter();
            }
            MySqlCommand cmd = new MySqlCommand("SELECT etat, SUM(montant) as 'montant total' FROM facture GROUP BY etat;", cnx.conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable data = new DataTable();
            adapter.Fill(data);

            chart2.DataSource = data;
            try
            {
                chart2.Series.Clear();
                chart2.Series.Add("Etat");
                chart2.Series["Etat"].ChartType = SeriesChartType.Bar;
                chart2.Series["Etat"].XValueMember = "etat";
                chart2.Series["Etat"].YValueMembers = "montant total";
                chart2.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            loadChart2();

        }

       private void modifyLabelR()
        {
            if (cnx.conn.State == ConnectionState.Closed)
            {
                cnx.seConnecter();
            }
            MySqlCommand cmd = new MySqlCommand("SELECT count(idR) FROM rendezvous;", cnx.conn);
            object result1 = cmd.ExecuteScalar();
            label6.Text = result1.ToString();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            loadChart1();
            loadChart2();
            modifyLabel();
            modifyLabelR();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            this.Close();
            if (cnx.conn.State == System.Data.ConnectionState.Open)
            {
                cnx.seDeconnecter();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
