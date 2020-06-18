
using System.Collections.Generic;
using System.Windows.Forms;

namespace Chip8Emulator.Games
{
    class Brix : IGame
    {
        private Dictionary<Keys, byte> keyMap = new Dictionary<Keys, byte>();

        public Brix()
        {
            InitKeyMap();
        }

        public Dictionary<Keys, byte> GetKeyOverride()
        {
            return keyMap;
        }

        public byte[] RomData()
        {
            return GameDataLoader.LoadRomFromAssembly("Chip8Emulator.Roms.BRIX");
        }

        private void InitKeyMap()
        {
            keyMap.Add(Keys.Left, 0x4);
            keyMap.Add(Keys.Right, 0x6);
        }
    }
}