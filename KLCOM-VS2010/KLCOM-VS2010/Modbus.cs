using System;
using System.Collections.Generic;
using System.Text;
//using WFDLL;

namespace MODBUS
{
    public class Modbus
    {
        protected string _PortName;

        public string PortName
        {
            get { return _PortName; }
            set
            {
                _PortName = value;
                SP.PortName = _PortName;
            }
        }
        protected int _PortBaud;

        public int PortBaud
        {
            get { return _PortBaud; }
            set
            {
                _PortBaud = value;
                SP.PortBaud = _PortBaud;
            }
        }
        protected int _Timeout;

        public int Timeout
        {
            get { return _Timeout; }
            set
            {
                _Timeout = value;
                SP.Timeout = _Timeout;
            }
        }

        protected System.IO.Ports.Parity _parity;

        public System.IO.Ports.Parity Parity
        {
            get { return _parity; }
            set 
            {
                _parity = value;
                SP.Parity = _parity;
            }
        }

        protected MySerialPort SP = new MySerialPort();

        public Modbus()
        {
        }

        public Modbus(string PName, int PBaud, System.IO.Ports.Parity parity, int timeout)
        {
            _PortName = PName;
            _PortBaud = PBaud;
            _Timeout = timeout;
            _parity = parity;
            SP.PortName = _PortName;
            SP.PortBaud = _PortBaud;
            SP.Timeout = timeout;
            SP.Parity = parity;

        }

        ///// <summary>
        ///// 读取输入寄存器(F0x04)
        ///// </summary>
        ///// <param name="CommAddr">通讯地址</param>
        ///// <param name="StartAddr">起始地址</param>
        ///// <param name="RegNum">寄存器数</param>
        ///// <param name="data">数据</param>
        ///// <returns></returns>
        //public MODBUSRESULT ReadInputRegister(string CommAddr, UInt16 StartAddr, UInt16 RegCount, out string data)
        //{
        //    if ((CommAddr.Length % 2) != 0) { throw new Exception("CommAddr数据长度错误"); }
            
        //    StringBuilder cmd = new StringBuilder();
        //    cmd.Append(CommAddr);        //通讯地址
        //    cmd.Append("04");
        //    cmd.Append(StartAddr.ToString("X4"));       //起始地址
        //    cmd.Append(RegCount.ToString("X4"));        //寄存器数
        //    cmd.Append(MyMath.CRC_16(cmd.ToString()));
        //    return SP.SendAndReceive(cmd.ToString(), RegCount * 2 + 4 + CommAddr.Length / 2, out data);
        //}

        ///// <summary>
        ///// 读取保持寄存器(F0x03)
        ///// </summary>
        ///// <param name="CommAddr">通讯地址</param>
        ///// <param name="StartAddr">起始地址</param>
        ///// <param name="RegNum">寄存器数</param>
        ///// <param name="data">数据</param>
        ///// <returns></returns>
        //public MODBUSRESULT ReadHoldingRegister(string CommAddr, UInt16 StartAddr, UInt16 RegCount, out string data)
        //{
        //    if ((CommAddr.Length % 2) != 0) { throw new Exception("CommAddr数据长度错误"); }
            
        //    StringBuilder cmd = new StringBuilder();
        //    cmd.Append(CommAddr);        //通讯地址
        //    cmd.Append("03");
        //    cmd.Append(StartAddr.ToString("X4"));       //起始地址
        //    cmd.Append(RegCount.ToString("X4"));        //寄存器数
        //    cmd.Append(MyMath.CRC_16(cmd.ToString()));
        //    return SP.SendAndReceive(cmd.ToString(), RegCount * 2 + 4 + CommAddr.Length / 2, out data);
        //}

        /// <summary>
        /// 读取多个寄存器
        /// </summary>
        /// <param name="CommAddr">通讯地址</param>
        /// <param name="FunCode">功能码</param>
        /// <param name="StartAddr">起始地址</param>
        /// <param name="RegCount">寄存器数</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public MODBUSRESULT ReadMultiRegister(string CommAddr, byte FunCode,UInt16 StartAddr, UInt16 RegCount, out string data)
        {
            if ((CommAddr.Length % 2) != 0) { throw new Exception("CommAddr数据长度错误"); }

            StringBuilder cmd = new StringBuilder();
            cmd.Append(CommAddr);        //通讯地址
            cmd.Append(FunCode.ToString("X2"));
            cmd.Append(StartAddr.ToString("X4"));       //起始地址
            cmd.Append(RegCount.ToString("X4"));        //寄存器数
            cmd.Append(MyMath.CRC_16(cmd.ToString()));
            return SP.SendAndReceive(cmd.ToString(), RegCount * 2 + 4 + CommAddr.Length / 2, out data);
        }

        /// <summary>
        /// 写多个寄存器
        /// </summary>
        /// <param name="CommAddr"></param>
        /// <param name="StartAddr"></param>
        /// <param name="RegCount"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public MODBUSRESULT WriteMultiRegister(string CommAddr, byte FunCode, UInt16 StartAddr, string data)
        {
            if ((data.Length % 4) != 0) { throw new Exception("data数据长度错误"); }

            StringBuilder cmd = new StringBuilder();
            cmd.Append(CommAddr);        //通讯地址
            cmd.Append(FunCode.ToString("X2"));
            cmd.Append(StartAddr.ToString("X4"));       //起始地址
            int RegCount = data.Length / 4;
            cmd.Append(RegCount.ToString("X4"));        //寄存器数
            int bytecnt = RegCount * 2;
            cmd.Append(bytecnt.ToString("X2"));        //字节计数
            cmd.Append(data);
            cmd.Append(MyMath.CRC_16(cmd.ToString()));
            return SP.SendAndReceive(cmd.ToString(), 7 + CommAddr.Length / 2, out data);
        }


        public static string GetResultMsg(MODBUSRESULT ret)
        {
            string msg = "";
            switch (ret)
            { 
                case MODBUSRESULT.DataAddrInvalid:
                    msg = "数据地址无效";
                    break;
                case MODBUSRESULT.DataCheckFall:
                    msg = "数据校验失败";
                    break;
                case MODBUSRESULT.DataFormatError:
                    msg = "数据格式错误";
                    break;
                case MODBUSRESULT.DataValueInvalid:
                    msg = "数据值无效";
                    break;
                case MODBUSRESULT.OK:
                    msg = "正常";
                    break;
                case MODBUSRESULT.PortOpenFall:
                    msg = "打开端口失败";
                    break;
                case MODBUSRESULT.Timeout:
                    msg = "通讯超时";
                    break;
                case MODBUSRESULT.UnknownError:
                    msg = "未知错误";
                    break;
                case MODBUSRESULT.UnknownFunCode:
                    msg = "不支持的功能码";
                    break;
                default:
                    msg = "未知错误";
                    break;
            }
            return msg;
        }

        public static string GetRegValue(string data,int StartReg,int RegNum)
        {
            int offset = 6;
            int startpoint = offset + StartReg * 4;
            int len = RegNum * 4;
            return data.Substring(startpoint, len).ToUpper();
        }
    }
}
