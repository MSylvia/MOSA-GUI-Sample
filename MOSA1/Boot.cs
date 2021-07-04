// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.External;
using Mosa.Kernel.x86;
using Mosa.Runtime;
using Mosa.Runtime.x86;
using MOSA1.Drawing;
using MOSA1.Driver;
using System;

namespace MOSA1
{
    public static class Boot
    {
        public static ConsoleSession Console;
        public static int ScreenWidth = 640;
        public static int ScreenHeight = 480;

        public static void Main()
        {
            Kernel.Setup();

            Console = ConsoleManager.Controller.Boot;
            Console.Clear();

            IDT.SetInterruptHandler(ProcessInterrupt);

            PCI.Setup();

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
                }
                catch (Exception E)
                {
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
