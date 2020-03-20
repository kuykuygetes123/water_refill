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
    public partial class frmMainMenu : Form
    {
        public frmMainMenu()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new frmDistOwner();
            window.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new frmAddClient();
            window.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new frmMonthlyBill();
            window.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var window = new frmAddUser();
            window.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
