public void AutoResizeColumnWidth(ListView lv)
{
            // lv = new ListView();
    int count = lv.Columns.Count;
    int MaxWidth = 0;
    Graphics graphics = lv.CreateGraphics();
    Font font = lv.Font;
    ListView.ListViewItemCollection items = lv.Items;

    string str;
    int width;

    lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

    for (int i = 0; i < count; i++)
    {
        str = lv.Columns[i].Text;
        MaxWidth = lv.Columns[i].Width;

        foreach (ListViewItem item in items)
        {
            str = item.SubItems[i].Text;
            width = (int)graphics.MeasureString(str, font).Width;
            if (width > MaxWidth)
            {
                MaxWidth = width;
            }
        }
                //if (i == 0)
                //{
                //    lv.Columns[i].Width = lv.SmallImageList.ImageSize.Width + MaxWidth;
                //}
        lv.Columns[i].Width = MaxWidth;
    }
}