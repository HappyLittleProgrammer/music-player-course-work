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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            textBox1.Text = LoggedUser.LoggedInUser;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VKLogIn f5 = new VKLogIn();
            f5.Show();
        }
    }
}
