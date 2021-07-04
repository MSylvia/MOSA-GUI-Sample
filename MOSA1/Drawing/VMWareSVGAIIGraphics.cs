using Mosa.External;

namespace MOSA1.Drawing
{
    public class VMWareSVGAIIGraphics : Graphics
    {
        VMWareSVGAII vMWareSVGAII;

        public VMWareSVGAIIGraphics(int Width, int Height)
        {
            vMWareSVGAII = new VMWareSVGAII();
            vMWareSVGAII.SetMode((uint)Width, (uint)Height);
            base.Width = Width;
            base.Height = Height;
        }

        public override void DrawFilledRectangle(uint Color, int X, int Y, int Width, int Height)
        {
            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    DrawPoint(Color, X + w, Y + h);
                }
            }
        }

        public override void DrawPoint(uint Color, int X, int Y)
        {
            if (X > LimitX && X < LimitX + LimitWidth && Y > LimitY && Y < LimitY + LimitHeight)
            {
                vMWareSVGAII.Video_Memory.Write32((uint)(FrameSize + ((Width * Y + X) * Bpp)), Color);
            }
        }

        public override void Update()
        {
            uint addr = vMWareSVGAII.Video_Memory.Address.ToUInt32();
            for (int i = 0; i < FrameSize; i++)
            {
                vMWareSVGAII.Video_Memory.Write8((uint)i, vMWareSVGAII.Video_Memory.Read8((uint)(FrameSize + i)));
            }
            vMWareSVGAII.Update();
        }

        public override void Clear(uint Color)
        {
        }

        public override void Disable()
        {
            vMWareSVGAII.Disable();
        }

        public override void DrawRectangle(uint Color, int X, int Y, int Width, int Height, int Weight)
        {
            DrawFilledRectangle(Color, X, Y, Width, Weight);

            DrawFilledRectangle(Color, X, Y, Weight, Height);
            DrawFilledRectangle(Color, X + (Width - Weight), Y, Weight, Height);

            DrawFilledRectangle(Color, X, Y + (Height - Weight), Width, Weight);
        }
    }
}
