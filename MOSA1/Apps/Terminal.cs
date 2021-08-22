using Mosa.External.x86.Driver;
using Mosa.Kernel.x86;
using MOSA1.Drawing;
using System.Collections.Generic;

namespace MOSA1.Apps
{
    class Terminal : Window
    {
        //public string Content = "MOSA is an open source software project that natively executes .NET applications within a virtual hypervisor or on bare metal hardware!";
        public string Content = "";
        public string aContent = "";

        public string Command = "";

        List<string> s;
        int MaxLine = 0;

        public Terminal()
        {
            Title = "Terminal";

            s = new List<string>();

            New();
        }

        int W = 0;
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
                            if (Command.Length != 0)
                            {
                                Content = Content.Substring(0, Content.Length - 1);
                                Command = Command.Substring(0, Command.Length - 1);
                            }
                        }
                        break;
                    case PS2Keyboard.KeyCode.Enter:
                        if (ContinuableCommand == "")
                        {
                            Content += "\n";
                            ProcessCommand();
                            Command = "";

                            New();
                        }
                        break;
                    case PS2Keyboard.KeyCode.ESC:
                        ContinuableCommand = "";
                        break;
                    default:
                        if (ContinuableCommand == "")
                        {
                            if (PS2Keyboard.IsCapsLock)
                            {
                                Content += KeyCode.KeyCodeToString();
                                Command += KeyCode.KeyCodeToString();
                            }
                            else
                            {
                                Content += KeyCode.KeyCodeToString().ToLower();
                                Command += KeyCode.KeyCodeToString().ToLower();
                            }
                        }
                        break;
                }
            }
        }


        string ContinuableCommand = "";
        string ContinuableCommandOutput = "";

        private void ProcessCommand()
        {
            switch (Command.ToUpper())
            {
                case "ABOUT":
                    Content += "Based on MOSA-Core. This Demo Was Made By nifanfa!\n";
                    break;
                case "SNAKE":
                    Content += "Launched \"Snake\"\n";
                    System.Windows.Add(new Snake() { X = 600, Y = 100, Width = 150, Height = 150 });
                    break;
                /*
            case "List PCI Devices":
                ushort Last = 0;
                foreach (var v in PCI.Devices)
                {
                    if(Last != v.VendorID)
                    {
                        Content += "VendorID:0x" + v.VendorID.ToString("X2") + "\n";
                    }
                    Last = v.VendorID;
                }
                break;
                */
                case "HELP":
                    Content += "About (Get About Info)" + "\n";
                    Content += "Snake (Launch Snake Game)" + "\n";
                    Content += "Clear (Clear Console)" + "\n";
                    Content += "FPS (Show FPS)" + "\n";
                    Content += "Get Free Memory (Get Free Memory)" + "\n";
                    break;
                case "CLEAR":
                    Content = "";
                    break;
                case "FPS":
                    ContinuableCommand = "FPS";
                    break;
                case "GET FREE MEMORY":
                    Content += (PageFrameAllocator.TotalPages - PageFrameAllocator.TotalPagesInUse) * PageFrameAllocator.PageSize / (1024 * 1024) + "MB\n";
                    break;
                default:
                    Content += "Bad Command\n";
                    break;
            }
        }

        public void New()
        {
            Content += ">";
        }

        public override void UIUpdate()
        {
            System.Graphics.DrawFilledRectangle(0x5B264D, X, Y, Width, Height);

            MaxLine = Height / 16;

            if (W < 60)
            {
                W++;
            }
            else
            {
                W = 0;
            }

            s.Clear();

            switch (ContinuableCommand)
            {
                case "FPS":
                    ContinuableCommandOutput = "FPS:" + FPSMeter.FPS + " Press ESC To Continue";
                    break;
                default:
                    ContinuableCommandOutput = "";
                    break;
            }


            if (W < 30)
            {
                aContent = Content + ContinuableCommandOutput + "_";
            }
            else
            {
                aContent = Content + ContinuableCommandOutput;
            }

            string l = "";
            int i = 0;
            foreach (var v in aContent)
            {
                if (v != '\n')
                {
                    i++;
                    l += v;
                }
                else
                {
                    s.Add(l);
                    i = 0;
                    l = "";

                    if (v != '\n')
                    {
                        l += v;
                    }
                }
            }
            s.Add(l);


            if (s.Count > MaxLine)
            {
                while (s.Count != MaxLine)
                {
                    s.RemoveAt(0);
                }
            }

            int k = 0;
            foreach (var v in s)
            {
                System.Graphics.DrawBitFontString("ArialCustomCharset16", 0xFFFFFFFF, v, X + 0, Y + k * ASCII.FontHeight);
                k++;
            }
        }
    }
}
