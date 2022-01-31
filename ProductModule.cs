using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pet
{
    public partial class ProductModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "Pet Shop Management System";

        bool check = false;
        ProductForm product;
        public ProductModule(ProductForm form)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            product = form;
            cbCategory.SelectedIndex = 0;
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to register this product?",
                   "Product Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbProduct(pname, ptype, pcategory, pqty, pprice)" +
                            "VALUES(@pname, @ptype, @pcategory, @pqty, @pprice)", cn);
                        cm.Parameters.AddWithValue("@pname", txtName.Text);
                        cm.Parameters.AddWithValue("@ptype", txtType.Text);
                        cm.Parameters.AddWithValue("@pcategory", cbCategory.Text);
                        cm.Parameters.AddWithValue("@pqty", int.Parse(txtQty.Text));
                        cm.Parameters.AddWithValue("@pprice", double.Parse(txtPrice.Text));

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Product has been successfully registered!", title);
                        Clear();
                        product.LoadProduct();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to edit this product?",
                   "Product Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("UPDATE tbProduct SET pname=@pname, ptype=@ptype, pcategory=@pcategory, pqty=@pqty, pprice=@pprice WHERE pcode=@pcode", cn);
                        cm.Parameters.AddWithValue("@pcode", lblPcode.Text);
                        cm.Parameters.AddWithValue("@pname", txtName.Text);
                        cm.Parameters.AddWithValue("@ptype", txtType.Text);
                        cm.Parameters.AddWithValue("@pcategory", cbCategory.Text);
                        cm.Parameters.AddWithValue("@pqty", int.Parse(txtQty.Text));
                        cm.Parameters.AddWithValue("@pprice", double.Parse(txtPrice.Text));

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Product has been successfully updated!", title);
                        Clear();
                        product.LoadProduct();
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e) //taking away errors opportunity
        {
            //only digit
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only digit nb
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //only decimal nb
            if((e.KeyChar=='.') && ((sender as TextBox).Text.IndexOf('.')>-1))
            {
                e.Handled = true;
            }
        }
        #region Method
        public void Clear()
        {
            txtName.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            txtType.Clear();
            cbCategory.SelectedIndex = 0;

            btnUpdate.Enabled = false;
        }

        public void CheckField()
        {
            if (txtName.Text == "" | txtPrice.Text == "" | txtQty.Text == "" | txtType.Text == "")
            {
                MessageBox.Show("Required data field!", "Warning");
                return;
            }
            check = true;
        }
        #endregion Method

    }
}
