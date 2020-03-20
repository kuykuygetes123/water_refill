using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WaterRefillSystem.Forms
{
    public partial class frmLogin : Form
    {
        string userName = Properties.Settings.Default.username;
        string userPassword = Properties.Settings.Default.passUser;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (userName.Equals(null))
            {
                return;
            }
            else
            {
                checkBox1.Checked = true;
                txtUser.Text = userName;
                txtPass.Text = userPassword;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["WaterRefillSystemConn"].ConnectionString;


            SqlCommand cmd = new SqlCommand("SELECT accUser,accPassword,accType FROM tbAccounts WHERE accUser= @Username AND accPassword= @Password", con);
            
            SqlParameter uName = new SqlParameter("@Username", SqlDbType.VarChar);
            SqlParameter uPass = new SqlParameter("@Password", SqlDbType.VarChar);

            uName.Value = txtUser.Text; 
            uPass.Value = txtPass.Text;

            cmd.Parameters.Add(uName);
            cmd.Parameters.Add(uPass);
            cmd.Connection.Open();

            SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            int count = 0;
            string accType = string.Empty;
            while (myReader.Read())
            {
                count+=1;
                accType = myReader["accType"].ToString();
            }
            if(count == 1)
            {
                MessageBox.Show("Login sucess Welcome " + txtUser.Text + "!");
                this.Hide();

                if (accType == "admin")
                {
                    var Distribution = new frmMainMenu();
                    Distribution.Show();
                }
                else
                {
                    var Distribution = new frmDistCashier();
                    Distribution.Show();
                }
            }
            else
            {
                MessageBox.Show("Invalid Login please check username and password");
            }

            if(con.State == ConnectionState.Open)
            {
                con.Dispose();
            }
        }

        private void checkBox1_Leave(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                userName = txtUser.Text;
                userPassword = txtPass.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                userName = "";
                userPassword = "";
                Properties.Settings.Default.Save();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = false;
            btnShow.Visible = false;
        }

        private void btnUnshow_Click(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;
            btnShow.Visible = true;
        }

        private void asdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtUser_Click(object sender, EventArgs e)
        {
            txtUser.Text = "";
            txtUser.TextAlign = HorizontalAlignment.Center;
            txtUser.ForeColor = Color.Snow;

        }

        private void txtPass_Click(object sender, EventArgs e)
        {
            txtPass.Text = "";
            txtPass.TextAlign = HorizontalAlignment.Center;
            txtPass.UseSystemPasswordChar = true;
            txtUser.ForeColor = Color.Snow;
        }

    }
}
