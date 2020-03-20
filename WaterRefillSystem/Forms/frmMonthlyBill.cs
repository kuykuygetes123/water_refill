using Microsoft.Reporting.WinForms;
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
using WaterRefillSystem.Class.Procedures;
using Dapper;

namespace WaterRefillSystem.Forms
{
    public partial class frmMonthlyBill : Form
    {
        public frmMonthlyBill()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            spClientMonthlyBindingSource1.DataSource=Procedure.fillDataTable(txtClientID.Text, dtpFrom.Value, dtpTo.Value);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Database.searchClientAuto(txtClientID, txtName);
        }

        private void txtClientID_TextChanged(object sender, EventArgs e)
        {
            DataTable data = Procedure.FetchRecords("spClients", "FetchRecord", txtClientID.Text, null);

            if (data.Rows.Count > 0)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    txtName.Text = data.Rows[i]["Name"].ToString();

                    foreach (DataRow row in data.Rows)
                    {
                        ListViewItem item = new ListViewItem(row[0].ToString());

                        item.SubItems.Add(data.Rows[i]["cID"].ToString());
                        item.SubItems.Add(data.Rows[i]["Name"].ToString());
                    }
                }
            }
            else
            {
                txtName.Text = string.Empty;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(frmPrint frm = new frmPrint())
            {
                frm.showDialog(txtClientID.Text,txtName.Text, DateTime.Parse(dtpFrom.Text), DateTime.Parse(dtpTo.Text));
                
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new frmMainMenu();
            window.Show();
        }
    }
}
