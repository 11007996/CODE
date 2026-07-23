using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;

namespace EAM.Service.Iot.IIotService
{
    /// <summary>
    /// 产品物模型属性service接口
    /// </summary>
    public interface IIotProductThingPropertyService : IBaseService<IotProductThingProperty>
    {
        PagedInfo<IotProductThingPropertyDto> GetList(IotProductThingPropertyQueryDto parm);

        IotProductThingProperty GetInfo(int PropertyId);

        IotProductThingProperty AddIotProductThingProperty(IotProductThingProperty parm);

        int UpdateIotProductThingProperty(IotProductThingProperty parm);

        IotProductThingPropertyExtend GetExtendInfo(int PropertyId);

        IotProductThingPropertyExtend AddIotProductThingPropertyExtend(IotProductThingPropertyExtend parm);

        int UpdateIotProductThingPropertyExtend(IotProductThingPropertyExtend parm);

        int DeleteIotProductThingPropertyExtend(int PropertyId);
    }
}