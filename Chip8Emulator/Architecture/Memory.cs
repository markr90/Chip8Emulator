using System;

namespace Chip8Emulator.Architecture
{
    public class Memory
    {
        private byte[] _memory;

        public Memory(int size)
        {
            _memory = new byte[size];
        }

        public void Write(ushort address, byte b)
        {
            try
            {
                _memory[address] = b;
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException(string.Format("INVALID_MEM_ADDRESS: 0x{0:x4}", address));
            }
        }

        public byte Read(ushort address)
        {
            try
            {
                return _memory[address];
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException(string.Format("INVALID_MEM_ADDRESS: 0x{0:x4}", address));
            }
        }

        public void Clear()
        {
            Array.Clear(_memory, 0, _memory.Length);
        }

        public void DumpConsole()
        {
            for (int i = 0x200; i < _memory.Length; i++)
            {
                Console.WriteLine("{0}: {1}", i, Read((ushort) i));
            }
        }

    }
}
