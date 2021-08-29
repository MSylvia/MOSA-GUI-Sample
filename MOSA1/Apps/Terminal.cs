using Mosa.External.x86.Driver;
using Mosa.Kernel.x86;
using Mosa.Runtime.x86;

namespace MOSA1.Apps
{
    class Terminal : Window
    {
        //public string Content = "MOSA is an open source software project that natively executes .NET applications within a virtual hypervisor or on bare metal hardware!";
        public string Content = "";
        public string aContent = "";

        public string Command = "";

        public Terminal()
        {
            Title = "Terminal";
        }

        PS2Keyboard.KeyCode KeyCode;

        public override void InputUpdate()
        {
            if (PS2Keyboard.KeyAvailable)
            {
                KeyCode = PS2Keyboard.GetKeyPressed();

                switch (KeyCode)
                {
                    case PS2Keyboard.KeyCode.Delete:
                        if (ContinuableCommand == "")
                        {
                            Command = "";

                            CursorX = 0;
                            virtualGraphics.DrawFilledRectangle(0x5B264D, CursorX, CursorY, Width, 16);
                            New();
                        }
                        break;
                    case PS2Keyboard.KeyCode.Enter:
                        if (ContinuableCommand == "")
                        {
                            WriteLine();
                            ProcessCommand();
                            Command = "";

                            New();
                        }
                        break;
                    case PS2Keyboard.KeyCode.ESC:
                        ContinuableCommand = "";
                        WriteLine();
                        New();
                        break;
                    default:
                        if (ContinuableCommand == "")
                        {
                            if (PS2Keyboard.IsCapsLock)
                            {
                                Write(KeyCode.KeyCodeToString());
                                Command += KeyCode.KeyCodeToString();
                            }
                            else
                            {
                                Write(KeyCode.KeyCodeToString().ToLower());
                                Command += KeyCode.KeyCodeToString().ToLower();
                            }
                        }
                        break;
                }
            }
        }


        string ContinuableCommand = "";

        public int CursorX = 0;
        public int CursorY = 0;

        public void Write(string s)
        {
            if (CursorY + 16 > Height)
            {
                CursorY -= 16;
                ASM.MEMCPY(virtualGraphics.VideoMemoryCacheAddr, (uint)(virtualGraphics.VideoMemoryCacheAddr + (16 * Width * 4)), (uint)(Width * (Height - 16) * 4));
                virtualGraphics.DrawFilledRectangle(0x5B264D, 0, Height - 16, Width, 16);
            }

            if (virtualGraphics == null) return;
            CursorX += virtualGraphics.DrawBitFontString("宋体CustomCharset16", 0xFFFFFFFF, s, CursorX, CursorY);
            if (CursorX > Width)
            {
                CursorX = 0;
                CursorY += 16;
            }
        }

        public void WriteLine(string s)
        {
            Write(s);
            CursorX = 0;
            CursorY += 16;
        }

        public void WriteLine()
        {
            CursorX = 0;
            CursorY += 16;
        }

        private void ProcessCommand()
        {
            switch (Command.ToUpper())
            {
                case "ABOUT":
                    WriteLine(@"  __  __                 ");
                    WriteLine(@" |  \/  |                ");
                    WriteLine(@" | \  | | ___  ___  ___ ");
                    WriteLine(@" | |\/| |/ _ \/ __|/ _` |");
                    WriteLine(@" | |  | | (_) \__ \ (_| |");
                    WriteLine(@" |_|  |_|\___/|___/\__,_|");
                    WriteLine("Based on MOSA-Core. This Demo Was Made By nifanfa!");
                    break;
                case "SNAKE":
                    WriteLine("Launched \"Snake\"");
                    System.Windows.Add(new Snake() { X = 600, Y = 100, Width = 150, Height = 150 });
                    break;
                case "HELP":
                    WriteLine("About (Get About Info)");
                    WriteLine("Snake (Launch Snake Game)");
                    WriteLine("Clear (Clear Console)");
                    WriteLine("FPS (Show FPS)");
                    WriteLine("Get Free Memory (Get Free Memory)");
                    break;
                case "CLEAR":
                    CursorX = 0;
                    CursorY = 0;
                    virtualGraphics.DrawFilledRectangle(0x5B264D, 0, 0, Width, Height);
                    break;
                case "FPS":
                    ContinuableCommand = "FPS";
                    break;
                case "GET FREE MEMORY":
                    WriteLine($"{(PageFrameAllocator.TotalPages - PageFrameAllocator.TotalPagesInUse) * PageFrameAllocator.PageSize / (1024 * 1024)}MB");
                    break;
                default:
                    WriteLine("Bad Command");
                    break;
            }
        }

        public void New()
        {
            Write(">");
        }

        public override void UIUpdate()
        {
            if (virtualGraphics == null)
            {
                virtualGraphics = new Mosa.External.x86.Drawing.VirtualGraphics(this.Width, this.Height);
                virtualGraphics.DrawFilledRectangle(0x5B264D, 0, 0, Width, Height);
                New();
            }

            switch (ContinuableCommand)
            {
                case "FPS":
                    CursorX = 0;
                    virtualGraphics.DrawFilledRectangle(0x5B264D, CursorX, CursorY, Width, 16);
                    Write("FPS:" + FPSMeter.FPS + " Press ESC To Continue");
                    break;
            }

            System.Graphics.DrawImageASM(virtualGraphics.bitmap, X, Y);
        }
    }
}
