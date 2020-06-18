using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chip8Emulator.Architecture
{
    static class Arithmetic
    {
        public static void SetXtoY(OpCode opcode, CPU cpu)
        {
            byte y = cpu.RegisterBank.Get(opcode.Y);
            cpu.RegisterBank.Set(opcode.X, y);
        }

        public static void SetXtoXorY(OpCode opcode, CPU cpu)
        {
            byte x = cpu.RegisterBank.Get(opcode.X);
            byte y = cpu.RegisterBank.Get(opcode.Y);
            cpu.RegisterBank.Set(opcode.X, (byte) (x | y));
        }

        public static void SetXtoXandY(OpCode opcode, CPU cpu)
        {
            byte x = cpu.RegisterBank.Get(opcode.X);
            byte y = cpu.RegisterBank.Get(opcode.Y);
            cpu.RegisterBank.Set(opcode.X, (byte)(x & y));
        }

        public static void SetXtoXxorY(OpCode opcode, CPU cpu)
        {
            byte x = cpu.RegisterBank.Get(opcode.X);
            byte y = cpu.RegisterBank.Get(opcode.Y);
            cpu.RegisterBank.Set(opcode.X, (byte)(x ^ y));
        }

        public static void XplusY(OpCode opcode, CPU cpu)
        {
            byte x = cpu.RegisterBank.Get(opcode.X);
            byte y = cpu.RegisterBank.Get(opcode.Y);
            int result = x + y;

            // Set carry flag
            if (result > 0xFF)
                cpu.RegisterBank.Set(Register.VF, 1);
            else
                cpu.RegisterBank.Set(Register.VF, 0);

            cpu.RegisterBank.Set(opcode.X, (byte)result);
        }

        public static void XsubY(OpCode opcode, CPU cpu)
        {
            byte x = cpu.RegisterBank.Get(opcode.X);
            byte y = cpu.RegisterBank.Get(opcode.Y);
            int result = x - y;

            // Set carry flag
            if (x > y)
                cpu.RegisterBank.Set(Register.VF, 1);
            else
                cpu.RegisterBank.Set(Register.VF, 0);

            cpu.RegisterBank.Set(opcode.X, (byte)result);
        }

        public static void StoreLeastSignificantBit(OpCode opcode, CPU cpu)
        {
            byte x = cpu.RegisterBank.Get(opcode.X);
            int flag = x & 0x1;
            cpu.RegisterBank.Set(Register.VF, (byte)flag);
            x = (byte) (x / 2);
            cpu.RegisterBank.Set(opcode.X, x);
        }

        public static void XisYsubX(OpCode opcode, CPU cpu)
        {
            byte x = cpu.RegisterBank.Get(opcode.X);
            byte y = cpu.RegisterBank.Get(opcode.Y);
            int result = y - x;

            // Set carry flag
            if (y > x)
                cpu.RegisterBank.Set(Register.VF, 1);
            else
                cpu.RegisterBank.Set(Register.VF, 0);

            cpu.RegisterBank.Set(opcode.X, (byte)result);
        }

        public static void StoreMostSignificantBit(OpCode opcode, CPU cpu)
        {
            byte x = cpu.RegisterBank.Get(opcode.X);
            int flag = ((x & (1 << 7)) != 0) ? 1 : 0;
            cpu.RegisterBank.Set(Register.VF, (byte)flag);
            x = (byte)(x * 2);
            cpu.RegisterBank.Set(opcode.X, x);
        }
    }
}
