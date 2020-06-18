
using System.Collections.Generic;

namespace Chip8Emulator.Architecture
{
    public delegate void Operation(OpCode opcode, CPU cpu);
    public class Decoder
    {
        private Dictionary<byte, Operation> OpCodes = new Dictionary<byte, Operation>();
        private Dictionary<byte, Operation> OpCodesMisc = new Dictionary<byte, Operation>();

        public Decoder()
        {
            SetOpCodes();
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
            OpCodes.Add(0xF, Operations.Misc);
        }
    }
}
