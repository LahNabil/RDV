namespace RDV
{
    public partial class Form1 : Form
    {
        bool sidebarExpand;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
    }
}