using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Chip8Emulator.Architecture
{
    public class CPU
    {
        public readonly RegisterBank RegisterBank = new RegisterBank();
        public readonly GraphicsProcessor Graphics = new GraphicsProcessor();
        public readonly Memory Memory = new Memory(4096);
        private readonly ushort[] Stack = new ushort[16];
        private HashSet<byte> pressedKeys = new HashSet<byte>(16);
        public readonly InternalTimer Timer = new InternalTimer();
        private readonly Decoder Decoder = new Decoder();
        public bool HasRomLoaded { get; private set; }

        private int nCycles;
        private bool _isRunning;

        public byte SP;   
        public ushort PC { get; private set; } // program counter
        public ushort I { get; private set; } // address register

        public CPU(int clockspeed)
        {
            nCycles = clockspeed / InternalTimer.Frequency;
        }

        public void Initialize()
        {
            _isRunning = true;
            PC = 0x200; // PC should start at 0x200 not at 0
            I = 0;
            SP = 0;
            Graphics.Reset();
            Array.Clear(Stack, 0, Stack.Length);
            RegisterBank.Clear();
            Memory.Clear();
            LoadFonts();
        }

        public void RunCycle()
        {

            ExecuteNextInstruction();
        }

        private void ExecuteNextInstruction()
        {
            OpCode opcode = FetchOpCode();
            Instruction instruction;
            lock (opcode)
            {
                instruction = Decoder.FetchInstruction(opcode);
            }
            instruction(opcode, this);
        }

        private OpCode FetchOpCode()
        {
            byte b1 = Memory.Read(PC++);
            byte b2 = Memory.Read(PC++);
            ushort opcode = (ushort)((b1 << 8) | (b2));
            return new OpCode(opcode);
        }

        private void LoadFonts()
        {
            // TODO this could be an issue?
            for (int i = 0; i < Font.Set.Length; i++)
            {
                Memory.Write((ushort)(5 * i + 0), (byte)((Font.Set[i] >> (8 * 4)) & 0xF0));
                Memory.Write((ushort)(5 * i + 1), (byte)((Font.Set[i] >> (8 * 3)) & 0xF0));
                Memory.Write((ushort)(5 * i + 2), (byte)((Font.Set[i] >> (8 * 2)) & 0xF0));
                Memory.Write((ushort)(5 * i + 3), (byte)((Font.Set[i] >> (8 * 1)) & 0xF0));
                Memory.Write((ushort)(5 * i + 4), (byte)((Font.Set[i] >> (8 * 0)) & 0xF0));
            }
        }
        
        public void Stop()
        {
            _isRunning = false;
        }

        public void LoadRom(byte[] rom)
        {
            for (int i = 0; i < rom.Length; i++)
            {
                Memory.Write((ushort)(0x200 + i), rom[i]);
            }
            HasRomLoaded = true;
        }

        public void SkipInstruction()
        {
            PC += 2;
        }

        public void Jump(ushort address)
        {
            PC = address;
        }

        public void Push(ushort value)
        {
            Stack[++SP] = value;
        }

        public ushort Pop()
        {
            return Stack[SP--];
        }

        public void SetI(ushort value)
        {
            I = value;
        }

        public void SetIRelative(ushort value)
        {
            I += value;
        }

        public bool IsKeyPressed(byte key)
        {
            return pressedKeys.Contains(key);
        }

        public void KeyDown(byte key)
        {
            pressedKeys.Add(key);
        }

        public void KeyUp(byte key)
        {
            pressedKeys.Remove(key);
        }

        public bool AnyKeyPressed()
        {
            return pressedKeys.Count > 0;
        }

        public byte GetKeyPress()
        {
            return pressedKeys.First();
        }

        public void JumpBackOneInstruction()
        {
            PC -= 2;
        }
    }
}
