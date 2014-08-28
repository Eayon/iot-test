using System;
using System.Collections.Generic;
using System.Text;

namespace MODBUS
{
    public class MyMath
    {

        static public byte[] StrToByte(string data)
        {
            byte[] bt = new byte[data.Length / 2];
            for (int i = 0; i < data.Length / 2; i++)
            {
                bt[i] = Convert.ToByte(data.Substring(i * 2, 2), 16);
            }
            return bt;
        }

        /// <summary>
        /// 计算CRC-16
        /// </summary>
        /// <param name="data"></param>
        /// <returns>高位在前</returns>
        static public string CRC_16(string data)
        {
            if ((data.Length % 2) != 0) { throw new Exception("参数\"data\"长度不合法"); }
            byte[] tmp = StrToByte(data);

            /*
            1、预置16位寄存器为十六进制FFFF（即全为1）。称此寄存器为CRC寄存器； 
            2、把第一个8位数据与16位CRC寄存器的低位相异或，把结果放于CRC寄存器； 
            3、把寄存器的内容右移一位(朝低位)，用0填补最高位，检查最低位； 
            4、如果最低位为0：重复第3步(再次移位); 如果最低位为1：CRC寄存器与多项式A001（1010 0000 0000 0001）进行异或； 
            5、重复步骤3和4，直到右移8次，这样整个8位数据全部进行了处理； 
            6、重复步骤2到步骤5，进行下一个8位数据的处理； 
            7、最后得到的CRC寄存器即为CRC码。
            */
            UInt16 CRCREG = (UInt16)0xffff;
            for (int i = 0; i < tmp.Length; i++)
            {
                CRCREG = (UInt16)(CRCREG ^ (UInt16)tmp[i]);//<< 8;
                for (int j = 0; j < 8; j++)
                {
                    UInt16 CRCtmp = (UInt16)(CRCREG & (UInt16)0x0001);
                    CRCREG = (UInt16)(CRCREG >> (UInt16)1);
                    if (CRCtmp == (UInt16)1)
                    {
                        CRCREG = (UInt16)(CRCREG ^ (UInt16)0xA001);
                    }
                }
            }

            string strtmp = CRCREG.ToString("X4");

            return strtmp.Substring(2, 2) + strtmp.Substring(0, 2);
            //return strtmp;
        }
    }
}
