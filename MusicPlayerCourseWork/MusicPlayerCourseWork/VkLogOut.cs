using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MusicPlayerCourseWork
{
    public partial class VkLogOut : Form
    {
        public VkLogOut()
        {
            InitializeComponent();
        }

        private void VkLogOut_Load(object sender, EventArgs e)
        {
            Process p;
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            p = new Process();
            p.StartInfo = psi;
            p = Process.Start("RunDll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 258", null, null, null);
            this.Close();
        }
    }
}
