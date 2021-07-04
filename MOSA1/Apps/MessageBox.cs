using MOSA1.Drawing;

namespace MOSA1.Apps
{
    class MessageBox : Window
    {
        public string Info = "";

        public MessageBox()
        {
            Title = "MessageBox";
        }

        public override void UIUpdate()
        {
            System.Graphics.DrawACS16(0x0, Info, X, Y);
        }
    }
}
