using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace Modbus_Poll_CS
{
    class modbus
    {
        private SerialPort sp = new SerialPort();
        public string modbusStatus;
        private byte startFrame = 0x40;
        private byte stopFrame = 0x2A;


        #region Constructor / Deconstructor
        public modbus()
        {
        }
        ~modbus()
        {
        }
        #endregion

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
            //Redundant and Not Needed in custom implementation
           /* ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;*/

            for (int i = 0; i < (message.Length) - 2; i++)
            {
                byteCRCfull = (byte)(byteCRCfull ^ message[i]);
                //Redundant and Not Needed in custom implementation
              /*  for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }*/
            }
          /*  CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);*/
            CRC = byteCRCfull;
        }
        #endregion

        #region Build Message
        private void BuildMessage(byte type, ushort address, byte data, byte slaveId, ref byte[] message)
        {
            //Array to receive CRC bytes:
            byte byteCRC = 0xFF;

            message[0] = startFrame;
            message[1] = type;
            message[2] = (byte)(address >> 8);
            message[3] = (byte)address;
            message[4] = data;
            message[5] = slaveId;

            GetCRC(message, ref byteCRC);
            message[6] = byteCRC;
            message[7] = stopFrame;
        }
        #endregion

        #region Check Response
        private bool CheckResponse(byte[] response)
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
        private void GetResponse(ref byte[] response)
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
        public bool NetwayProtocol(byte type, ushort address, byte data, byte slaveId, ref byte[] values)
        {
            //Ensure port is open:
            if (sp.IsOpen)
            {
                //Clear in/out buffers:
                sp.DiscardOutBuffer();
                sp.DiscardInBuffer();
                //Function 3 request is always 8 bytes:
                byte[] message = new byte[8];
                //Function 3 response buffer:
                byte[] response = new byte[8];
                //Build outgoing modbus message:
              //  BuildMessage(address, (byte)3, start, registers, ref message);
                BuildMessage(type, address, data, slaveId, ref message);
                //Send modbus message to Serial Port:
                try
                {
                    sp.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in read event: " + err.Message;
                    return false;
                }
                //Evaluate message:
                if (CheckResponse(response))
                {
                    //Return requested register values:
                    for (int i = 0; i < response.Length ; i++)
                    {
                        values[i] = response[i];
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
        }
        #endregion
    }
}


