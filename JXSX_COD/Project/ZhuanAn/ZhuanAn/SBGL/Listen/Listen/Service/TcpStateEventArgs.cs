using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Listen.Service
{
    public class TcpStateEventArgs : EventArgs
    {
        public EndPoint remoteEndPoint;
        public byte[] buffer = null;
        public EventType eventType;
    }

    public enum EventType
    {
        Send,
        Received
    }
}
