﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RailForms
{
    public partial class SystemUserLogin : Form
    {
        public SystemUserLogin()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

            try
            {
                Pages.sysLgin = new SystemLogin();
                Pages.sysLgin.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            textUsername.Text = "";
            textPassword.Text = "";
        }

        private void btnUL_Click(object sender, EventArgs e)
        {

            try
            {
                String user_id = textUsername.Text.Trim();
                String pass = textPassword.Text.Trim();
                if (textUsername.Text != "" & textPassword.Text != "")
                {
                    String queryText = "SELECT Count(*) FROM userinfo WHERE username = @user_id AND password = @pass";
                    SqlConnection con = new SqlConnection("Data Source=(local);Initial Catalog=Railway_Reservation_System;Integrated Security=True");

                    using (SqlCommand cmd = new SqlCommand(queryText, con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@user_id", textUsername.Text + "_user");  // cmd is SqlCommand 
                        cmd.Parameters.AddWithValue("@pass", textPassword.Text);

                        int result = (int)cmd.ExecuteScalar();

                        if (result > 0)
                        {
                            MessageBox.Show("LOGIN SUCCEED!!!!");
                            Pages.sysUsrPnl = new SysUserPanel(textUsername.Text+"_user");
                            Pages.sysUsrPnl.Show();
                            this.Close();
                        }
                        else
                            MessageBox.Show("User Not Found!");
                        con.Close();
                    }
                }
                else
                    MessageBox.Show("Fill all fields");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SystemUserLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
