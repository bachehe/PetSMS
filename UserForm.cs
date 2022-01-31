﻿using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pet
{
    public partial class UserForm : Form 
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "Pet Shop Management System";

        public UserForm()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            LoadUser();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadUser();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UserModule module = new UserModule(this);
            module.ShowDialog();
        }
        

        private void btnClose_Click(object sender, EventArgs e)
        {
            Exit exit = new Exit();
            exit.ShowDialog();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUser.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                UserModule module = new UserModule(this);
                module.lbluid.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtName.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtAddress.Text = dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                module.txtPhone.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();
                module.cbRole.Text = dgvUser.Rows[e.RowIndex].Cells[5].Value.ToString();
                module.dtDob.Text = dgvUser.Rows[e.RowIndex].Cells[6].Value.ToString();
                module.txtPassword.Text = dgvUser.Rows[e.RowIndex].Cells[7].Value.ToString();

                module.btnSave.Enabled = false;
                module.btnUpdate.Enabled = true;
                module.ShowDialog();
            }
            else if(colName == "Delete")
            {
                if(MessageBox.Show("Are you sure you want to delete?", "Delete Record", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dbcon.executeQuery("DELETE FROM tbUser WHERE id LIKE '" +
                        dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString() + "'"); //using dbcon to shorten

                    MessageBox.Show("User data has been succesfully removed", title, MessageBoxButtons.OK,  MessageBoxIcon.Question); 
                }
            }
            LoadUser();
        }

        #region Method
        public void LoadUser()
        {
            int i = 0;
            dgvUser.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbUser WHERE CONCAT(name, address, phone, dob, role) " +
                "LIKE '%" + txtSearch.Text + "%'", cn);
            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvUser.Rows.Add(i,
                    dr[0].ToString(),
                    dr[1].ToString(),
                    dr[2].ToString(),
                    dr[3].ToString(),
                    dr[4].ToString(),
                    DateTime.Parse(dr[5].ToString()).ToShortDateString(),
                    dr[6].ToString());
            }
            dr.Close();
            cn.Close();
        }
        #endregion Method
    }
}
