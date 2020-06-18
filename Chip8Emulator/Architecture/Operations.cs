using System;
using System.Diagnostics;
using static Chip8Emulator.Architecture.Register;

namespace Chip8Emulator.Architecture
{
    static class Operations
    {
        private static Random rnd = new Random();
        private static byte NextRandom()
        {
            return (byte)(rnd.Next(0, 256));
        }

        public static void ClearOrReturn(OpCode opcode, CPU cpu)
        {
            if (opcode.NN == 0xE0)
            {
                cpu.Graphics.Reset();
            }
            else if (opcode.NN == 0xEE)
            {
                cpu.Jump(cpu.Pop());
            }
        }

        public static void Jump(OpCode opCode, CPU cpu)
        {
            cpu.Jump(opCode.NNN);
        }

        public static void JumpWithOffset(OpCode opCode, CPU cpu)
        {
            cpu.Jump((ushort)(opCode.NNN + cpu.RegisterBank.Get(V0)));
        }

        public static void CallSubroutine(OpCode opcode, CPU cpu)
        {
            cpu.Push(cpu.PC);
            cpu.Jump(opcode.NNN);
        }

        public static void SkipIfXEqual(OpCode opcode, CPU cpu)
        {
            if (cpu.RegisterBank.Get(opcode.X) == opcode.NN)
            {
                cpu.SkipInstruction();
            }
        }

        public static void SkipIfXNotEqual(OpCode opcode, CPU cpu)
        {
            if (cpu.RegisterBank.Get(opcode.X) != opcode.NN)
            {
                cpu.SkipInstruction();
            }
        }

        public static void SkipIfXEqualsY(OpCode opcode, CPU cpu)
        {
            if (cpu.RegisterBank.Get(opcode.X) == cpu.RegisterBank.Get(opcode.Y))
            {
                cpu.SkipInstruction();
            }
        }

        public static void SkipIfXNotEqualsY(OpCode opcode, CPU cpu)
        {
            if (cpu.RegisterBank.Get(opcode.X) != cpu.RegisterBank.Get(opcode.Y))
            {
                cpu.SkipInstruction();
            }
        }

        public static void SetX(OpCode opcode, CPU cpu)
        {
            cpu.RegisterBank.Set(opcode.X, opcode.NN);
        }

        public static void AddX(OpCode opcode, CPU cpu)
        {
            byte result = (byte)(cpu.RegisterBank.Get(opcode.X) + opcode.NN);
            cpu.RegisterBank.Set(opcode.X, result);
        }

        public static void SetI(OpCode opCode, CPU cpu)
        {
            cpu.SetI(opCode.NNN);
        }

        public static void SetXRandom(OpCode opcode, CPU cpu)
        {
            cpu.RegisterBank.Set(opcode.X, NextRandom());
        }

        public static void DrawSprite(OpCode opcode, CPU cpu)
        {
            byte startx = cpu.RegisterBank.Get(opcode.X);
            byte starty = cpu.RegisterBank.Get(opcode.Y);
            byte n = cpu.RegisterBank.Get(opcode.N);

            for (byte i = 0; i < n; i++)
            {
                var spriteLine = cpu.Memory.Read((ushort)(cpu.I + i));

                for (var bit = 0; bit < 8; bit++)
                {
                    var x = (startx + bit) % Graphics.ScreenWidth;
                    var y = (starty + i) % Graphics.ScreenHeight;

                    bool spriteBit = ((spriteLine >> (7 - bit)) & 1) != 0;
                    bool curBit = cpu.Graphics.Pixel(x, y);
                    bool newBit = spriteBit ^ curBit; // new bit is XOR of cur and spritebit

                    cpu.Graphics.SetPixel(x, y, newBit);

                    if (curBit && !newBit)
                        cpu.RegisterBank.Set(Register.VF, 1);

                }
            }

        }

        public static void SkipOnKey(OpCode opcode, CPU cpu)
        {
            byte x = cpu.RegisterBank.Get(opcode.X);
            if (opcode.NN == 0x9E)
            {
                if (cpu.IsKeyPressed(x))
                    cpu.SkipInstruction();
            }
            if (opcode.NN == 0xA1)
            {
                if (!cpu.IsKeyPressed(x))
                    cpu.SkipInstruction();
            }
        }

        public static void GetArithmetic(OpCode opcode, CPU cpu)
        {
            switch (opcode.N)
            {
                case 0x0:
                    Arithmetic.SetXtoY(opcode, cpu);
                    break;
                case 0x1:
                    Arithmetic.SetXtoXorY(opcode, cpu);
                    break;
                case 0x2:
                    Arithmetic.SetXtoXandY(opcode, cpu);
                    break;
                case 0x3:
                    Arithmetic.SetXtoXxorY(opcode, cpu);
                    break;
                case 0x4:
                    Arithmetic.XplusY(opcode, cpu);
                    break;
                case 0x5:
                    Arithmetic.XsubY(opcode, cpu);
                    break;
                case 0x6:
                    Arithmetic.StoreLeastSignificantBit(opcode, cpu);
                    break;
                case 0x7:
                    Arithmetic.XisYsubX(opcode, cpu);
                    break;
                case 0xE:
                    Arithmetic.StoreMostSignificantBit(opcode, cpu);
                    break;
                default:
                    throw new InvalidOperationException(string.Format("INVALID_OP_CODE: {0}", opcode.Code));
            }
        }

        public static void Misc(OpCode opcode, CPU cpu)
        {
            switch (opcode.NN)
            {
                case 0x07:
                    SetXToDelay(opcode, cpu);
                    break;
                case 0x0A:
                    WaitForKey(opcode, cpu);
                    break;
                case 0x15:
                    SetDelayTimer(opcode, cpu);
                    break;
                case 0x18:
                    SetSoundTimer(opcode, cpu);
                    break;
                case 0x1E:
                    AddToMemAddress(opcode, cpu);
                    break;
                case 0x29:
                    SetItoSpriteAddress(opcode, cpu);
                    break;
                case 0x33:
                    StoreBCD(opcode, cpu);
                    break;
                case 0x55:
                    RegDump(opcode, cpu);
                    break;
                case 0x65:
                    RegLoad(opcode, cpu);
                    break;
                default:
                    throw new InvalidOperationException(string.Format("INVALID_OP_CODE: {0}", opcode.Code));
            }
        }

        public static void SetXToDelay(OpCode opcode, CPU cpu)
        {
            cpu.RegisterBank.Set(opcode.X, cpu.Timer.GetDelay());
        }

        public static void WaitForKey(OpCode opcode, CPU cpu)
        {
            byte key = cpu.WaitForKeyPress();
            cpu.RegisterBank.Set(opcode.X, key);
        }

        public static void SetDelayTimer(OpCode opcode, CPU cpu)
        {
            byte x = cpu.RegisterBank.Get(opcode.X);
            cpu.Timer.SetDelay(x);
        }

        public static void SetSoundTimer(OpCode opcode, CPU cpu)
        {
            byte x = cpu.RegisterBank.Get(opcode.X);
            cpu.Timer.SetSound(x);
        }

        public static void AddToMemAddress(OpCode opCode, CPU cpu)
        {
            byte x = cpu.RegisterBank.Get(opCode.X);
            cpu.SetIRelative(x);
        }

        public static void SetItoSpriteAddress(OpCode opcode, CPU cpu)
        {
            byte x = cpu.RegisterBank.Get(opcode.X);
            cpu.SetI((ushort) (x * 5));
        }

        public static void StoreBCD(OpCode opcode, CPU cpu)
        {
            ushort address = cpu.I;
            byte x = cpu.RegisterBank.Get(opcode.X);
            cpu.Memory.Write((ushort) (address + 0), (byte)((x / 100) % 10));
            cpu.Memory.Write((ushort) (address + 1), (byte)((x / 10) % 10));
            cpu.Memory.Write((ushort) (address + 2), (byte)(x % 10));
        }

        public static void RegDump(OpCode opcode, CPU cpu)
        {
            ushort addressStart = cpu.I;
            for (byte i = 0; i <= opcode.X; i++)
            {
                cpu.Memory.Write((ushort)(addressStart + i), cpu.RegisterBank.Get(i));
            }
        }

        public static void RegLoad(OpCode opcode, CPU cpu)
        {
            ushort addressStart = cpu.I;
            for (byte i = 0; i <= opcode.X; i++)
            {
                cpu.RegisterBank.Set(i, cpu.Memory.Read((ushort)(addressStart + i)));
            }
        }
    }
}
