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
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            if (opcode.NN == 0xE0)
            {
                cpu.Graphics.Reset();
            }
            else if (opcode.NN == 0xEE)
            {
                cpu.Jump(cpu.Pop());
            }
        }

        public static void Jump(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            cpu.Jump(opcode.NNN);
        }

        public static void JumpWithOffset(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            cpu.Jump((ushort)(opcode.NNN + cpu.RegisterBank.Get(V0)));
        }

        public static void CallSubroutine(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            cpu.Push(cpu.PC);
            cpu.Jump(opcode.NNN);
        }

        public static void SkipIfXEqual(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            if (cpu.RegisterBank.Get(opcode.X) == opcode.NN)
            {
                cpu.SkipInstruction();
            }
        }

        public static void SkipIfXNotEqual(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            if (cpu.RegisterBank.Get(opcode.X) != opcode.NN)
            {
                cpu.SkipInstruction();
            }
        }

        public static void SkipIfXEqualsY(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            if (cpu.RegisterBank.Get(opcode.X) == cpu.RegisterBank.Get(opcode.Y))
            {
                cpu.SkipInstruction();
            }
        }

        public static void SkipIfXNotEqualsY(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            if (cpu.RegisterBank.Get(opcode.X) != cpu.RegisterBank.Get(opcode.Y))
            {
                cpu.SkipInstruction();
            }
        }

        public static void SetX(OpCode opcode, CPU cpu)
        {
            cpu.RegisterBank.Set(opcode.X, opcode.NN);
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
            //Console.WriteLine("X = 0x{0:x2} (0x{1:x2})", cpu.RegisterBank.Get(opcode.X), opcode.NN);
#endif
        }

        public static void AddX(OpCode opcode, CPU cpu)
        {
            byte x = cpu.RegisterBank.Get(opcode.X);
            byte result = (byte)(x + opcode.NN);
            cpu.RegisterBank.Set(opcode.X, result);
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
            Console.WriteLine("X = {0} = X ({1}) + NN ({2})", cpu.RegisterBank.Get(opcode.X), x, opcode.NN);
#endif
        }

        public static void SetI(OpCode opcode, CPU cpu)
        {
            cpu.SetI(opcode.NNN);
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
        }

        public static void SetXRandom(OpCode opcode, CPU cpu)
        {
            cpu.RegisterBank.Set(opcode.X, (byte)(NextRandom() & opcode.NN));
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
        }

        public static void DrawSprite(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            byte startx = cpu.RegisterBank.Get(opcode.X);
            byte starty = cpu.RegisterBank.Get(opcode.Y);
            byte n = opcode.N;

            cpu.RegisterBank.Set(Register.VF, 0);
            for (byte i = 0; i < n; i++)
            {
                var spriteLine = cpu.Memory.Read((ushort)(cpu.I + i));

                for (var bit = 0; bit < 8; bit++)
                {
                    var x = (startx + bit) % GraphicsProcessor.ScreenWidth;
                    var y = (starty + i) % GraphicsProcessor.ScreenHeight;

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
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
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
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
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

        public static void SetXToDelay(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            cpu.RegisterBank.Set(opcode.X, cpu.Timer.GetDelay());
        }

        public static void WaitForKey(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            if (cpu.AnyKeyPressed())
            {
                byte key = cpu.GetKeyPress();
                cpu.RegisterBank.Set(opcode.X, key);
            }
            else
            {
                cpu.JumpBackOneInstruction();
            }
        }

        public static void SetDelayTimer(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            byte x = cpu.RegisterBank.Get(opcode.X);
            cpu.Timer.SetDelay(x);
        }

        public static void SetSoundTimer(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            byte x = cpu.RegisterBank.Get(opcode.X);
            cpu.Timer.SetSound(x);
        }

        public static void AddToMemAddress(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            byte x = cpu.RegisterBank.Get(opcode.X);
            cpu.SetI((ushort) (cpu.I + x));
        }

        public static void SetItoSpriteAddress(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
            byte x = cpu.RegisterBank.Get(opcode.X);
            cpu.SetI((ushort) (x * 5));
        }

        public static void StoreBCD(OpCode opcode, CPU cpu)
        {
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
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
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
        }

        public static void RegLoad(OpCode opcode, CPU cpu)
        {
            ushort addressStart = cpu.I;
            for (byte i = 0; i <= opcode.X; i++)
            {
                cpu.RegisterBank.Set(i, cpu.Memory.Read((ushort)(addressStart + i)));
            }
#if DEBUG
            DebugInfo(opcode, System.Reflection.MethodBase.GetCurrentMethod().Name);
#endif
        }

        private static void DebugInfo(OpCode opcode, string mnemonic)
        {
            Console.WriteLine("OP 0x{0:x4} : {1}", opcode.Code, mnemonic);
        }
    }
}
