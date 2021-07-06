using Mosa.External;
using Mosa.Runtime.x86;
using System;

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

        public override void DrawPoint(uint Color, int X, int Y)
        {
            if (X >= LimitX && X <= LimitX + LimitWidth && Y > LimitY && Y < LimitY + LimitHeight)
            {
                vMWareSVGAII.Video_Memory.Write32((uint)(FrameSize + ((Width * Y + X) * Bpp)), Color);
            }
        }

        public unsafe override void Update()
        {
            uint addr = vMWareSVGAII.Video_Memory.Address.ToUInt32();
            for (int i = 0; i < FrameSize; i++)
            {
                Native.Set8((uint)(addr + i), Native.Get8((uint)(addr + FrameSize + i)));
            }
            vMWareSVGAII.Update();
        }

        public override void Clear(uint Color)
        {
            throw new NotImplementedException();
        }

        public override void Disable()
        {
            vMWareSVGAII.Disable();
        }
    }
}
