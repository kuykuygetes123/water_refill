using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterRefillSystem.Class;

namespace WaterRefillSystem.Forms
{
    public partial class frmAddUser : Form
    {

        SqlConnection con = new SqlConnection();
        SqlCommand cmd;
        string type;
        public frmAddUser()
        {
            InitializeComponent();
        }

        private void rbtnCashier_CheckedChanged(object sender, EventArgs e)
        {

            // Change the check box position to be opposite its current position.
            if (rbtnCashier.Checked == true)
            {
                type = "cashier";
            }
        }

        private void rbtnOwner_CheckedChanged(object sender, EventArgs e)
        {
            // Change the check box position to be opposite its current position.
            if (rbtnOwner.Checked==true)
            {
                type = "owner";
            }
        }

        private void frmAddUser_Load(object sender, EventArgs e)
        {

        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new frmMainMenu();
            window.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            insertToDatabase();
            cleanTextFields();
        }

        private void insertToDatabase()
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["WaterRefillSystemConn"].ConnectionString;
            con.Open();

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                DataTable dtData = new DataTable();

                cmd = new SqlCommand("spUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "SaveData");
                cmd.Parameters.AddWithValue("@user", txtUser.Text);
                cmd.Parameters.AddWithValue("@pass", txtPass.Text);
                cmd.Parameters.AddWithValue("@type",type);

                int numRes = cmd.ExecuteNonQuery();
                if (numRes > 0)
                {
                    MessageBox.Show("User Saved Successfully !!!");
                    con.Close();
                }
                else
                    MessageBox.Show("Please Try Again !!!");
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:- " + ex.Message);
            }
        }

        private void cleanTextFields()
        {
            txtUser.Text = string.Empty;
            txtPass.Text = string.Empty;
        }
    }
}
