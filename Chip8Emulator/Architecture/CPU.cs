using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chip8Emulator.Architecture
{
    public class CPU
    {
        public readonly RegisterBank RegisterBank = new RegisterBank();
        public readonly Graphics Graphics = new Graphics();
        public readonly Memory Memory = new Memory(4096);
        private readonly ushort[] Stack = new ushort[16];
        private HashSet<byte> pressedKeys = new HashSet<byte>(16);
        public readonly Timer Timer = new Timer();

        private bool _isRunning = false;
        private bool _halted;
        public byte SP;   
        public ushort PC { get; private set; } // program counter
        public ushort I { get; private set; } // address register

        private void RunCycle()
        {
            // Execute OpCode

            // Draw graphics

            // Store key press
        }

        private void ExecuteOpCode()
        {
            // Fetch opcode

            // Decode opcode

            // Execute opcode

            // Update timers
        }

        private OpCode FetchOpCode()
        {
            // Fetch opcode

            // Return opcode

            throw new NotImplementedException();
        }

        public void Run()
        {
            _isRunning = true;
            while (_isRunning)
            {
                RunCycle();
            }
        }

        public void LoadRom()
        {

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
            Stack[SP++] = value;
        }

        public ushort Pop()
        {
            return Stack[--SP];
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

        public byte WaitForKeyPress()
        {
            while (pressedKeys.Count == 0)
            {
                _halted = true;
            }
            _halted = false;
            return pressedKeys.First();
        }
    }
}
