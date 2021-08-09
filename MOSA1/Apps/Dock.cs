﻿using Mosa.Kernel.x86;
using MOSA1.Drawing;
using MOSA1.Driver;
using System.Drawing;

namespace MOSA1.Apps
{
    class Dock : Window
    {
        Image SoundImage;

        public Dock()
        {
            Title = "Dock";

            int[] SoundImageRaw = new int[] { -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -1, -1, -16777216, -16777216, -16777216, -1, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -1, -1, -1, -16777216, -16777216, -1, -16777216, -1, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -1, -1, -1, -1, -16777216, -1, -16777216, -1, -16777216, -1, -16777216, -16777216, -16777216, -16777216, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -16777216, -16777216, -1, -16777216, -1, -16777216, -1, -16777216, -16777216, -16777216, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -16777216, -16777216, -16777216, -1, -16777216, -1, -16777216, -1, -16777216, -16777216, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -16777216, -16777216, -16777216, -1, -16777216, -1, -16777216, -1, -16777216, -16777216, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -16777216, -16777216, -16777216, -1, -16777216, -1, -16777216, -1, -16777216, -16777216, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -16777216, -16777216, -16777216, -1, -16777216, -1, -16777216, -1, -16777216, -16777216, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -16777216, -16777216, -16777216, -1, -16777216, -1, -16777216, -1, -16777216, -16777216, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -16777216, -16777216, -16777216, -1, -16777216, -1, -16777216, -1, -16777216, -16777216, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -16777216, -16777216, -1, -16777216, -1, -16777216, -1, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -1, -1, -1, -1, -16777216, -1, -16777216, -1, -16777216, -1, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -1, -1, -1, -16777216, -16777216, -1, -16777216, -1, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -1, -1, -16777216, -16777216, -16777216, -1, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, -16777216, };
            SoundImage = new Image() { Width = 20, Height = 20, RawData = new Mosa.External.x86.MemoryBlock(SoundImageRaw) };

            Height = DockHeight;
        }

        string s;
        int devide = 10;
        int vol_btn_x = 0;
        int vol_btn_y = 0;
        VolumeController VolumeController;
        
        public const int DockHeight = 30;

        public override void UIUpdate()
        {
            System.Graphics.DrawFilledRectangle(0x313131, X, Y, Width, Height);
            s = CMOS.Hour.ToString("X2") + ":" + CMOS.Minute.ToString("X2");
            System.Graphics.DrawACS16(0xFFFFFFFF, s, Width - (s.Length * ASCII.FontWidth) - ASCII.FontWidth, Y + (Height / 2 - ASCII.FontHeight / 2));

            vol_btn_x = Width - (s.Length * ASCII.FontWidth) - ASCII.FontWidth - SoundImage.Width - devide;
            vol_btn_y = Y + (Height / 2 - SoundImage.Height / 2);
            System.Graphics.DrawImage(SoundImage, vol_btn_x, vol_btn_y, -16777216);
            InitVolumeController();
        }

        int VolConT = 0;
        public override void InputUpdate()
        {
            VolConT++;

            if (PS2Mouse.X > vol_btn_x && PS2Mouse.Y < vol_btn_x + SoundImage.Width && PS2Mouse.Y > vol_btn_y && PS2Mouse.Y < vol_btn_y + SoundImage.Height)
            {
                if (PS2Mouse.Btn == "Left" && VolConT > 15)
                {
                    VolumeController.SetVisible(!VolumeController.Visible);
                    VolConT = 0;
                }
            }
        }

        bool Init = false;
        void InitVolumeController()
        {
            if (!Init)
            {
                VolumeController = new VolumeController() { Width = 20, Height = 100, X = vol_btn_x, Y = vol_btn_y - 120, NoBar = true, Visible = false };
                System.Windows.Add(VolumeController);
                Init = true;
            }
        }
    }
}
