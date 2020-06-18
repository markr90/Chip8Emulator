
using System.Collections.Generic;
using System.Windows.Forms;

namespace Chip8Emulator.Games
{
    public interface IGame
    {
        Dictionary<Keys, byte> GetKeyOverride();
        byte[] RomData();
    }
}
