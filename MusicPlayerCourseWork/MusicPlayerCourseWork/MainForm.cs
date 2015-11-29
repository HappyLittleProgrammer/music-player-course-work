using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Threading;

namespace MusicPlayerCourseWork
{
    public partial class MainForm : Form
    {

        public List<Audio> audioList;
        WMPLib.IWMPPlaylist PlayList;
        WMPLib.IWMPMedia Media;


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LogRegMain f1 = new LogRegMain();
            f1.Visible = false;
            textBox1.Text = LoggedUser.LoggedInUser;
            axWindowsMediaPlayer1.settings.volume = 10;
        }

        public class Audio
        {
            public int aid { get; set; }
            public int owner_id { get; set; }
            public string artist { get; set; }
            public string title { get; set; }
            public int duration { get; set; }
            public string url { get; set; }
            public string lurlcs_id { get; set; }
            public int genre { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VKLogIn f5 = new VKLogIn();
            f5.Show();
            backgroundWorker1.RunWorkerAsync();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            while (!VKLogIn.auth) { Thread.Sleep(500); }

            WebRequest request = WebRequest.Create("https://api.vk.com/method/audio.get?owner_id=" + VKLogIn.vkuserid + "&need_user=0&access_token=" + VKLogIn.acc_token);

            WebResponse response = request.GetResponse();

            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();

            response.Close();

            responseFromServer = HttpUtility.HtmlDecode(responseFromServer);

            JToken token = JToken.Parse(responseFromServer);
            audioList = token["response"].Children().Skip(1).Select(c => c.ToObject<Audio>()).ToList();

            this.Invoke((MethodInvoker)delegate
            {

                PlayList = axWindowsMediaPlayer1.playlistCollection.newPlaylist("vkPlayList");


                for (int i = 0; i < audioList.Count(); i++)
                {
                    Media = axWindowsMediaPlayer1.newMedia(audioList[i].url);
                    PlayList.appendItem(Media);
                    listBox1.Items.Add(audioList[i].artist + " - " + audioList[i].title);
                }
                axWindowsMediaPlayer1.currentPlaylist = PlayList;
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                comboBox1.Enabled = true;
            });
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                axWindowsMediaPlayer1.Ctlcontrols.currentItem = axWindowsMediaPlayer1.currentPlaylist.get_Item(listBox1.SelectedIndex);
            }
        }

        private void axWindowsMediaPlayer1_CurrentItemChange(object sender, AxWMPLib._WMPOCXEvents_CurrentItemChangeEvent e)
        {

            if (listBox1.SelectedIndex > -1)
            {

                if (listBox1.SelectedIndex != audioList.Count - 1)
                {
                    if (axWindowsMediaPlayer1.Ctlcontrols.currentItem.name == axWindowsMediaPlayer1.currentPlaylist.get_Item(listBox1.SelectedIndex + 1).name)
                    {
                        listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
                    }
                }

                if (listBox1.SelectedIndex != 0)
                {
                    if (axWindowsMediaPlayer1.Ctlcontrols.currentItem.name == axWindowsMediaPlayer1.currentPlaylist.get_Item(listBox1.SelectedIndex - 1).name)
                    {
                        listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
                    }
                }

                if (axWindowsMediaPlayer1.Ctlcontrols.currentItem.name != axWindowsMediaPlayer1.currentPlaylist.get_Item(listBox1.SelectedIndex).name)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.currentItem = axWindowsMediaPlayer1.currentPlaylist.get_Item(listBox1.SelectedIndex);
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            
            if(comboBox1.SelectedIndex == 1) {
                var SongUrl = new Uri(audioList[listBox1.SelectedIndex].url);
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Music files (*.mp3)|*.mp3";
                save.FileName = audioList[listBox1.SelectedIndex].artist + " - " + audioList[listBox1.SelectedIndex].title + ".mp3";
                if (save.ShowDialog()==DialogResult.OK)
                {
                    wc.DownloadFile(SongUrl,save.FileName);
                }
            }

            if (comboBox1.SelectedIndex == 0)
            {
               
                FolderBrowserDialog save = new FolderBrowserDialog();
                if (save.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < audioList.Count(); i++)
                    {
                        var SongUrl = new Uri(audioList[i].url);
                        wc.DownloadFile(SongUrl,save.SelectedPath + "\\" + audioList[i].artist + " - " + audioList[i].title + ".mp3");
                    } 
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex != -1)
            {
                button2.Enabled = true;
            }
        }
    }
}
