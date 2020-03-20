using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterRefillSystem.Forms
{
    public partial class frmDistCashier : Form
    {
        public frmDistCashier()
        {
            InitializeComponent();
        }

        private void frmDistCashier_Load(object sender, EventArgs e)
        {

        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new frmLogin();
            window.Show();
        }
    }
}
