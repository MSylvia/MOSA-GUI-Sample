// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.Kernel.x86;
using Mosa.Runtime.x86;
using MOSA1.Drawing;
using MOSA1.Driver;
using System;

namespace MOSA1
{
    public static class Boot
    {
        //For VMWareSVGAII
        //public static int ScreenWidth = 800;
        //public static int ScreenHeight = 600;

        //For VBE
        //If we use vbe so we can run this demo on real hardware
        public static int ScreenWidth
        {
            get
            {
                return VBE.ScreenWidth;
            }
        }
        public static int ScreenHeight
        {
            get
            {
                return VBE.ScreenHeight;
            }
        }

        public static void Main()
        {
            Kernel.Setup();

            Console.Clear();

            IDT.SetInterruptHandler(ProcessInterrupt);

            PS2Keyboard.Initialize();
            PS2Mouse.Initialize(ScreenWidth, ScreenHeight);

            ASCII.Setup();

            SoundBlaster16.Initialize();

            System system = new System();

            for (; ; )
            {
                try
                {
                    system.Run();
                    Native.Hlt();
                }
                catch (Exception E)
                {
                    System.Graphics.DrawACS16(0xFFFFFFFF, E.Message, 10, 10);
                    System.Graphics.Update();
                    System.Graphics.Disable();
                    Console.Write(E.Message);
                    for (; ; )
                    {
                        Native.Hlt();
                    }
                }
            }
        }

        public static void ProcessInterrupt(uint interrupt, uint errorCode)
        {
            switch (interrupt)
            {
                case 0x2C:
                    PS2Mouse.OnInterrupt();
                    break;
                case 0x21:
                    PS2Keyboard.OnInterrupt();
                    break;
            }
        }
    }
}
