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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.statusList = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lastFilledQTY = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.lastFilledAMT = new System.Windows.Forms.TextBox();
            this.totalAMT = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.totalQTY = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.unitrateTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.errorTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.slaveIdfield = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lastFilledQTY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slaveIdfield)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 348);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(526, 22);
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Ready";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(203, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Status";
            // 
            // statusList
            // 
            this.statusList.FormattingEnabled = true;
            this.statusList.Location = new System.Drawing.Point(275, 51);
            this.statusList.Name = "statusList";
            this.statusList.Size = new System.Drawing.Size(82, 21);
            this.statusList.TabIndex = 14;
            this.statusList.SelectedIndexChanged += new System.EventHandler(this.statusList_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(173, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "LastFilledQuantity";
            
            // 
            // lastFilledQTY
            // 
            this.lastFilledQTY.Location = new System.Drawing.Point(266, 90);
            this.lastFilledQTY.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.lastFilledQTY.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lastFilledQTY.Name = "lastFilledQTY";
            this.lastFilledQTY.ReadOnly = true;
            this.lastFilledQTY.Size = new System.Drawing.Size(120, 20);
            this.lastFilledQTY.TabIndex = 16;
            this.lastFilledQTY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lastFilledQTY.ValueChanged += new System.EventHandler(this.lastFilledQTY_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(173, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "LastFilledAmount";
            // 
            // lastFilledAMT
            // 
            this.lastFilledAMT.Location = new System.Drawing.Point(266, 139);
            this.lastFilledAMT.Name = "lastFilledAMT";
            this.lastFilledAMT.ReadOnly = true;
            this.lastFilledAMT.Size = new System.Drawing.Size(100, 20);
            this.lastFilledAMT.TabIndex = 18;
            this.lastFilledAMT.Text = "0";
            // 
            // totalAMT
            // 
            this.totalAMT.Location = new System.Drawing.Point(266, 175);
            this.totalAMT.Name = "totalAMT";
            this.totalAMT.ReadOnly = true;
            this.totalAMT.Size = new System.Drawing.Size(100, 20);
            this.totalAMT.TabIndex = 20;
            this.totalAMT.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(173, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "TotalAmount";
            // 
            // totalQTY
            // 
            this.totalQTY.Location = new System.Drawing.Point(266, 224);
            this.totalQTY.Name = "totalQTY";
            this.totalQTY.ReadOnly = true;
            this.totalQTY.Size = new System.Drawing.Size(100, 20);
            this.totalQTY.TabIndex = 22;
            this.totalQTY.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(173, 224);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "TotalQuantity";
            // 
            // unitrateTextBox
            // 
            this.unitrateTextBox.Location = new System.Drawing.Point(266, 267);
            this.unitrateTextBox.Name = "unitrateTextBox";
            this.unitrateTextBox.ReadOnly = true;
            this.unitrateTextBox.Size = new System.Drawing.Size(100, 20);
            this.unitrateTextBox.TabIndex = 24;
            this.unitrateTextBox.Text = "8090";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(173, 267);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "UnitRate";
            // 
            // errorTextBox
            // 
            this.errorTextBox.Location = new System.Drawing.Point(266, 312);
            this.errorTextBox.Name = "errorTextBox";
            this.errorTextBox.ReadOnly = true;
            this.errorTextBox.Size = new System.Drawing.Size(100, 20);
            this.errorTextBox.TabIndex = 26;
            this.errorTextBox.Text = "40";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(173, 319);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Error";
            // 
            // slaveIdfield
            // 
            this.slaveIdfield.Location = new System.Drawing.Point(266, 12);
            this.slaveIdfield.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.slaveIdfield.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.slaveIdfield.Name = "slaveIdfield";
            this.slaveIdfield.ReadOnly = true;
            this.slaveIdfield.Size = new System.Drawing.Size(120, 20);
            this.slaveIdfield.TabIndex = 29;
            this.slaveIdfield.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.slaveIdfield.ValueChanged += new System.EventHandler(this.slaveIdfield_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(199, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "slaveId";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 370);
            this.Controls.Add(this.slaveIdfield);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.errorTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.unitrateTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.totalQTY);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.totalAMT);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lastFilledAMT);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lastFilledQTY);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.statusList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusStrip);
            this.Name = "Form1";
            this.Text = "Modbus Poll";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lastFilledQTY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slaveIdfield)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox statusList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown lastFilledQTY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox lastFilledAMT;
        private System.Windows.Forms.TextBox totalAMT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox totalQTY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox unitrateTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox errorTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown slaveIdfield;
        private System.Windows.Forms.Label label10;
    }
}

