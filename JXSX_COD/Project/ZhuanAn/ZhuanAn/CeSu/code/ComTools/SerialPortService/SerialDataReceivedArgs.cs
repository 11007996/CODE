using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComTools.SerialPortService
{
    public class SerialDataReceivedArgs
    {
        public SerialPort SerialPort { get; set; }
        public byte[] Data { get; set; }
    }
}
