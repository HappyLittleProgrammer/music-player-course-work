﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MusicPlayerCourseWork
{
    public partial class Login : Form
    {
        
        private string conn;
        private MySqlConnection connect;

        public Login()
        {
            InitializeComponent();
        }

        private void db_connection()
        {
            try
            {
                conn = "Server=MYSQL5013.HostBuddy.com;Database=db_9e45aa_usersm;Uid=9e45aa_usersm;Pwd=23091996;";
                connect = new MySqlConnection(conn);
                connect.Open();
            }
            catch (MySqlException e)
            {
                throw;
            }
        }

        private bool validate_login(string user, string pass)
        {
            db_connection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from users where username=@user and password=@pass";
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@pass", pass);
            cmd.Connection = connect;
            MySqlDataReader login = cmd.ExecuteReader();
            if (login.Read())
            {
                LoggedUser.LoggedInUser = login.GetString("id");
                connect.Close();
                return true;
            }
            else
            {
                connect.Close();
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string pass = textBox2.Text;

            if (user == "" || pass == "")
            {
                MessageBox.Show("Empty Fields Detected ! Please fill up all the fields");
                return;
            }

            bool r = validate_login(user, pass);

            if (r){
                Close();

                MainForm f2 = new MainForm();
                f2.Show();
                }
            else
                MessageBox.Show("Incorrect Login Credentials");

        }

    }
}
