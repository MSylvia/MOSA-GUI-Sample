using Mosa.External.x86.Drawing;
using MOSA1.Core;
using MOSA1.Drawing;
using MOSA1.Driver;
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

        public bool HasAnimation = false;

        public int X_Desktop;
        public int Y_Desktop;

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
                        System.ActiveWindowIndex = System.Windows.GetWindowIndex(this);
                    }

                    if (PS2Mouse.X > X + Width - BarHeight && PS2Mouse.X < X + Width && PS2Mouse.Y > Y - BarHeight && PS2Mouse.Y < Y && !Move)
                    {
                        this.Visible = false;
                    }
                }
                else
                {
                    Move = false;
                    System.IsMovingWindow = false;
                }

                if (Move)
                {
                    this.X = Math.Clamp(PS2Mouse.X - OffsetX, 0, Boot.ScreenWidth - Width - 1);
                    this.Y = Math.Clamp(PS2Mouse.Y - OffsetY, BarHeight, Boot.ScreenHeight - Height - 1);
                }

                System.Graphics.SetLimit(X, Y - BarHeight, Width, BarHeight + Height);

                //Bar
                System.Graphics.DrawFilledRectangle(0x73206C, X, Y - BarHeight, Width - BarHeight, BarHeight);

                if (System.ActiveWindowIndex == System.Windows.GetWindowIndex(this))
                {
                    System.Graphics.DrawACS16(0xFFFFFFFF, "(Active)" + Title, X + (BarHeight / 2) - (16 / 2), Y - BarHeight + (BarHeight / 2) - (16 / 2));
                }
                else
                {
                    System.Graphics.DrawACS16(0xFFFFFFFF, Title, X + (BarHeight / 2) - (16 / 2), Y - BarHeight + (BarHeight / 2) - (16 / 2));
                }

                //Hide
                System.Graphics.DrawFilledRectangle(0x313131, X + Width - BarHeight, Y - BarHeight, BarHeight, BarHeight);
            }
            else 
            {
                InputUpdate();
            }

            if (!HasAnimation) 
            {
                System.Graphics.DrawFilledRectangle(0xFFFFFFFF, X, Y, Width, Height);
            }

            if (System.ActiveWindowIndex == System.Windows.GetWindowIndex(this))
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
