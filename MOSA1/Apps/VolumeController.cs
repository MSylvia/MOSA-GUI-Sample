using MOSA1.Drawing;
using MOSA1.Driver;
using System;

namespace MOSA1.Apps
{
    class VolumeController : Window
    {
        public VolumeController()
        {
            Title = "VolumeController";
            HasAnimation = true;
        }

        int Precent = 100;

        int w = 0;

        public override void InputUpdate()
        {
            w++;

            if (PS2Mouse.X > this.X && PS2Mouse.X < this.X + this.Width && PS2Mouse.Y > this.Y&&PS2Mouse.Y< this.Y + Height) 
            {
                if(PS2Mouse.Btn == "Left") 
                {
                    Precent = (int)((int)100-(((PS2Mouse.Y - (double)this.Y) / Height) * 100));

                    //0x0 -> 0xF
                    SoundBlaster16.SetSoundVolume(Math.Clamp((byte)(Precent / 6), (byte)0xC, (byte)0xF));

                    if (w > 10)
                    {
                        SoundBlaster16.Play(SoundBlaster16.Info);
                        w = 0;
                    }
                }
            }
        }

        bool ShowAnimFinished = false;
        int T = 0;

        public override void UIUpdate()
        {
            //Before UIUpdate Will ResetLimit

            if (!ShowAnimFinished) 
            {
                T += 10;
                System.Graphics.SetLimit(X, Y + Height - T, Width + 1, T);
                if(T + 10 > Height) 
                {
                    ShowAnimFinished = true;
                }
            }

            System.Graphics.DrawFilledRectangle(0xFFFFFF, X, Y, Width, Height);
            System.Graphics.DrawFilledRectangle(0x313131, X, Y + Height - ((Height * Precent) / 100), Width, (Height*Precent)/100);

            //After UIUpdate Will ResetLimit
        }

        public override void SetVisible(bool Visible)
        {
            ShowAnimFinished = false;
            T = 0;
            base.SetVisible(Visible);
        }
    }
}
