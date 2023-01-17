namespace RDV
{
    public partial class Form1 : Form
    {
        bool sidebarExpand;
        public Form1()
        {
            InitializeComponent();
        }
        private void showChildForminPanel(object Form)
        {
            if(this.parent.Controls.Count > 0)
            {
                this.parent.Controls.RemoveAt(0);
            }


            Form form = Form as Form;
            form.TopLevel = false;
            this.parent.Controls.Add(form);
            form.Show();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showChildForminPanel(new Dashboard());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                sideBar.Width -= 10;
                if(sideBar.Width == sideBar.MinimumSize.Width)
                {
                    sidebarExpand= false;
                    timer1.Stop();
                }
            }
            else
            {
                sideBar.Width += 10;
                if(sideBar.Width == sideBar.MaximumSize.Width)
                {
                    sidebarExpand = true;
                    timer1.Stop();
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showChildForminPanel(new Dashboard());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            showChildForminPanel(new patient());
        }

        private void button5_Click(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            showChildForminPanel(new RendezVous());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            showChildForminPanel(new Facture());
        }

        private void parent_Paint(object sender, PaintEventArgs e)
        {

        }
      
    }
}