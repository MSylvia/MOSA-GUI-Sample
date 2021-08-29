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
            System.Graphics.DrawBitFontString("宋体CustomCharset16", 0x0, Info, X, Y);
        }
    }
}
