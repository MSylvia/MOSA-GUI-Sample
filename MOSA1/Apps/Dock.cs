using Mosa.External.x86;
using Mosa.External.x86.Drawing.Fonts;
using Mosa.Kernel.x86;

namespace MOSA1.Apps
{
    class Dock : Window
    {
        public Dock()
        {
            Title = "Dock";

            Height = DockHeight;
        }

        string s;
        int devide = 10;
        int vol_btn_x = 0;
        int vol_btn_y = 0;

        public const int DockHeight = 30;

        public override void UIUpdate()
        {
            System.Graphics.DrawFilledRectangle(0x313131, X, Y, Width, Height);
            s = CMOS.Hour.ToString("X2") + ":" + CMOS.Minute.ToString("X2").PadLeft(2, '0');
            System.Graphics.DrawBitFontString("ArialCustomCharset16", 0xFFFFFFFF, s, Width - BitFont.Calculate("ArialCustomCharset16", s) - 16, Y + (Height / 2 - 8));
        }
    }
}
