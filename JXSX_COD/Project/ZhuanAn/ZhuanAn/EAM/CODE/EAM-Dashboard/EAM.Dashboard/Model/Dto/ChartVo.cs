namespace EAM.Dashboard.Model.Dto
{
    public class ChartBaseVo<T1, T2>
    {
        public T1 Name { get; set; }
        public T2 Value { get; set; }
    }

    public class ChartXYData<TX, TY, TD>
    {
        public List<TX> XData { get; set; }

        public List<TY> YData { get; set; }

        public List<TD> Data { get; set; }
    }
}