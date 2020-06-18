using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chip8Emulator.Architecture
{
    public class OpCode
    {
        public readonly ushort Code;
        public readonly ushort NNN;
        public readonly byte NN, X, Y, N;

        public OpCode(ushort opcode)
        {
            Code = opcode;
            NNN = (ushort)(opcode & 0x0FFF);
            NN = (byte)(opcode & 0x00FF);
            N = (byte) (opcode & 0x000F);
            X = (byte)((opcode & 0x0F00) >> 8); // bitshift to get higher nibbles
            Y = (byte)((opcode & 0x00F0) >> 4); // bitshift to get higher nibbles
        }
    }
}
