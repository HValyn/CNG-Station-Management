using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace Modbus_Poll_CS
{
    class modbus
    {
        public SerialPort sp;
        
        public string modbusStatus;
        private byte startFrame = 0x40;
        private byte stopFrame = 0x2A;
        private byte slaveId = 0x01;
        private byte data = 0x00;
        byte[] Message = new byte[8];

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

        #region Function 16 - Write Multiple Registers
        public bool SendFc16(byte address, ushort start, ushort registers, short[] values)
        {
            return true;
            //Ensure port is open:
            if (sp.IsOpen)
            {
                //Clear in/out buffers:
                sp.DiscardOutBuffer();
                sp.DiscardInBuffer();
                //Message is 1 addr + 1 fcn + 2 start + 2 reg + 1 count + 2 * reg vals + 2 CRC
                byte[] message = new byte[9 + 2 * registers];
                //Function 16 response is fixed at 8 bytes
                byte[] response = new byte[8];

                //Add bytecount to message:
                message[6] = (byte)(registers * 2);
                //Put write values into message prior to sending:
                for (int i = 0; i < registers; i++)
                {
                    message[7 + 2 * i] = (byte)(values[i] >> 8);
                    message[8 + 2 * i] = (byte)(values[i]);
                }
                //Build outgoing message:
              //  BuildMessage(address, (byte)16, start, registers, ref message);
                //BuildMessage(startFrame, 0x01, 51, data, slaveId, stopFrame, ref message);
                //Send Modbus message to Serial Port:
                try
                {
                    sp.Write(message, 0, message.Length);
                    Listen(ref response);
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in write event: " + err.Message;
                    return false;
                }
                //Evaluate message:
                if (CheckMessage(response))
                {
                    modbusStatus = "Write successful";
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
        public bool NetwayProtocol(byte type, ushort address, byte data, byte slaveId, ref byte[] values)
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
                   //Function 3 request is always 8 bytes:

                  //Function 3 response buffer:
             /*   BuildMessage(type, address, data, slaveId, ref response);    //Build outgoing modbus message:
                //Send modbus message to Serial Port:
                try
                {
                    sp.Write(response, 0, response.Length);
                    Listen(ref Message);
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in read event: " + err.Message;
                    return false;
                }*/

                

        }
        #endregion

    }
}
