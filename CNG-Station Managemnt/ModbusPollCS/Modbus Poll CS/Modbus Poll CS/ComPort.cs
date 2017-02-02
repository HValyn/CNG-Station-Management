using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace Modbus_Poll_CS
{
    public partial class ComPort : Form
    {
        SerialPort sp = new SerialPort();
        public ComPort()
        {
            InitializeComponent();
            LoadListboxes();

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (nodeCount.Value < 1) 
            {
                MessageBox.Show("Please Select atleast 1 node to continue.");
            }
            else
            {
                Form1 form1 = new Form1(lstPorts.SelectedItem.ToString(), (int)nodeCount.Value);
                //form1.selectedPort = lstPorts.SelectedItem.ToString();
                this.Visible = false;
                form1.ShowDialog();
                this.Visible = true;
            }
            
        }
        #region Load Listboxes
        private void LoadListboxes()
        {
            //Three to load - ports, baudrates, datetype.  Also set default textbox values:
            //1) Available Ports:
            string[] ports = SerialPort.GetPortNames();

            foreach (string port in ports)
            {
                lstPorts.Items.Add(port);
            }
            lstPorts.SelectedIndex = 0;
           
        }
        #endregion
        private void ComPort_Load(object sender, EventArgs e)
        {

        }

        private void lstPorts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
