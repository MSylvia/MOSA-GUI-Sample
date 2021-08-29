using Mosa.External.x86.Drawing;
using Mosa.External.x86.Driver;
using MOSA1.Apps;
using System;

namespace MOSA1
{
    public class Window
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        bool Move = false;
        private int OffsetX;
        private int OffsetY;

        public string Title = "Form";
        public static int BarHeight = 20;

        public bool Visible = true;
        public bool NoBar = false;

        public int X_Desktop;
        public int Y_Desktop;

        public static VirtualGraphics virtualGraphics;

        public bool Actived
        {
            get
            {
                return System.ActiveWindowIndex == System.Windows.IndexOf(this);
            }
        }

        public void Active()
        {
            System.ActiveWindowIndex = System.Windows.IndexOf(this);
        }

        public void Update()
        {
            if (!Visible)
            {
                return;
            }

            if (!NoBar)
            {
                if (PS2Mouse.Btn == "Left")
                {
                    //                                             Hide Button
                    if (PS2Mouse.X > X && PS2Mouse.X < X + Width - (BarHeight) && PS2Mouse.Y > Y - BarHeight && PS2Mouse.Y < Y && !Move && !System.IsMovingWindow)
                    {
                        Move = true;
                        System.IsMovingWindow = true;

                        OffsetX = PS2Mouse.X - X;
                        OffsetY = PS2Mouse.Y - Y;

                        //
                        System.ActiveWindowIndex = System.Windows.IndexOf(this);
                    }
                }
                else
                {
                    Move = false;
                    System.IsMovingWindow = false;
                }

                if (Move)
                {
                    this.X = Math.Clamp(PS2Mouse.X - OffsetX, 0, Boot.ScreenWidth - Width);
                    this.Y = Math.Clamp(PS2Mouse.Y - OffsetY, BarHeight, Boot.ScreenHeight - Height - 0 - Dock.DockHeight);
                }

                System.Graphics.SetLimit(X, Y - BarHeight, Width, BarHeight + Height);

                //Bar
                //0x5B264D

                if(Actived)
                {
                    System.Graphics.DrawFilledRectangle(0x73206C, X + 0, (Y - BarHeight) + 5, Width, BarHeight - 5);
                    System.Graphics.DrawFilledRoundedRectangle(0x73206C, X + 0, (Y - BarHeight) + 0, Width, BarHeight, 5);
                }
                else 
                {
                    System.Graphics.DrawFilledRectangle(0x5B264D, X + 0, (Y - BarHeight) + 5, Width, BarHeight - 5);
                    System.Graphics.DrawFilledRoundedRectangle(0x5B264D, X + 0, (Y - BarHeight) + 0, Width, BarHeight, 5);
                }
                System.Graphics.DrawBitFontString("宋体CustomCharset16", 0xFFFFFFFF, Title, X + (BarHeight / 2) - (16 / 2), Y - BarHeight + (BarHeight / 2) - (16 / 2));

                //Hide
                //System.Graphics.DrawFilledRectangle(0x313131, X + Width - BarHeight, Y - BarHeight, BarHeight, BarHeight);
            }
            else
            {
                InputUpdate();
            }

            System.Graphics.DrawFilledRectangle(0xFFFFFFFF, X, Y, Width, Height);

            if (Actived)
            {
                InputUpdate();
            }

            UIUpdate();


            System.Graphics.ResetLimit();
        }

        public virtual void UIUpdate()
        {
        }

        public virtual void InputUpdate()
        {
        }

        public virtual void SetVisible(bool Visible)
        {
            this.Visible = Visible;
        }

        public void Exit()
        {
            System.Windows.Remove(this);
        }
    }
}
