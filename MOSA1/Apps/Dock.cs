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

        public const int DockHeight = 30;

        public override void UIUpdate()
        {
            System.Graphics.DrawFilledRectangle(0x313131, X, Y, Width, Height);
            s = CMOS.Hour.ToString() + ":" + CMOS.Minute.ToString().PadLeft(2, '0');
            System.Graphics.DrawBitFontString("宋体CustomCharset16", 0xFFFFFFFF, s, Width - BitFont.Calculate("宋体CustomCharset16", s) - Height, Y + (Height / 2 - 8));
        }
    }
}
