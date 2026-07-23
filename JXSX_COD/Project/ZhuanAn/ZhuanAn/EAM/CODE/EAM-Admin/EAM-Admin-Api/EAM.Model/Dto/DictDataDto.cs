namespace EAM.Model.Dto
{
    public class DictDataDto
    {
        public string DictValue { get; set; }
        public string DictLabel { get; set; }

        /// <summary>
        /// 扩展描述
        ///     用于对相同名称的数据扩展，以达到区分的目的。
        ///     对于多属性的数据最好用JSON字符串拼接，方便前端扩展显示。
        /// </summary>
        public string DictDesc {  get; set; }
    }
}