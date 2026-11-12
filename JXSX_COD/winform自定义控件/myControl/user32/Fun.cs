using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace user32
{
    class Fun
    {

// 输入类型
public enum InputType : uint
{
    INPUT_MOUSE = 0,
    INPUT_KEYBOARD = 1,
    INPUT_HARDWARE = 2
}

// 鼠标事件标记，和mouse_event标记基本一样
[Flags]
public enum MouseEventFlags : uint
{
    MOVE = 0x0001,
    LEFTDOWN = 0x0002,
    LEFTUP = 0x0004,
    RIGHTDOWN = 0x0008,
    RIGHTUP = 0x0010,
    MIDDLEDOWN = 0x0020,
    MIDDLEUP = 0x0040,
    XDOWN = 0x0080,
    XUP = 0x0100,
    WHEEL = 0x0800,
    HWHEEL = 0x1000,
    ABSOLUTE = 0x8000
}

[StructLayout(LayoutKind.Sequential)]
public struct MOUSEINPUT
{
    public int dx;
    public int dy;
    public uint mouseData;
    public MouseEventFlags dwFlags;
    public uint time;
    public IntPtr dwExtraInfo;
}

[StructLayout(LayoutKind.Sequential)]
public struct KEYBDINPUT
{
    public ushort wVk;
    public ushort wScan;
    public uint dwFlags;
    public uint time;
    public IntPtr dwExtraInfo;
}

[StructLayout(LayoutKind.Explicit)]
public struct INPUT_UNION
{
    [FieldOffset(0)] public MOUSEINPUT mi;
    [FieldOffset(0)] public KEYBDINPUT ki;
}

[StructLayout(LayoutKind.Sequential)]
public struct INPUT
{
    public InputType type;
    public INPUT_UNION u;
}

    }
}
