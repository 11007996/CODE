using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;

namespace EAM.Service.Iot.IIotService
{
    /// <summary>
    /// 产品主题类表service接口
    /// </summary>
    public interface IIotProductTopicService : IBaseService<IotProductTopic>
    {
        PagedInfo<IotProductTopicDto> GetList(IotProductTopicQueryDto parm);

        IotProductTopic GetInfo(int TopicId);

        IotProductTopic AddIotProductTopic(IotProductTopic parm);

        int UpdateIotProductTopic(IotProductTopic parm);
    }
}