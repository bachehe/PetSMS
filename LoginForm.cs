﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pet
{
    public partial class LoginForm : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "Pet Shop Management System";
        public LoginForm()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Exit Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string _name = "", _role = "";
                cn.Open();
                cm = new SqlCommand("SELECT name, role FROM tbUser WHERE name=@name and password=@password", cn);
                cm.Parameters.AddWithValue("@name", txtname.Text);
                cm.Parameters.AddWithValue("@password", txtpass.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    _name = dr["name"].ToString();
                    _role = dr["role"].ToString();
                    MessageBox.Show($"Welcome   {_name} | ", "ACCES GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainForm main = new MainForm();
                    main.lblUsername.Text = _name;
                    main.lblRole.Text = _role;

                    if (_role == "Administrator")
                        main.btnUser.Enabled = true;
                    this.Hide();

                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnForget_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please contact your superior", "FORGET PASSWORD", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }














        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
