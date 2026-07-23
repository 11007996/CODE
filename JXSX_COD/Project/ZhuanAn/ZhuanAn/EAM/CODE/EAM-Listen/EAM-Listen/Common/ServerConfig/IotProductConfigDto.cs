using EAM.Listen.Model;
using System.Collections.Generic;

namespace EAM.Listen.Common.Config
{
    /// <summary>
    /// 单个产品的信息
    /// </summary>
    public class IotProductConfigDto
    {
        public IotProduct Product { get; set; }
        public List<IotProductThingProperty> Propertys { get; set; }
        public List<IotProductThingEvent> Events { get; set; }
        public List<IotProductTopic> Topics { get; set; }
        public List<IotProductEventAction> Actions { get; set; }
        public IotProductParserScript ParserScript { get; set; }
        public Jint.Engine JsEngine { get; set; }
    }
}