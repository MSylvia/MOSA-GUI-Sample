﻿using Mosa.External;
using Mosa.Kernel.x86;
using MOSA1.Drawing;
using MOSA1.Driver;
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
            if (PS2Keyboard.KeyAvailable())
            {
                KeyCode = PS2Keyboard.GetKeyPressed();

                switch (KeyCode)
                {
                    case PS2Keyboard.KeyCode.Delete:
                        if (ContinuableCommand == "")
                        {
                            if (Content.Length != 0)
                            {
                                Content = Content.Substring(0, Content.Length - 1);
                            }
                            if (Command.Length != 0)
                            {
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
                                Content += KeyCode.ToStr();
                                Command += KeyCode.ToStr();
                            }
                            else
                            {
                                Content += KeyCode.ToStr().ToLower();
                                Command += KeyCode.ToStr().ToLower();
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
                    Content += "Powered by Managed Operating System Alliance Project Ver:2.0.0.141. This demo made by nifanfa!\n";
                    break;
                case "SNAKE":
                    Content += "Launched \"Snake\"\n";
                    System.Windows.Add(new Snake() { X = 400, Y = 50, Width = 150, Height = 150 });
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
                case "PLAY":
                    SoundBlaster16.Play(SoundBlaster16.Info);
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
            MaxLine = Height / ASCII.FontHeight;

            if (W < 60)
            {
                W++;
            }
            else
            {
                W = 0;
            }

            //Content = PS2Keyboard.KData[1].ToString("X2");

            System.Graphics.DrawFilledRectangle(0x5B264D, X, Y, Width, Height);

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
                if (i < ((Width - ASCII.FontWidth) / ASCII.FontWidth) && v != '\n')
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
                System.Graphics.DrawACS16(0xFFFFFFFF, v, X + 0, Y + k * ASCII.FontHeight);
                k++;
            }
        }
    }
}
