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
    public partial class frmAddClient : Form
    {

        SqlConnection con = new SqlConnection();
        SqlCommand cmd;
        string ID;
        public frmAddClient()
        {
            InitializeComponent();
        }

        private void frmAddClient_Load(object sender, EventArgs e)
        {
            Database.dataTableToListView("spClients", "FetchData", lstClients);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            insertToDatabase();
            Database.dataTableToListView("spClients", "FetchData", lstClients);
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

                cmd = new SqlCommand("spClients", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "SaveData");
                cmd.Parameters.AddWithValue("@cID",ID);
                cmd.Parameters.AddWithValue("@cName", txtName.Text);
                cmd.Parameters.AddWithValue("@cAddress", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Contact", txtContact.Text);

                int numRes = cmd.ExecuteNonQuery();
                if (numRes > 0)
                {
                    MessageBox.Show("New Client Saved Successfully !!!");
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
            txtName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtContact.Text = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            if (lstClients.SelectedItems.Count > 0)
            {
                ID = lstClients.FocusedItem.SubItems[0].Text;
                txtName.Text = lstClients.FocusedItem.SubItems[1].Text;
                txtAddress.Text = lstClients.FocusedItem.SubItems[2].Text;
                txtContact.Text = lstClients.FocusedItem.SubItems[3].Text;
            }
            else
            {
                ID = string.Empty;
                txtName.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtContact.Text = string.Empty;
            }
        }
    }
}
