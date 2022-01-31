using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pet
{
    class DbConnect
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        private string con;

        public string connection()
        {
            con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\tu\PetShop\Pet Shop Management System\dbPetShop.mdf;Integrated Security=True; Connect Timeout=30";
            return con;
        }

        public void executeQuery(string sql)
        {
            try
            {
                cn.ConnectionString = connection();
                cn.Open();
                cm = new SqlCommand(sql, cn);
                cm.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
