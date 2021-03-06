using Mosa.External.x86.Drawing;
using Mosa.External.x86.Drawing.Fonts;
using Mosa.External.x86.Driver;
using MOSA1.Apps;
using System.Collections.Generic;

namespace MOSA1
{
    public class System
    {
        public static Graphics Graphics;
        int[] cursor = new int[]
        {
            1,0,0,0,0,0,0,0,0,0,0,0,
            1,1,0,0,0,0,0,0,0,0,0,0,
            1,2,1,0,0,0,0,0,0,0,0,0,
            1,2,2,1,0,0,0,0,0,0,0,0,
            1,2,2,2,1,0,0,0,0,0,0,0,
            1,2,2,2,2,1,0,0,0,0,0,0,
            1,2,2,2,2,2,1,0,0,0,0,0,
            1,2,2,2,2,2,2,1,0,0,0,0,
            1,2,2,2,2,2,2,2,1,0,0,0,
            1,2,2,2,2,2,2,2,2,1,0,0,
            1,2,2,2,2,2,2,2,2,2,1,0,
            1,2,2,2,2,2,2,2,2,2,2,1,
            1,2,2,2,2,2,2,1,1,1,1,1,
            1,2,2,2,1,2,2,1,0,0,0,0,
            1,2,2,1,0,1,2,2,1,0,0,0,
            1,2,1,0,0,1,2,2,1,0,0,0,
            1,1,0,0,0,0,1,2,2,1,0,0,
            0,0,0,0,0,0,1,2,2,1,0,0,
            0,0,0,0,0,0,0,1,2,2,1,0,
            0,0,0,0,0,0,0,1,2,2,1,0,
            0,0,0,0,0,0,0,0,1,1,0,0
        };
        public static string Log = "";
        public static List<Window> Windows;

        public static bool IsMovingWindow = false;
        public static int ActiveWindowIndex = 0;

        public System()
        {
            //For VMWareSVGAII
            //Graphics = new VMWareSVGAIIGraphics(Boot.ScreenWidth, Boot.ScreenHeight);
            //For VBE
            Graphics = new VBEGraphics();

            string CustomCharset = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
            byte[] 宋体CustomCharset16 = Mosa.External.x86.Convert.FromBase64String("AAAAAAAAAAAAAAQABAAEAAQABAAAAAAABAAAAAAAAAAAAAAAAAAFAAUACgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAKAAoAHwAKAAoAHwAKAAoAAAAAAAAAAAAAAAAAAAAEAA4AFQAUAAwABgAFABUADgAEAAAAAAAAAAAAAAAAAAAACQAVABYAFQAOgAaACoAJAAAAAAAAAAAAAAAAAAAAAAAEAAoACgANgBUAFQASgA0AAAAAAAAAAAAAAAAACAAIABAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABAAIAAgAEAAQABAAEAAQAAgACAAEAAAAAAAAAAAAAAAgABAAEAAIAAgACAAIAAgAEAAQACAAAAAAAAAAAAAAAAAAAAAAABAAVAA4ADgAVAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAACAAIAD4ACAAIAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIAAgAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAB+AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIAAAAAAAAAAAAAAAAAAAAAIABAAEAAgACAAQABAAIAAgAEAAAAAAAAAAAAAAAAAAAAA4AEQARABEAEQARABEADgAAAAAAAAAAAAAAAAAAAAAABAAMAAQABAAEAAQABAAOAAAAAAAAAAAAAAAAAAAAAAAOABEAEQACAAQACAAQAB8AAAAAAAAAAAAAAAAAAAAAAA4AEQABAAYAAQABABEADgAAAAAAAAAAAAAAAAAAAAAAAgAGAAYACgASAB8AAgAHAAAAAAAAAAAAAAAAAAAAAAAfABAAEAAeABEAAQARAA4AAAAAAAAAAAAAAAAAAAAAAAYACQAQABYAGQARABEADgAAAAAAAAAAAAAAAAAAAAAADwABAAIAAgAEAAQABAAEAAAAAAAAAAAAAAAAAAAAAAAOABEAEQAOABEAEQARAA4AAAAAAAAAAAAAAAAAAAAAAA4AEQARABMADQABABIADAAAAAAAAAAAAAAAAAAAAAAAAAAAAAQAAAAAAAAAAAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEAAAAAAAAAAQABAAAAAAAAAAAAAAAAAAAAAEAAgAEAAgACAAEAAIAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAB+AAAAfgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAIAAQAAgABAAEAAgAEAAgAAAAAAAAAAAAAAAAAAAAAAA4AEQARAAIABAAEAAAABAAAAAAAAAAAAAAAAAAAAAAABwAIgBKAFoAWgBcACIAHAAAAAAAAAAAAAAAAAAAAAAAEAAQABgAKAAoADwAJABmAAAAAAAAAAAAAAAAAAAAAAB4ACQAJAA4ACQAJAAkAHgAAAAAAAAAAAAAAAAAAAAAADwARABAAEAAQABAAEQAOAAAAAAAAAAAAAAAAAAAAAAAeAAkACQAJAAkACQAJAB4AAAAAAAAAAAAAAAAAAAAAAB8ACQAKAA4ACgAIAAkAHwAAAAAAAAAAAAAAAAAAAAAAHwAJAAoADgAKAAgACAAcAAAAAAAAAAAAAAAAAAAAAAAHAAkAEAAQABOAEQAJAAYAAAAAAAAAAAAAAAAAAAAAABmACQAJAA8ACQAJAAkAGYAAAAAAAAAAAAAAAAAAAAAAHwAEAAQABAAEAAQABAAfAAAAAAAAAAAAAAAAAAAAAAAPgAIAAgACAAIAAgACAAIAEgAcAAAAAAAAAAAAAAAAAB2ACQAKAAwACgAJAAkAHYAAAAAAAAAAAAAAAAAAAAAAHAAIAAgACAAIAAgACIAfgAAAAAAAAAAAAAAAAAAAAAA7gBsAGwAbABUAFQAVADWAAAAAAAAAAAAAAAAAAAAAABuACQANAA0ACwALAAkAHQAAAAAAAAAAAAAAAAAAAAAADgARABEAEQARABEAEQAOAAAAAAAAAAAAAAAAAAAAAAAeAAkACQAOAAgACAAIABwAAAAAAAAAAAAAAAAAAAAAAA4AEQARABEAEQAdABMADgADAAAAAAAAAAAAAAAAAAAAHgAJAAkADgAKAAkACQAdgAAAAAAAAAAAAAAAAAAAAAAPABEAEAAMAAIAAQARAB4AAAAAAAAAAAAAAAAAAAAAAB8AFQAEAAQABAAEAAQADgAAAAAAAAAAAAAAAAAAAAAAGYAJAAkACQAJAAkACQAGAAAAAAAAAAAAAAAAAAAAAAAZgAkACQAKAAoABgAEAAQAAAAAAAAAAAAAAAAAAAAAABUAFQAVABUADgAKAAoACgAAAAAAAAAAAAAAAAAAAAAAGwAKAAoABAAEAAoACgAbAAAAAAAAAAAAAAAAAAAAAAAbAAoACgAKAAQABAAEAA4AAAAAAAAAAAAAAAAAAAAAAB8AEgACAAQABAAIAAkAHwAAAAAAAAAAAAAAAAAHAAQABAAEAAQABAAEAAQABAAEAAcAAAAAAAAAAAAAAAAACAAIAAQABAAEAAIAAgACAAEAAQAAAAAAAAAAAAAADgACAAIAAgACAAIAAgACAAIAAgAOAAAAAAAAAAAAAAAEAAoAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAfgAAAAAAAAAAACAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABgAJAAcACQAHgAAAAAAAAAAAAAAAAAAAGAAIAAgACAAOAAkACQAJAA4AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAcACQAIAAkABgAAAAAAAAAAAAAAAAAAAAMAAQABAAEABwAJAAkACQAHgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGAAkADwAIAAcAAAAAAAAAAAAAAAAAAAADAASABAAEAA8ABAAEAAQADwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB4AJAAYACAAHAAiABwAAAAAAAAAAAAAAGAAIAAgACAAOAAkACQAJAB2AAAAAAAAAAAAAAAAAAAAEAAQAAAAAAAwABAAEAAQADgAAAAAAAAAAAAAAAAAAAAIAAgAAAAAABgACAAIAAgACAAIAHAAAAAAAAAAAAAAAGAAIAAgACAALAAoADAAKABkAAAAAAAAAAAAAAAAAAAAcAAQABAAEAAQABAAEAAQAHwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHgAVABUAFQAVAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAeAAkACQAJAB2AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAYACQAJAAkABgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAHgAJAAkACQAOAAgAHAAAAAAAAAAAAAAAAAAAAAAAAAAHAAkACQAJAAcAAQADgAAAAAAAAAAAAAAAAAAAAAAAABsADAAIAAgAHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADwAIAAYAAQAPAAAAAAAAAAAAAAAAAAAAAAAAAAQABAAPAAQABAAEAAcAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABsACQAJAAkAB4AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGwAKAAoABAAEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAVABUADgAKAAoAAAAAAAAAAAAAAAAAAAAAAAAAAAAAABsACgAEAAoAGwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGYAJAAkABgACAAQAGAAAAAAAAAAAAAAAAAAAAAAAAAAPAAIABAAEAA8AAAAAAAAAAAAAAAAAAwACAAIAAgACAAYAAgACAAIAAgADAAAAAAAAAAAAAAACAAIAAgACAAIAAgACAAIAAgACAAIAAgAAAAAAAAAAAAwABAAEAAQABAACAAQABAAEAAQADAAAAAAAAAAAAAAADQASAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=");
            BitFont.RegisterBitFont(new BitFontDescriptor("宋体CustomCharset16", CustomCharset, 宋体CustomCharset16, 16));
            
            Windows = new List<Window>();
            Windows.Add(new Terminal() { X = 50, Y = 80, Width = 500, Height = 400 });
            Windows.Add(new Dock() { X = 0, Y = Boot.ScreenHeight - 30, Width = Boot.ScreenWidth, NoBar = true });
        }

        public void Run()
        {
            Graphics.DrawFilledRectangle(0x2B001D, 0, 0, Boot.ScreenWidth, Boot.ScreenHeight);
            MosaLogo.Draw(Graphics, 10);

            /*
            Graphics.DrawACS16(0xFFFFFFFF, "Managed Operating System Alliance Project Ver:2.0.0.141", 10, 10);
            Graphics.DrawACS16(0xFFFFFFFF, "PS/2 Mouse IRQ12(0x2C) X:" + PS2Mouse.X + " " + "Mouse Y:" + PS2Mouse.Y + " " + "MouseStatus:" + PS2Mouse.Btn, 10, 26);
            Graphics.DrawACS16(0xFFFFFFFF, "CPU Vendor:" + CpuStructure.Vendor, 10, 42);
            Graphics.DrawACS16(0xFFFFFFFF, Log, 10, 58);
            Graphics.DrawACS16(0xFFFFFFFF, CMOS.Year.ToString("X2") + "/" + CMOS.Month.ToString("X2") + "/" + CMOS.Day.ToString("X2") + " " + CMOS.Hour.ToString("X2") + ":" + CMOS.Minute.ToString("X2") + ":" + CMOS.Second.ToString("X2"), 10, 72);
            Graphics.DrawACS16(0xFFFFFFFF, "FPS:" + FPSMeter.FPS, 10, 86);
            */

            for (int i = Windows.Count - 1; i >= 0; i--)
            {
                Windows[i].Update();
            }

            DrawCursor(Graphics, PS2Mouse.X, PS2Mouse.Y);

            Graphics.Update();

            FPSMeter.Update();
        }

        public void DrawCursor(Graphics graphics, int x, int y)
        {
            for (int h = 0; h < 21; h++)
            {
                for (int w = 0; w < 12; w++)
                {
                    if (cursor[h * 12 + w] == 1)
                    {
                        graphics.DrawPoint(0x0, w + x, h + y);
                    }
                    if (cursor[h * 12 + w] == 2)
                    {
                        graphics.DrawPoint(0xFFFFFFFF, w + x, h + y);
                    }
                }
            }
        }
    }
}
