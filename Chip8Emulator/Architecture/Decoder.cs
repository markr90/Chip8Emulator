
using System;
using System.Collections.Generic;

namespace Chip8Emulator.Architecture
{
    public delegate void Instruction(OpCode opcode, CPU cpu);
    public class Decoder
    {
        private Dictionary<byte, Instruction> OpCodes = new Dictionary<byte, Instruction>();
        private Dictionary<byte, Instruction> OpCodesMisc = new Dictionary<byte, Instruction>();

        public Decoder()
        {
            SetOpCodes();
            SetOpCodesMisc();
        }

        private void SetOpCodes()
        {
            OpCodes.Add(0x0, Operations.ClearOrReturn);
            OpCodes.Add(0x1, Operations.Jump);
            OpCodes.Add(0x2, Operations.CallSubroutine);
            OpCodes.Add(0x3, Operations.SkipIfXEqual);
            OpCodes.Add(0x4, Operations.SkipIfXNotEqual);
            OpCodes.Add(0x5, Operations.SkipIfXEqualsY);
            OpCodes.Add(0x6, Operations.SetX);
            OpCodes.Add(0x7, Operations.AddX);
            OpCodes.Add(0x8, Operations.GetArithmetic);
            OpCodes.Add(0x9, Operations.SkipIfXNotEqualsY);
            OpCodes.Add(0xA, Operations.SetI);
            OpCodes.Add(0xB, Operations.JumpWithOffset);
            OpCodes.Add(0xC, Operations.SetXRandom);
            OpCodes.Add(0xD, Operations.DrawSprite);
            OpCodes.Add(0xE, Operations.SkipOnKey);
        }

        private void SetOpCodesMisc()
        {
            OpCodesMisc.Add(0x07, Operations.SetXToDelay);
            OpCodesMisc.Add(0x0A, Operations.WaitForKey);
            OpCodesMisc.Add(0x15, Operations.SetDelayTimer);
            OpCodesMisc.Add(0x18, Operations.SetSoundTimer);
            OpCodesMisc.Add(0x1E, Operations.AddToMemAddress);
            OpCodesMisc.Add(0x29, Operations.SetItoSpriteAddress);
            OpCodesMisc.Add(0x33, Operations.StoreBCD);
            OpCodesMisc.Add(0x55, Operations.RegDump);
            OpCodesMisc.Add(0x65, Operations.RegLoad);
        }

        public Instruction FetchInstruction(OpCode opcode)
        {
            byte classifier = (byte)((opcode.Code & 0xF000) >> 12);

            try
            {
                if (classifier != 0xF)
                    return OpCodes[classifier];
                else
                    return OpCodesMisc[opcode.NN];
            }
            catch (KeyNotFoundException)
            {
                throw new InvalidOperationException(string.Format("INVALID_OP_CODE: 0x{0:x4}", opcode.Code));
            }
        }
    }
}
