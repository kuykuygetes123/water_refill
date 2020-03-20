using System;
using System.Windows.Forms;
using WaterRefillSystem.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using WaterRefillSystem.Class;
using WaterRefillSystem.Class.Procedures;

namespace WaterRefillSystem
{
    public partial class frmDistOwner : Form
    {
        bool chkModifyButton = false;

        public frmDistOwner()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Is DR Number "+txtDR.Text+" correct?", "Before Saving...",MessageBoxButtons.OKCancel);
            if(result == DialogResult.OK)
            {
                Database.insert(lstNewAdded,txtCNumber.Text, txtDR.Text, dtrIssued.Text, txtQuantity.Text,txtPrice.Text,cmbType.Text,txtReturned.Text, cmbIncharge.Text,txtCollection.Text,txtRemarks.Text);
                Database.dataTableToListView("spDelivery", "FetchData",lstNewAdded);
                cleanTextFields();
            }
            else
            {
                return;
            }
        }

        private void btnExitModi_MouseClick(object sender, MouseEventArgs e)
        {
            btnModify.Visible = true;
            btnAdd.Visible = true;
            btnDelete.Visible = false;
            lstNewAdded.CheckBoxes = false;
        }

        private void lblAddName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var addClient = new frmAddClient();
            addClient.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Database.searchClientAuto(txtCNumber,txtName);
            Database.dataTableToListView("spDelivery", "FetchData",lstNewAdded);
        }

        private void frmDistOwner_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstNewAdded.Items.Count; i++)
            {

                if (lstNewAdded.Items[i].Checked)
                {
                    String drNum = lstNewAdded.Items[i].SubItems[1].Text;
                    Procedure.deleteData(drNum);

                    lstNewAdded.Items[i].Remove();
                }

            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            MessageBox.Show("EDIT MODE!");
            modify();
            chkModifyButton = true;

        }

        private void btnExitModi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("All Modifications are Successfully Saved!");
            chkModifyButton = false;
            lblAddName.Visible = true;
            txtDR.Enabled = true;
            cleanTextFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Database.insert(lstNewAdded, txtCNumber.Text, txtDR.Text, dtrIssued.Text, txtQuantity.Text, txtPrice.Text, cmbType.Text, txtReturned.Text, cmbIncharge.Text, txtCollection.Text, txtRemarks.Text);
            Database.dataTableToListView("spDelivery", "FetchData",lstNewAdded);
            cleanTextFields();
        }

        private void lstNewAdded_ItemActivate(object sender, EventArgs e)
        {

            //DataTable data = fetchData("spDelivery", "FetchRecord");
            //int a = lstNewAdded.FocusedItem.Index;
            if (lstNewAdded.SelectedItems.Count > 0 && chkModifyButton.Equals(true))
            {
                //txtName.Text = data.Rows[a]["Client"].ToString();
                txtName.Text = lstNewAdded.FocusedItem.SubItems[0].Text;
                txtDR.Text = lstNewAdded.FocusedItem.SubItems[1].Text;
                dtrIssued.Text = lstNewAdded.FocusedItem.SubItems[2].Text;
                txtReturned.Text = lstNewAdded.FocusedItem.SubItems[3].Text;
                txtQuantity.Text = lstNewAdded.FocusedItem.SubItems[4].Text;
                txtPrice.Text = lstNewAdded.FocusedItem.SubItems[5].Text;
                cmbType.Text = lstNewAdded.FocusedItem.SubItems[7].Text;
                cmbIncharge.Text = lstNewAdded.FocusedItem.SubItems[8].Text;
                txtCollection.Text = lstNewAdded.FocusedItem.SubItems[9].Text;
                txtRemarks.Text = lstNewAdded.FocusedItem.SubItems[10].Text;

            }
            else
            {
                txtName.Text = string.Empty;
                txtDR.Text = string.Empty;
                dtrIssued.Text = string.Empty;
                txtReturned.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                txtPrice.Text = string.Empty;
                cmbType.Text = string.Empty;
                cmbIncharge.Text = string.Empty;
                txtCollection.Text = string.Empty;
                txtRemarks.Text = string.Empty;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            var window = new frmMainMenu();
            window.Show();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void txtCNumber_TextChanged(object sender, EventArgs e)
        {
            DataTable data = Procedure.FetchRecords("spClients", "FetchRecord", txtCNumber.Text, null);

            if (data.Rows.Count > 0)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    txtCNumber.Text = data.Rows[i]["cID"].ToString();
                    txtName.Text = data.Rows[i]["Name"].ToString();
                    txtContact.Text = data.Rows[i]["Contact"].ToString();
                    txtAddress.Text = data.Rows[i]["ADDRESS"].ToString();
                }
            }
            else
            {
                txtName.Text = string.Empty;
                txtContact.Text = string.Empty;
                txtAddress.Text = string.Empty;
            }

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            DataTable data = Procedure.FetchRecords("spClients", "FetchRecord", null, txtName.Text);

            if (data.Rows.Count > 0)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    txtCNumber.Text = data.Rows[i]["cID"].ToString();
                    txtName.Text = data.Rows[i]["Name"].ToString();
                    txtContact.Text = data.Rows[i]["Contact"].ToString();
                    txtAddress.Text = data.Rows[i]["ADDRESS"].ToString();
                }
            }
            else
            {
                txtCNumber.Text = string.Empty;
                txtContact.Text = string.Empty;
                txtAddress.Text = string.Empty;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
           lstNewAdded.Items.Clear();
           if (txtSearch.TextLength == 0 && lstNewAdded.Focused == false)
            {
                txtSearch.Text = "Search Name or Delivery Number...";
                Database.dataTableToListView("spDelivery", "FetchData",lstNewAdded);
                return;
            }
            else
            {
                DataTable data = Procedure.fetchSearch(txtSearch.Text);

                if (data.Rows.Count > 0) {
                    foreach (DataRow row in data.Rows)
                    {
                        ListViewItem item = new ListViewItem(row[0].ToString());
                        for (int i = 1; i < data.Columns.Count; i++)
                        {
                            item.SubItems.Add(row[i].ToString());
                        }
                        lstNewAdded.Items.Add(item);
                    }
                }
                else
                {
                    lstNewAdded.Items.Clear();
                }
            }
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            txtSearch.SelectionStart = 0;
            txtSearch.SelectionLength = txtSearch.Text.Length;
        }

        //FORM MODIFIERS
        private void modify()
        {
            txtDR.Enabled = false;
            lblAddName.Visible = false;
            btnModify.Visible = false;
            btnAdd.Visible = false;
            btnDelete.Visible = true;
            lstNewAdded.CheckBoxes = true;
        }

        private void cleanTextFields()
        {
            txtName.Text = string.Empty;
            txtDR.Text = string.Empty;

            DateTime today = DateTime.Today;
            dtrIssued.Text = today.ToString("MM/dd/yyyy");

            txtReturned.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtPrice.Text = string.Empty;
            cmbType.Text = string.Empty;
            cmbIncharge.Text = string.Empty;
            txtCollection.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }

    }
}