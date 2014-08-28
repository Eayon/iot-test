using System;
using System.Collections.Generic;
using System.Text;

namespace MODBUS
{
    public enum MODBUSRESULT : int
    {
        OK,
        Timeout = -1,
        DataAddrInvalid = -2,
        DataValueInvalid = -3,
        UnknownFunCode = -4,
        DataCheckFall = -5,
        UnknownError = -6,
        DataFormatError = -7,
        PortOpenFall = -8
    }
}
