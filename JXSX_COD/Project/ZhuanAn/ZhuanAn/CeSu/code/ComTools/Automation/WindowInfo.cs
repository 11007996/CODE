using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComTools.Automation
{
    public class WindowInfo
    {
        public WindowInfo(IntPtr hWnd, string title)
        {
            HWnd = hWnd;
            Title = title;
        }
        public IntPtr HWnd { get; }
        public string Title { get; }
    }
}
