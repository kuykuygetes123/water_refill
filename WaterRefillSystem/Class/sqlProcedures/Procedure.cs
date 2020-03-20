using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterRefillSystem.Class.Procedures
{
    class Procedure
    {
        static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["WaterRefillSystemConn"].ConnectionString);
        static SqlCommand cmd;

        public static DataTable fetchData(string procedure, string action)
        {   
            con.Open();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataTable dtData = new DataTable();
            cmd = new SqlCommand(procedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType", action);
            SqlDataAdapter sqlSda = new SqlDataAdapter(cmd);
            sqlSda.Fill(dtData);

            con.Close();
            return dtData;
        }

        public static DataTable fetchSearch(string toBeSearch)
        {
            con.Open();

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataTable dtData = new DataTable();
            cmd = new SqlCommand("spDelivery", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType", "SearchData");
            cmd.Parameters.AddWithValue("@Search", toBeSearch);
            SqlDataAdapter sqlSda = new SqlDataAdapter(cmd);
            sqlSda.Fill(dtData);

            con.Close();
            return dtData;
        }

        public static DataTable FetchRecords(string procedure, string action, string cID, string name)
        {
            con.Open();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            DataTable dtData = new DataTable();
            cmd = new SqlCommand(procedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ActionType", action);
            cmd.Parameters.AddWithValue("@cID", cID);
            cmd.Parameters.AddWithValue("@cName", name);
            SqlDataAdapter sqlSda = new SqlDataAdapter(cmd);
            sqlSda.Fill(dtData);


            con.Close();
            return dtData;
        }

        public static void deleteData(String drNum)
        {

            con.Open();

            if (!string.IsNullOrEmpty(drNum))
            {
                try
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    DataTable dtData = new DataTable();
                    cmd = new SqlCommand("spDelivery", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ActionType", "DeleteData");
                    cmd.Parameters.AddWithValue("@DrNum", drNum);
                    int numRes = cmd.ExecuteNonQuery();
                    if (numRes > 0)
                    {
                        MessageBox.Show("Record Deleted Successfully !!!");
                        con.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please Try Again !!!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:- " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please Select A Record !!!");
            }
        }

        public static string computeTotalAmount(string clientID, string dateFrom, string dateTo)
        {
            con.Open();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string function = @"SELECT [dbo].[ComputeTotalAmount](@clientID,@fromDate,@toDate) AS TotalAmount;";
            cmd = new SqlCommand(function, con);

            cmd.Parameters.AddWithValue("@clientID", clientID);
            cmd.Parameters.AddWithValue("@fromDate", dateFrom);
            cmd.Parameters.AddWithValue("@toDate", dateTo);

            //execute the SQLCommand
            string functionResult = (string)cmd.ExecuteScalar();

            con.Close();
            return functionResult;

        }

        public static DataTable fillDataTable(string clientID, DateTime dateFrom, DateTime dateTo)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            DataTable dtData = new DataTable();
            cmd = new SqlCommand("spClientMonthly", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@clientID", clientID);
            cmd.Parameters.AddWithValue("@fromDate", dateFrom);
            cmd.Parameters.AddWithValue("@toDate", dateTo);
            SqlDataAdapter sqlSda = new SqlDataAdapter(cmd);
            sqlSda.Fill(dtData);

            con.Close();
            return dtData;
        }
    }
}
