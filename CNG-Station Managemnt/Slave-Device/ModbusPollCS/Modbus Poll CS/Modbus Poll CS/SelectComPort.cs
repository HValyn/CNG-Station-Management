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
    public partial class SelectComPort : Form
    {
        SerialPort sp = new SerialPort();
        public SelectComPort()
        {
            InitializeComponent();
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                comPorts.Items.Add(port);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comPorts.Items.Count == 0)
            {
                MessageBox.Show("No Com Ports Found");
                this.Close();
            }
            else if(comPorts.SelectedItem != null) 
            {
                this.Close();
            }
        }
    }
}
