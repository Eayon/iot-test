using MODBUS;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KLCOM_N1000
{
   
    [Guid("154BD6A6-5AB8-4d7d-A343-0A68AB79470B")]
    public interface KLCOM_Interface
    {
        [DispId(1)]
        string GetData(string dataname);
    }

    [Guid("D11FEA37-AC57-4d39-9522-E49C4F9826BB"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface MyCom_Events
    {
    }

    [Guid("2E3C7BAD-1051-4622-9C4C-215182C6BF58"), ClassInterface(ClassInterfaceType.None), ComSourceInterfaces(typeof(MyCom_Events))]
    public class DataTrans : KLCOM_Interface
    {
        public string GetData(string dataname)
        {
            Modbus objMB = new Modbus("COM1", 9600, Parity.Even, 1000);

            string recData = "";
            byte readFunCod = 0x04;
            UInt16 readAdd = 0x0000;
            UInt16 readLength = 0x08;
            string data ="";

            MODBUSRESULT ret = objMB.ReadMultiRegister("01", readFunCod, readAdd, readLength, out recData);
            string s = "send:  " + "addr = " + "01" + ";funCode =" + readFunCod.ToString() + ";readAddr =" + readAdd.ToString() + ";readLength =" + readLength.ToString();
            if (ret == MODBUSRESULT.OK)
            {
                //data += "al";
                
                //data  += recData;
                if ((readFunCod == 0x04) && (recData.Substring(2, 2) == "04"))
                {
                    switch (dataname)
                    {
                        case "trsd":
                            data = getRealValue(recData, 0, 0, 100).ToString(); //土壤湿度
                            break;
                        case "trwd":
                            data = getRealValue(recData, 1, -30, 70).ToString(); //土壤温度
                            break;
                        case "eyht":
                            data = getRealValue(recData, 2, 0, 2000).ToString(); //二氧化碳
                            break;
                        case "zd":
                            data = getRealValue(recData, 3, 0, 200000).ToString(); //照度
                            break;
                        case "kqsd":
                            data = getRealValue(recData, 4, 0, 100).ToString(); //空气湿度
                            break;
                        case "kqwd":
                            data = getRealValue(recData, 5, -20, 60).ToString(); //空气温度
                            break;
                    }
                //string turangshidu = getRealValue(recData, 0, 0, 100).ToString(); //土壤湿度
                //data = getRealValue(recData, 1, -30, 70).ToString(); //土壤温度
                //string eryanghuatan = getRealValue(recData, 2, 0, 2000).ToString(); //二氧化碳
                //string zhaodu = getRealValue(recData, 3, 0, 200000).ToString(); //照度
                //string kongqishidu = getRealValue(recData, 4, 0, 100).ToString(); //空气湿度
                //string kongqiwendu = getRealValue(recData, 5, -20, 60).ToString(); //空气温度
                }
            }

            return data;
        }

        private double getRealValue(string recData, int index, int min, int max)
        {
            double mAvalue = (int.Parse(Modbus.GetRegValue(recData, index, 1), System.Globalization.NumberStyles.HexNumber) * 20.0 / 10000) * 100;

            double realValue = ((mAvalue - 400) * (Math.Abs(max) + Math.Abs(min)) / 1600) + min; //  ((mAvalue - 400) * (max + min) / 1600) + min;
            //realValue = ((1342.8-400) * 80/1600 - 20);
            return realValue;
        }
    }
}
