namespace WaterRefillSystem.Forms
{
    partial class frmPrint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.spClientMonthlyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.billDataSet = new WaterRefillSystem.BillDataSet();
            this.rptViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.spClientMonthlyTableAdapter = new WaterRefillSystem.BillDataSetTableAdapters.spClientMonthlyTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.spClientMonthlyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.billDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // spClientMonthlyBindingSource
            // 
            this.spClientMonthlyBindingSource.DataMember = "spClientMonthly";
            this.spClientMonthlyBindingSource.DataSource = this.billDataSet;
            // 
            // billDataSet
            // 
            this.billDataSet.DataSetName = "BillDataSet";
            this.billDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rptViewer
            // 
            this.rptViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.spClientMonthlyBindingSource;
            this.rptViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.rptViewer.LocalReport.ReportEmbeddedResource = "WaterRefillSystem.BillingRprt.rdlc";
            this.rptViewer.Location = new System.Drawing.Point(0, 0);
            this.rptViewer.Name = "rptViewer";
            this.rptViewer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rptViewer.ServerReport.BearerToken = null;
            this.rptViewer.Size = new System.Drawing.Size(800, 450);
            this.rptViewer.TabIndex = 0;
            // 
            // spClientMonthlyTableAdapter
            // 
            this.spClientMonthlyTableAdapter.ClearBeforeFill = true;
            // 
            // frmPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rptViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmPrint";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Form2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spClientMonthlyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.billDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rptViewer;
        private System.Windows.Forms.BindingSource spClientMonthlyBindingSource;
        private BillDataSet billDataSet;
        private BillDataSetTableAdapters.spClientMonthlyTableAdapter spClientMonthlyTableAdapter;
    }
}