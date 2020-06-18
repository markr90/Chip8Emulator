

namespace Chip8Emulator.Architecture
{
    public class RegisterBank
    {
        private byte[] _registers = new byte[16];

        public byte Get(byte address)
        {
            return _registers[address];
        }
        public void Set(byte address, byte value)
        {
            _registers[address] = value;
        }

        public byte Get(Register r)
        {
            return _registers[(int) r];
        }

        public void Set(Register r, byte b)
        {
            _registers[(int) r] = b;
        }
    }

    public enum Register
    {
        V0, V1, V2, V3, V4, V5, V6, V7, V8, V9, VA, VB, VC, VD, VE, VF
    }
}
