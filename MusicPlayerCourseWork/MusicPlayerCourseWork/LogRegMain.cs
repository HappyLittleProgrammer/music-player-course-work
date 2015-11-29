using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayerCourseWork
{
    public partial class LogRegMain : Form
    {
        public LogRegMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registration f3 = new Registration();
            f3.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Login f4 = new Login();
            f4.Show();
            Hide();
        }

    }
}
