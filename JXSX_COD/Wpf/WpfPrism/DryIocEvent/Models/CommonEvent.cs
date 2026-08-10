using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryIocEvent.Models
{
    //事件总线实现类
    public class CommonEvent:PubSubEvent<EventParamer>
    {

    }
    public class EventParamer
    {
        public string message { get; set; }
    }
}
 