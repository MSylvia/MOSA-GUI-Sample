using Mosa.External;
using Mosa.Kernel.x86;
using Mosa.Runtime;

namespace MOSAOnRealHardware
{
    class VBEDriver
    {
		public MemoryBlock Video_Memory;

        public VBEDriver()
        {
			Video_Memory = GetPhysicalMemory(VBE.MemoryPhysicalLocation, (uint)(VBE.ScreenWidth * VBE.ScreenHeight * (VBE.BitsPerPixel / 8)));
        }

		public MemoryBlock GetPhysicalMemory(Pointer address, uint size)
		{
			var start = (uint)address.ToInt32();

			// Map physical memory space to virtual memory space
			for (var at = start; at < start + size; at += PageFrameAllocator.PageSize)
			{
				PageTable.MapVirtualAddressToPhysical(at, at);
			}

			return new MemoryBlock(address, size);
		}
	}
}
