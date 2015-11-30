using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
           var myFilter = new Windows.Web.Http.FiltersHttpBaseProtocolFilter();
           var cookieManager = myFilter.CookieManager;
           var myCookieJar = cookieManager.GetCookies(new Uri("https://oauth.vk.com"));
           foreach (var cookie in myCookieJar)
           {
               cookieManager.DeleteCookie(cookie);
           }
        }
    }
}
