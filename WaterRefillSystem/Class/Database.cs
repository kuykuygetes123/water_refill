using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterRefillSystem.Class.Procedures;

namespace WaterRefillSystem.Class
{
    class Database
    {
        static SqlConnection con = new SqlConnection();
        static SqlCommand cmd;

        public static void insert(ListView listView,string num, string dr, string issueDate, string qty, string price, string type, string ret, string inchrge, string coll, string remark)
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

                cmd = new SqlCommand("spDelivery", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ActionType", "SaveData");
                cmd.Parameters.AddWithValue("@clientID", num);
                cmd.Parameters.AddWithValue("@DrNum", dr);
                cmd.Parameters.AddWithValue("@issuDate", issueDate);
                cmd.Parameters.AddWithValue("@Qty", qty);
                cmd.Parameters.AddWithValue("@Unit", price);
                cmd.Parameters.AddWithValue("@PayType", type);
                cmd.Parameters.AddWithValue("@returned", ret);
                cmd.Parameters.AddWithValue("@incharge", inchrge);
                cmd.Parameters.AddWithValue("@delCollect", coll);
                cmd.Parameters.AddWithValue("@remarks", remark);

                int numRes = cmd.ExecuteNonQuery();
                if (numRes > 0)
                {
                    MessageBox.Show("Record Saved Successfully !!!");
                    con.Close();

                    listView.Items.Clear();
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

        public static void dataTableToListView(string procedure, string action, ListView listView)
        {
            listView.Items.Clear();
            
            DataTable data = Procedure.fetchData(procedure, action);
            foreach (DataRow row in data.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (int i = 1; i < data.Columns.Count; i++)
                {
                    item.SubItems.Add(row[i].ToString());
                }
                listView.Items.Add(item);
            }
        }
        public static void searchClientAuto(TextBox txtCNumber, TextBox txtName)
        {
            DataTable data = Procedure.fetchData("spClients", "FetchData");
            AutoCompleteStringCollection coll = new AutoCompleteStringCollection();
            AutoCompleteStringCollection col2 = new AutoCompleteStringCollection();

            if (data.Rows.Count > 0)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {

                    coll.Add(data.Rows[i]["cID"].ToString());
                    col2.Add(data.Rows[i]["Name"].ToString());
                }
            }
            else
            {
                MessageBox.Show("Name not found");
            }

            txtCNumber.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtCNumber.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtCNumber.AutoCompleteCustomSource = coll;


            txtName.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtName.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtName.AutoCompleteCustomSource = col2;
        }
    }
}
