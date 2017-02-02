using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.IO.Ports;
using System.Globalization;

namespace Modbus_Poll_CS
{
    public partial class Form1 : Form
    {
        string selectedPort = "";
        modbus mb = new modbus();
        SerialPort sp = new SerialPort();
        static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        bool isPolling = false;
        byte type = 0x01;
        Byte[] errorMessage;
        Boolean[] flagArray;
        Boolean[] PowerError;
        NodeControl[] nodeControls;
        int totalNodes;
        

        public  void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
           // if (!isPolling) 
               // 
            timer.Stop();
            isPolling = true;
            for(byte i = 1; i <= totalNodes; i++)
            {
                GetStatus(i);
            }
            timer.Start();

        }       

       /* public Form1()
        {
            InitializeComponent();
           // LoadListboxes();
            timer.Tick += new EventHandler(TimerEventProcessor);

            timer.Interval = 1000;
            StartPoll();
            groupBox7.Click += new EventHandler(showDetailViewContrioller);
        }*/
        public Form1(String Port, int nodeCount) 
        {
            InitializeComponent();
            // LoadListboxes();
            flagArray = new Boolean[nodeCount];
            PowerError = new Boolean[nodeCount];
            errorMessage = new Byte[nodeCount];
            nodeControls = new NodeControl[nodeCount];
            totalNodes = nodeCount;
            timer.Tick += new EventHandler(TimerEventProcessor);
            selectedPort = Port;
           // timer.Interval = 1000;
          //  groupBox7.Click += new EventHandler(showDetailViewContrioller);
            
            tableLayoutPanel1.AutoSize = true;
            //tableLayoutPanel1.Name = "Desk";
            //tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.RowCount = 0;
            tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;

            
            for (int i = 0; i < nodeCount; i++) 
            {
                // NodeControl newControl = new NodeControl();
                nodeControls[i] = new NodeControl();
                /*Label newControl = new Label();
                newControl.Text = i.ToString();*/
                nodeControls[i].setIndexandFuelType((i+1), "CNG");
                tableLayoutPanel1.Controls.Add(nodeControls[i], 0, i);

                

            }
            StartPoll();
        }
        public void showDetailViewContrioller(Object myObject, EventArgs myEventArgs)
        {
            // if (!isPolling) 
            DetailNode detailNode = new DetailNode();
            detailNode.ShowDialog();
            
        } 
        

        #region Start and Stop Procedures
        public void StartPoll()
        {

            //Open COM port using provided settings:
            if (mb.Open(selectedPort, 9600,
                8, Parity.None, StopBits.One))
            {
                //btnStart.Enabled = false;

               timer.Interval = 100;
               timer.Start();
            }
            lblStatus.Text = mb.modbusStatus;
            
        }
        public void StopPoll()
        {
            //Stop timer and close COM port:
            isPolling = false;
           // timer.Stop();
            mb.Close();

            //btnStart.Enabled = true;

            lblStatus.Text = mb.modbusStatus;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartPoll();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopPoll();
        }

        #endregion


        #region Methods to get Register Values

        public uint get32bitValue(ushort address, byte slaveId)
        {
            byte[] values = new byte[8];
            byte[] nodeAmount = new byte[4];
            for (ushort i = 0; i < 4; i++)
            {
                int newAddress = address + i;

                if (mb.NetwayProtocol(type, Convert.ToUInt16(newAddress), 0x00, slaveId, ref values))
                {
                    nodeAmount[i] = values[4];
                }
                else
                {
                    return 0;
                }

            }
            return BitConverter.ToUInt32(nodeAmount, 0);
        }


        public ushort get16bitValue(ushort address,byte slaveId)
        {
            byte[] values = new byte[8];
            byte[] nodeAmount = new byte[2];
            for (ushort i = 0; i < 2; i++)
            {
              int newAddress = address + i;

              if (mb.NetwayProtocol(type, Convert.ToUInt16(newAddress), 0x00, slaveId, ref values))
              {
                  nodeAmount[i] = values[4];
              }
              else
              {
                  return 0;
              }

            }
             return BitConverter.ToUInt16(nodeAmount, 0);
        }

        public byte get8bitValue(ushort address, byte slaveId)
        {
            byte[] values = new byte[8];
            byte nodeAmount = 0;

            if (mb.NetwayProtocol(type, address, 0x00, slaveId, ref values))
                nodeAmount = values[4];
            else
            {
                return 0;
            }

            return nodeAmount;
        }

        public string getStatusofNode(byte slaveId)
        {
            byte nodeStatus = get8bitValue( 51, slaveId);
            String status = "";
            
            switch (nodeStatus)
            {
                case 1:
                    status = "Error-";
                    errorMessage[(slaveId-1)] = get8bitValue(44,slaveId);
                   // groupBox4.BackColor = Color.Yellow;
                    nodeControls[(slaveId-1)].setbackColor(Color.Yellow);
                    break;
                case 2:
                    status = "Ready";
                    nodeControls[(slaveId-1)].setbackColor(Color.Green);
                   // groupBox4.BackColor = Color.Green;
                    PowerError[(slaveId-1)] = false;
                    break;
                case 3:
                    status = "Filling In Progress";
                    nodeControls[(slaveId - 1)].setbackColor(Color.Red);
                   // groupBox4.BackColor = Color.Red;
                    flagArray[(slaveId - 1)] = true;
                    PowerError[(slaveId - 1)] = false;
                    break;
                case 4:
                    status = "Nozzle UP";
                    nodeControls[(slaveId - 1)].setbackColor(Color.Orange);
                    //groupBox4.BackColor = Color.Orange;
                    PowerError[(slaveId - 1)] = false;
                    break;
                case 5:
                    status = "EndFill";
                    nodeControls[(slaveId - 1)].setbackColor(Color.Blue);
                    //groupBox4.BackColor = Color.Blue;
                    PowerError[(slaveId - 1)] = false;
                    break;
                case 6:
                    status = "Payment";
                    PowerError[(slaveId - 1)] = false;
                    break;
            }
            if (flagArray[(slaveId - 1)] && nodeStatus != 3) 
            {
                GetTotals(slaveId);
                CNGDatabaseDataSet.TransactionRow newTransactionRow = this.cNGDatabaseDataSet.Transaction.NewTransactionRow();
                newTransactionRow.ReferenceId =  this.cNGDatabaseDataSet.Transaction.Rows.Count;
                newTransactionRow.Node = slaveId;
                newTransactionRow.TransactionTime = DateTime.Now;

                if (nodeStatus == 1)
                {
                    newTransactionRow.Error = errorMessage[(slaveId - 1)];
                }

                newTransactionRow.TotalAmount = get32bitValue(68, slaveId);
                newTransactionRow.TotalQuantity = get32bitValue(72, slaveId);
                newTransactionRow.UnitRate = get16bitValue(64604, slaveId); 
                newTransactionRow.LastFilledAmount = get32bitValue(64596, slaveId);
                newTransactionRow.LastFilledQuantity = get16bitValue(64600, slaveId);
                if (errorMessage[(slaveId - 1)] == 40)
                {
                    PowerError[(slaveId - 1)] = true;
                }
                else
                {
                    PowerError[(slaveId - 1)] = false;
                }

                // Add the row to the Region table
                this.cNGDatabaseDataSet.Transaction.Rows.Add(newTransactionRow);

                // Save the new row to the database
                this.transactionTableAdapter.Update(this.cNGDatabaseDataSet.Transaction);
                //Node++;


                flagArray[(slaveId - 1)] = false;
            }
            if (nodeStatus == 1 && errorMessage[(slaveId - 1)] == 40 && PowerError[(slaveId - 1)] == false) 
            {
                GetTotals(slaveId);
                CNGDatabaseDataSet.TransactionRow newTransactionRow = this.cNGDatabaseDataSet.Transaction.NewTransactionRow();
                newTransactionRow.ReferenceId = this.cNGDatabaseDataSet.Transaction.Rows.Count;
                newTransactionRow.Node = slaveId;
                newTransactionRow.TransactionTime = DateTime.Now;
                newTransactionRow.Error = errorMessage[(slaveId - 1)];


                newTransactionRow.TotalAmount = get32bitValue(68, slaveId);
                newTransactionRow.UnitRate = get16bitValue(64604, slaveId);
                newTransactionRow.TotalQuantity = get32bitValue(72, slaveId);

                // Add the row to the Region table
                this.cNGDatabaseDataSet.Transaction.Rows.Add(newTransactionRow);

                // Save the new row to the database
                this.transactionTableAdapter.Update(this.cNGDatabaseDataSet.Transaction);
                PowerError[(slaveId - 1)] = true;
                //Node++;
            }
            return status;
        }
        #endregion


        #region Button Actions

        public void GetStatus(byte slaveID)
         
        {
          //  NodeControl tempControl = nodeControls[slaveID];
            
            String StatusText = getStatusofNode(slaveID);

            if (errorMessage[(slaveID - 1)] != 0)
            {
                StatusText += (errorMessage.ToString());
                errorMessage[(slaveID - 1)] = 0;
            }
            nodeControls[(slaveID - 1)].setStatus(StatusText);
            
        }


        private void GetTotals(byte slaveID)
        {
            double total;
            total = get32bitValue (64596, slaveID ); //64596 // 60
            nodeControls[(slaveID - 1)].setFilledAMT(total);
           /* total = total / 100;
            FilledAMT.Text = String.Format("{0:0.00}", total);
            FilledAMT.TextAlign = HorizontalAlignment.Right;*/

            total = get16bitValue (64600, slaveID ); //64600 // 54
            nodeControls[(slaveID - 1)].setFilledQTY(total);
           /* total = total / 100;
            FilledQTY.Text = String.Format("{0:0.00}", total);
            FilledQTY.TextAlign = HorizontalAlignment.Right;*/

            total = get16bitValue(64604, slaveID);
            nodeControls[(slaveID - 1)].setUnitRate(total);
           /* total = total / 100;
            UnitRate.Text = String.Format("{0:0.00}", total);
            UnitRate.TextAlign = HorizontalAlignment.Center;*/

            total = get32bitValue(68, slaveID);
            nodeControls[(slaveID - 1)].setTotAMT(total);
           /* total = total / 100;
            TotAMT.Text = String.Format("{0:0,0.00}", total);
            TotAMT.TextAlign = HorizontalAlignment.Right;*/

            total = get32bitValue(72, slaveID);
            nodeControls[(slaveID - 1)].setTotQTY(total);
          /*  total = total / 100;
            TotQTY.Text = String.Format("{0:0,0.00}", total);
            TotQTY.TextAlign = HorizontalAlignment.Right;*/
        }
        #endregion


        #region Redundant TextBox Methods
        private void txtWriteRegister_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSlaveID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtStartAddr_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void statusTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cNGDatabaseDataSet.Transaction' table. You can move, or remove it, as needed.
          //  this.transactionTableAdapter.Fill(this.cNGDatabaseDataSet.Transaction);
            // TODO: This line of code loads data into the 'cNGDatabaseDataSet.Transaction' table. You can move, or remove it, as needed.
         //   this.transactionTableAdapter.Fill(this.cNGDatabaseDataSet.Transaction);
            // TODO: This line of code loads data into the 'cNGDatabaseDataSet.Transaction' table. You can move, or remove it, as needed.
         //   this.transactionTableAdapter.Fill(this.cNGDatabaseDataSet.Transaction);
            // TODO: This line of code loads data into the 'cNGDatabaseDataSet.Transaction' table. You can move, or remove it, as needed.
         //   this.transactionTableAdapter.Fill(this.cNGDatabaseDataSet.Transaction);

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void cOMPortSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetailNode detailNode = new DetailNode();
            detailNode.ShowDialog();
            //ComPort C0 = new ComPort();
            //C0.ShowDialog();
            //timer.Interval = 1000;
            //timer.Start();
        }

        private void lstPorts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void transactionBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.transactionBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.cNGDatabaseDataSet);

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {
            ;
        }

        private void groupBox7_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void DetailNode_Click(object sender, EventArgs e)
        {
            DetailNode detailNode = new DetailNode();
            detailNode.ShowDialog();
        }
    }

}