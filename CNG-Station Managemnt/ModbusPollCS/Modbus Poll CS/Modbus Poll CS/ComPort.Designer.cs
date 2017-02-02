namespace Modbus_Poll_CS
{
    partial class ComPort
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstPorts = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nodeCount = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nodeCount)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstPorts);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Location = new System.Drawing.Point(30, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 127);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Port Info";
            // 
            // lstPorts
            // 
            this.lstPorts.FormattingEnabled = true;
            this.lstPorts.Location = new System.Drawing.Point(95, 36);
            this.lstPorts.Name = "lstPorts";
            this.lstPorts.Size = new System.Drawing.Size(82, 21);
            this.lstPorts.TabIndex = 2;
            this.lstPorts.SelectedIndexChanged += new System.EventHandler(this.lstPorts_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(95, 76);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(82, 23);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Connect";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nodeCount);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(337, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(203, 127);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "NodeInfo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "No. of Nodes";
            // 
            // nodeCount
            // 
            this.nodeCount.Location = new System.Drawing.Point(77, 36);
            this.nodeCount.Name = "nodeCount";
            this.nodeCount.Size = new System.Drawing.Size(120, 20);
            this.nodeCount.TabIndex = 1;
            // 
            // ComPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 359);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ComPort";
            this.Text = "Port Selection";
            this.Load += new System.EventHandler(this.ComPort_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nodeCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox lstPorts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nodeCount;
        private System.Windows.Forms.Label label2;

    }
}