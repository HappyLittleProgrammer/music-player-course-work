using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web;

namespace MusicPlayerCourseWork
{
    public partial class VKLogIn : Form
    {
        
        public static string acc_token;
        public static string vkuserid;
        public static bool auth;

        public VKLogIn()
        {
            InitializeComponent();
        }

        private void VKLogIn_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate("https://oauth.vk.com/authorize?client_id=5164327&display=page&redirect_uri=https://oauth.vk.com/blank.html&scope=audio&response_type=token&v=5.40");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                string url = webBrowser1.Url.ToString();
                string l = url.Split('#')[1];
                if (l[0] == 'a')
                {
                    acc_token = l.Split('&')[0].Split('=')[1];
                    vkuserid = l.Split('=')[3];
                    auth = true;
                    this.Close();

                }
            }
            catch
            {

            }
        }
       
    }
}
