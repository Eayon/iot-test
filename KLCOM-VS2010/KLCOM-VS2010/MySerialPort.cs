using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Timers;

namespace MODBUS
{
    public class MySerialPort
    {
        private string _PortName;

        public string PortName
        {
            get { return _PortName; }
            set { _PortName = value; }
        }
        private int _PortBaud;

        public int PortBaud
        {
            get { return _PortBaud; }
            set { _PortBaud = value; }
        }
        private int _Timeout;

        public int Timeout
        {
            get { return _Timeout; }
            set { _Timeout = value; }
        }

        private System.IO.Ports.Parity _parity;

        public System.IO.Ports.Parity Parity
        {
            get { return _parity; }
            set { _parity = value; }
        }

        private bool IsTimeout = false;
        private bool DataIsReceived = false;
        //private Timer MyTimer = new Timer();
        private string DataBuff = "";
        private int DataRecCount = 0;
        private int BytesToRec = 0;
        private byte DataHead = 0;
        //private MODBUSRESULT Result = MODBUSRESULT.OK;

        public MySerialPort()
        {

        }

        private MODBUSRESULT CheckData(string RecData,string SendData)
        {
            if (RecData.Substring(0,2) != SendData.Substring(0,2))
            {
                return MODBUSRESULT.DataCheckFall;
            }
            if (MyMath.CRC_16(RecData.Substring(0, RecData.Length - 4)) != RecData.Substring(RecData.Length - 4, 4))
            {
                return MODBUSRESULT.DataCheckFall;
            }
            if (RecData.Substring(2, 2) == "86")
            {
                return MODBUSRESULT.UnknownFunCode;
            }
            if (RecData.Substring(2, 2) == "83")
            {
                return MODBUSRESULT.DataAddrInvalid;
            }
            if (RecData.Substring(2, 2) == "90")
            {
                return MODBUSRESULT.DataValueInvalid;
            }
            return MODBUSRESULT.OK;
        }

        public MODBUSRESULT SendAndReceive(string SendData, int _BytesToRec,out string RetData)
        {
            RetData = "";
            IsTimeout = false;
            DataIsReceived = false;
            DataBuff = "";
            DataRecCount = 0;
            BytesToRec = _BytesToRec;

            SerialPort sp = new SerialPort(_PortName, _PortBaud, _parity, 8, StopBits.One);
            try
            {
                sp.Open();
            }
            catch
            {
                return MODBUSRESULT.PortOpenFall;
            }
            if (SendData.Length / 2 > 0)
            {
                byte[] bt = MyMath.StrToByte(SendData);
                DataHead = bt[0];
                sp.Write(bt, 0, bt.Length);
            }
            if (_BytesToRec > 0)
            {
                
                sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                Timer MyTimer = new Timer(_Timeout);
                MyTimer.Elapsed += new ElapsedEventHandler(MyTimer_Elapsed);
                MyTimer.Enabled = true;
                MyTimer.Start();
                while ((IsTimeout == false) && (DataIsReceived == false)) ;
                MyTimer.Stop();
                MyTimer.Elapsed -= new ElapsedEventHandler(MyTimer_Elapsed);
                MyTimer.Dispose();
                sp.Close();
            }
            else
            {
                sp.Close();
                return MODBUSRESULT.OK;
            }

            if (IsTimeout == true)
            {
                return MODBUSRESULT.Timeout;
            }
            //检查数据
            MODBUSRESULT ret = CheckData(DataBuff, SendData);
            if (ret == MODBUSRESULT.OK)
            {
                RetData = DataBuff;
                return MODBUSRESULT.OK;
            }
            else
            {
                return ret;
            }
        }

        void MyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            Timer mt = (Timer)sender;
            mt.Enabled = false;
            IsTimeout = true;
        }

        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            SerialPort sp = (SerialPort)sender;
            int BytesToRead = sp.BytesToRead;
            byte[] data = new byte[BytesToRead];
            sp.Read(data, 0, BytesToRead);
            for (int i = 0; i < data.Length; i++)
            {
                if (DataRecCount == 0)
                {
                    if (DataHead == data[i])
                    {
                        DataBuff += data[i].ToString("X2");
                        DataRecCount++;
                    }
                }
                else
                {
                    DataBuff += data[i].ToString("X2");
                    if (DataRecCount == 1)
                    {
                        if ((data[i] == 0x86) || (data[i] == 0x83) || (data[i] == 0x90))
                        {
                            BytesToRec = 5;
                        }
                    }
                    DataRecCount++;
                    if ((BytesToRec > 0) && (DataRecCount == BytesToRec))
                    {
                        DataIsReceived = true;
                        break;
                    }
                }
            }
        }
    }
}
