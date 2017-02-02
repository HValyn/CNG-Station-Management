namespace Modbus_Poll_CS
{
    partial class Form1
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cOMPortSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.transactionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cNGDatabaseDataSet = new Modbus_Poll_CS.CNGDatabaseDataSet();
            this.transactionTableAdapter = new Modbus_Poll_CS.CNGDatabaseDataSetTableAdapters.TransactionTableAdapter();
            this.tableAdapterManager = new Modbus_Poll_CS.CNGDatabaseDataSetTableAdapters.TableAdapterManager();
            this.tableAdapterManager1 = new Modbus_Poll_CS.CNGDatabaseDataSetTableAdapters.TableAdapterManager();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transactionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cNGDatabaseDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 489);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(864, 22);
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Ready";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cOMPortSelectionToolStripMenuItem});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.configurationToolStripMenuItem.Text = "GetData";
            // 
            // cOMPortSelectionToolStripMenuItem
            // 
            this.cOMPortSelectionToolStripMenuItem.Name = "cOMPortSelectionToolStripMenuItem";
            this.cOMPortSelectionToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.cOMPortSelectionToolStripMenuItem.Text = "Show Transactions";
            this.cOMPortSelectionToolStripMenuItem.Click += new System.EventHandler(this.cOMPortSelectionToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(864, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM6";
            // 
            // transactionBindingSource
            // 
            this.transactionBindingSource.DataMember = "Transaction";
            this.transactionBindingSource.DataSource = this.cNGDatabaseDataSet;
            // 
            // cNGDatabaseDataSet
            // 
            this.cNGDatabaseDataSet.DataSetName = "CNGDatabaseDataSet";
            this.cNGDatabaseDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // transactionTableAdapter
            // 
            this.transactionTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.TransactionTableAdapter = this.transactionTableAdapter;
            this.tableAdapterManager.UpdateOrder = Modbus_Poll_CS.CNGDatabaseDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // tableAdapterManager1
            // 
            this.tableAdapterManager1.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager1.Connection = null;
            this.tableAdapterManager1.TransactionTableAdapter = null;
            this.tableAdapterManager1.UpdateOrder = Modbus_Poll_CS.CNGDatabaseDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(864, 465);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(864, 511);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Station Sates";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transactionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cNGDatabaseDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cOMPortSelectionToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitAmountDataGridViewTextBoxColumn;
        private CNGDatabaseDataSet cNGDatabaseDataSet;
        private System.Windows.Forms.BindingSource transactionBindingSource;
        private CNGDatabaseDataSetTableAdapters.TransactionTableAdapter transactionTableAdapter;
        private CNGDatabaseDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private CNGDatabaseDataSetTableAdapters.TableAdapterManager tableAdapterManager1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

