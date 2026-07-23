using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Iot;

namespace EAM.Service.Iot.IIotService
{
    /// <summary>
    /// 产品表service接口
    /// </summary>
    public interface IIotProductService : IBaseService<IotProduct>
    {
        PagedInfo<IotProductDto> GetList(IotProductQueryDto parm);

        IotProduct GetInfo(int ProductId);

        IotProduct AddIotProduct(IotProduct parm);

        int UpdateIotProduct(IotProduct parm);

        int ReleaseIotProduct(int ProductId);

        PagedInfo<DictDataDto> GetDict(IotProductQueryDto parm);
    }
}