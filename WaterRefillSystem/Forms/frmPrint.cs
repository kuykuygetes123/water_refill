using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterRefillSystem.Class.Procedures;

namespace WaterRefillSystem.Forms
{
    public partial class frmPrint : Form
    {
        string clientId, Name;
        DateTime from, to;
        public void showDialog(string clientId, string Name, DateTime from, DateTime to)
        {
            this.clientId = clientId;
            this.Name = Name;
            this.from = from;
            this.to = to;

            this.ShowDialog(); //Display and activate this form (Form2)

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        public frmPrint()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'billDataSet.spClientMonthly' table. You can move, or remove it, as needed.
            this.spClientMonthlyTableAdapter.Fill(this.billDataSet.spClientMonthly, this.clientId, this.from, this.to);

            ReportParameter rp = new ReportParameter("Name", this.Name, false);

            string localDate = DateTime.Now.ToString("dddd, MMMM dd yyyy");
            ReportParameter rp2 = new ReportParameter("Date", localDate, false);

            string period = this.from.ToString("MMMM yyyy");
            ReportParameter rp3 = new ReportParameter("period", period, false);

            string totalAmount = Procedure.computeTotalAmount(this.clientId, this.from.ToString(), this.to.ToString());
            ReportParameter rp4 = new ReportParameter("total", totalAmount, false);

            this.rptViewer.LocalReport.SetParameters(new ReportParameter[] { rp, rp2, rp3, rp4 });

            this.rptViewer.RefreshReport();
        }
    }
}
