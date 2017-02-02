using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Timers;
using System.IO.Ports;
using Microsoft.Win32;
using System.Text.RegularExpressions;
namespace Modbus_Poll_CS
{
    public partial class Form1 : Form
    {
        SerialPort sp = new SerialPort();
        String portName;
        int selectedIndex;
       // sp.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
        //static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        bool isPolling = false;
        byte type = 0x01;
        Boolean fillingDone = false;
        // ModBus Variables
        String PID, VID;
        public string modbusStatus;
        private byte stopFrame = 0x2A;
        byte[] Message = new byte[8];
        //byte address = 0x51;
        private int rxcount = 0;
        byte slaveId = 1;
        #region GUI Delegate Declarations
        public delegate void GUIDelegate(string paramString);
        public delegate void GUIClear();
        public delegate void GUIStatus(string paramString);
        #endregion

        public Form1()
        {
            InitializeComponent();
            LoadListboxes();
           // MessageBox.Show(statusList.SelectedIndex.ToString());
           // timer.Tick += new EventHandler(TimerEventProcessor);
            sp.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            sp.ReceivedBytesThreshold = 8;
            //StartPoll();
            
        }
        private void FindPortName() 
        {
            if (PID == null || VID == null)
            {
                DialogPIDVID dialogPIDVID = new DialogPIDVID();
                dialogPIDVID.ShowDialog();

            }
            else 
            {
                startFindingPort();
            }
            
        }
        private void startFindingPort() 
        {
            List<string> names = ComPortNames(PID, VID);
            if (names.Count > 0)
            {
                foreach (String s in SerialPort.GetPortNames())
                {
                    if (names.Contains(s))
                        //Console.WriteLine("My Arduino port is " + s);
                        portName = s;
                    StartPoll(portName);
                    break;
                }
            }
            else
            {
                //Console.WriteLine("No COM ports found");
                MessageBox.Show("No Com Port or No Com Port with the given Text");
            }
        }
        List<string> ComPortNames(String VID, String PID)
        {
            String pattern = String.Format("^VID_{0}.PID_{1}", VID, PID);
            Regex _rx = new Regex(pattern, RegexOptions.IgnoreCase);
            List<string> comports = new List<string>();
            RegistryKey rk1 = Registry.LocalMachine;
            RegistryKey rk2 = rk1.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum");
            foreach (String s3 in rk2.GetSubKeyNames())
            {
                RegistryKey rk3 = rk2.OpenSubKey(s3);
                foreach (String s in rk3.GetSubKeyNames())
                {
                    if (_rx.Match(s).Success)
                    {
                        RegistryKey rk4 = rk3.OpenSubKey(s);
                        foreach (String s2 in rk4.GetSubKeyNames())
                        {
                            RegistryKey rk5 = rk4.OpenSubKey(s2);
                            RegistryKey rk6 = rk5.OpenSubKey("Device Parameters");
                            comports.Add((string)rk6.GetValue("PortName"));
                        }
                    }
                }
            }
            return comports;
        }
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
                try
                {
                    Listen(ref Message);
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in read event: " + err.Message;
                    lblStatus.Text = modbusStatus;
                }
                if (CheckMessage(Message))    //Evaluate message:
                {

                    for (int i = 0; i < Message.Length; i++)  //Return requested register values:
                    {

                        this.Message[i] = Message[i];

                    }
                    modbusStatus = "Read successful";
                    lblStatus.Text = modbusStatus;
                    if (Message[5] == slaveId) 
                    {
                        loop();
                        // WriteProtocol
                    }
                    
                }
                else
                {
                    modbusStatus = "CRC error";
                    lblStatus.Text = modbusStatus;
                }
                
           /* rxcount = rxcount + 1;
            if (rxcount < 8) return;
            rxcount = 0;*/
           // loop();
        }
        #region Open / Close Procedures
        public bool Open(string portName, int baudRate, int databits, Parity parity, StopBits stopBits)
        {
            //Ensure port isn't already opened:
            if (!sp.IsOpen)
            {
                //Assign desired settings to the serial port:
                sp.PortName = portName;
                sp.BaudRate = baudRate;
                sp.DataBits = databits;
                sp.Parity = parity;
                sp.StopBits = stopBits;
                //These timeouts are default and cannot be editted through the class at this point:
                sp.ReadTimeout = 60;
                sp.WriteTimeout = 60;

                try
                {
                    sp.Open();
                }
                catch (Exception err)
                {
                    modbusStatus = "Error opening " + portName + ": " + err.Message;
                    return false;
                }
                modbusStatus = portName + " opened successfully";
                return true;
            }
            else
            {
                modbusStatus = portName + " already opened";
                return false;
            }
        }
        public bool Close()
        {
            //Ensure port is opened before attempting to close:
            if (sp.IsOpen)
            {
                try
                {
                    sp.Close();
                }
                catch (Exception err)
                {
                    modbusStatus = "Error closing " + sp.PortName + ": " + err.Message;
                    return false;
                }
                modbusStatus = sp.PortName + " closed successfully";
                return true;
            }
            else
            {
                modbusStatus = sp.PortName + " is not open";
                return false;
            }
        }
        #endregion
        #region CRC Computation
        private void GetCRC(byte[] message, ref byte CRC)
        {
            //Function expects a modbus message of any length as well as a 2 byte CRC array in which to 
            //return the CRC values:
            byte byteCRCfull = 0x00;

            for (int i = 0; i < (message.Length) - 2; i++)
            {
                byteCRCfull = (byte)(byteCRCfull ^ message[i]);
                
            }
            /*  CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
              CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);*/
            CRC = byteCRCfull;
        }
        #endregion
        #region Build Message
        private void BuildMessage(byte type, ushort address, byte data, byte slaveId, ref byte[] outgoingMessage)
        {
            //Array to receive CRC bytes:
            byte byteCRC = 0xFF;

            outgoingMessage[0] = Message[0];
            outgoingMessage[1] = Message[1];
            outgoingMessage[2] = Message[2];
            outgoingMessage[3] = Message[3];
            outgoingMessage[4] = data;
            outgoingMessage[5] = 0x00;

            GetCRC(outgoingMessage, ref byteCRC);
            outgoingMessage[6] = byteCRC;
            outgoingMessage[7] = stopFrame;
        }
        #endregion
        #region Check Response
        private bool CheckMessage(byte[] response)
        {
            //Perform a basic CRC check:
            byte byteCRC = 0xFF;
            GetCRC(response, ref byteCRC);
            if (byteCRC == response[response.Length - 2])
                return true;
            else
                return false;
        }
        #endregion
        #region Get Response
        private void Listen(ref byte[] response)
        {
            //There is a bug in .Net 2.0 DataReceived Event that prevents people from using this
            //event as an interrupt to handle data (it doesn't fire all of the time).  Therefore
            //we have to use the ReadByte command for a fixed length as it's been shown to be reliable.
            for (int i = 0; i < response.Length; i++)
            {
                response[i] = (byte)(sp.ReadByte());
            }
        }
        #endregion
        #region Function 3 - Read Registers
        public bool WriteProtocol(byte type, ushort address, byte data, byte slaveId, ref byte[] values)
        {
            byte[] response = new byte[8];
            if (sp.IsOpen)  //Ensure port is open:
            {

                sp.DiscardOutBuffer();  //Clear in/out buffers:
                sp.DiscardInBuffer();


                BuildMessage(type, address, data, slaveId, ref response);    //Build outgoing modbus message:
                //Send modbus message to Serial Port:
                try
                {
                    sp.Write(response, 0, response.Length);
                    // Listen(ref Message);
                    modbusStatus = "Write Successful";
                    return true;
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in Write event: " + err.Message;
                    return false;
                }


            }
            else
            {
                modbusStatus = "Serial port not open";
                return false;
            }
        }
      /*  public bool NetwayProtocol(byte type, ushort address, byte data, byte slaveId, ref byte[] values)
        {


            if (sp.IsOpen)  //Ensure port is open:
            {

                sp.DiscardOutBuffer();  //Clear in/out buffers:
                sp.DiscardInBuffer();
                try
                {
                    Listen(ref Message);
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in read event: " + err.Message;
                    return false;
                }
                if (CheckMessage(Message))    //Evaluate message:
                {

                    for (int i = 0; i < Message.Length; i++)  //Return requested register values:
                    {

                        values[i] = Message[i];

                    }
                    modbusStatus = "Read successful";
                    return true;
                }
                else
                {
                    modbusStatus = "CRC error";
                    return false;
                }


            }
            else
            {
                modbusStatus = "Serial port not open";
                return false;
            }
        }*/
        #endregion

        #region Load Listboxes
        private void LoadListboxes()
        {
            //Three to load - ports, baudrates, datetype.  Also set default textbox values:
            //1) Available Ports:
           /* string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                lstPorts.Items.Add(port);
            }*/
            String[] status = new String[]{ "Error", "Ready", "Filling In progress", "Nozzle UP", "EndFill", "Payment" };
            
            foreach (string state in status)
            {
                statusList.Items.Add(state);
            }
           // lstPorts.SelectedIndex = 3;
            statusList.SelectedIndex = 0;
            //2) Baudrates:
           /* string[] baudrates = { "9600" };

            foreach (string baudrate in baudrates)
            {
                lstBaudrate.Items.Add(baudrate);
            }

            lstBaudrate.SelectedIndex = 0;*/

            //3) Datatype:
        }
        #endregion

        #region Start and Stop Procedures
        private void StartPoll(String selectedPort)
        {
            //pollCount = 0;
            
            //Open COM port using provided settings:
            if (Open(selectedPort, Convert.ToInt32("9600"),
                8, Parity.None, StopBits.One))
            {
                
                isPolling = true;

                //loop();
          }

            lblStatus.Text = modbusStatus;
        }
        private void loop() 
        {
           // byte[] values = new byte[8];
            //timer.AutoReset = true
            
            int unitrate = Convert.ToInt16(unitrateTextBox.Text);
            byte[] bytesunitrate = BitConverter.GetBytes(unitrate);

            int tAmount = Convert.ToInt32(totalAMT.Text);
            byte[] bytestotalAMT = BitConverter.GetBytes(tAmount);

            int tQTY = Convert.ToInt32(totalQTY.Text);
            byte[] bytestQTY = BitConverter.GetBytes(tQTY);

            int filledQTY = Convert.ToInt32(lastFilledQTY.Text);
            byte[] bytesfilledQTY = BitConverter.GetBytes(filledQTY);

            int filledAMNT = Convert.ToInt32(lastFilledAMT.Text);
            byte[] bytesfilledAMNT = BitConverter.GetBytes(filledAMNT);
            //Boolean success = NetwayProtocol(type, 0x00, 0x00, 0x00, ref values);
                byte[] nodeAmount = new byte[2];
                nodeAmount[1] = Message[2];
                nodeAmount[0] = Message[3];
                uint address = BitConverter.ToUInt16(nodeAmount, 0);
                switch (address)
                {
                    case 51:
                        // mb.WriteProtocol(type, 0x00, 0x00, setStatusofNode(), ref values);
                        WriteProtocol(type, 0x00, setStatusofNode(), 0x00, ref Message);
                        //lblStatus.Text = mb.modbusStatus;
                        break;
                    case 44:
                        //errorTextBox.Text
                        // BitConverter.GetBytes(BitConverter.ToDouble(errorTextBox.Text));
                        WriteProtocol(type, 0x00, Convert.ToByte(errorTextBox.Text), 0x00, ref Message);
                        //lblStatus.Text = mb.modbusStatus;
                        break;
                    case 64604:

                        WriteProtocol(type, 0x00, bytesunitrate[0], 0x00, ref Message);
                        //mb.WriteProtocol(type, 0x00, bytesunitrate[1], 0x00, ref values);
                        break;
                    case 64605:
                        WriteProtocol(type, 0x00, bytesunitrate[1], 0x00, ref Message);
                        break;
                    case 68:
                        WriteProtocol(type, 0x00, bytestotalAMT[0], 0x00, ref Message);
                        break;
                    case 69:
                        WriteProtocol(type, 0x00, bytestotalAMT[1], 0x00, ref Message);
                        break;
                    case 70:
                        WriteProtocol(type, 0x00, bytestotalAMT[2], 0x00, ref Message);
                        break;
                    case 71:
                        WriteProtocol(type, 0x00, bytestotalAMT[3], 0x00, ref Message);
                        break;
                    case 72:
                        WriteProtocol(type, 0x00, bytestQTY[0], 0x00, ref Message);
                        break;
                    case 73:
                        WriteProtocol(type, 0x00, bytestQTY[1], 0x00, ref Message);
                        break;
                    case 74:
                        WriteProtocol(type, 0x00, bytestQTY[2], 0x00, ref Message);
                        break;
                    case 75:
                        WriteProtocol(type, 0x00, bytestQTY[3], 0x00, ref Message);
                        break;
                    case 64596:
                        WriteProtocol(type, 0x00, bytesfilledAMNT[0], 0x00, ref Message);
                        break;
                    case 64597:
                        WriteProtocol(type, 0x00, bytesfilledAMNT[1], 0x00, ref Message);
                        break;
                    case 64598:
                        WriteProtocol(type, 0x00, bytesfilledAMNT[2], 0x00, ref Message);
                        break;
                    case 64599:
                        WriteProtocol(type, 0x00, bytesfilledAMNT[3], 0x00, ref Message);
                        break;
                    case 64600:
                        WriteProtocol(type, 0x00, bytesfilledQTY[0], 0x00, ref Message);
                        break;
                    case 64601:
                        WriteProtocol(type, 0x00, bytesfilledQTY[1], 0x00, ref Message);
                        break;
                    case 64602:
                        WriteProtocol(type, 0x00, bytesfilledQTY[2], 0x00, ref Message);
                        break;
                    case 64603:
                        WriteProtocol(type, 0x00, bytesfilledQTY[3], 0x00, ref Message);
                        break;
                }
            lblStatus.Text = modbusStatus;
          //  loop();
        }
        private void StopPoll()
        {
            //Stop timer and close COM port:
            isPolling = false;
            //timer.Stop();
            Close();

            //btnStart.Enabled = true;

            lblStatus.Text = modbusStatus;
        }
        #endregion



        #region Data Type Event Handler
        private void lstDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //restart the data poll if datatype is changed during the process:
            if (isPolling)
            {
                StopPoll();
              //  dataType = lstDataType.SelectedItem.ToString();
                StartPoll(portName);
            }

        }
        #endregion
        public byte setStatusofNode()
        {
          
            // uint total = 0;
           // int i = statusList.SelectedIndex;
            //int selectedIndex = statusList.SelectedIndex;
            //selectedIndex = selectedIndex + 1;
            byte returnValue = (byte) (selectedIndex+1);
            return returnValue;

        }

        private void statusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = statusList.SelectedIndex;
            if (fillingDone) 
            {
                int tQty = Convert.ToInt32(totalQTY.Text);
                tQty = tQty + (int)lastFilledQTY.Value;
                totalQTY.Text = tQty.ToString();
                int tAmnt = Convert.ToInt32(totalAMT.Text);
                tAmnt = tAmnt + Convert.ToInt32(lastFilledAMT.Text);
                totalAMT.Text = tAmnt.ToString();
            }
            if (statusList.SelectedItem.ToString() == "Filling In progress") 
            {
                lastFilledQTY.ReadOnly = false;
                unitrateTextBox.ReadOnly = false;
                errorTextBox.ReadOnly = false;
                fillingDone = true;

            }
            else if (statusList.SelectedItem.ToString() == "Error")
            {
                lastFilledQTY.ReadOnly = true;
                unitrateTextBox.ReadOnly = true;
                errorTextBox.ReadOnly = false;
                fillingDone = false;
            }
            else 
            {
                lastFilledQTY.ReadOnly = true;
                unitrateTextBox.ReadOnly = true;
                errorTextBox.ReadOnly = true;
                fillingDone = false;
            }
        }

        private void lastFilledQTY_ValueChanged(object sender, EventArgs e)
        {
            int Amnt = (int)lastFilledQTY.Value;
            Amnt = Amnt * Convert.ToInt32(unitrateTextBox.Text);
            lastFilledAMT.Text = Amnt.ToString();
        }

        private void slaveIdfield_ValueChanged(object sender, EventArgs e)
        {
            
                slaveId = (byte) slaveIdfield.Value;
            
        }

        

        
    }

}