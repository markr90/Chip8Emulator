
using System.Collections.Generic;
using System.Windows.Forms;


namespace Chip8Emulator.Games
{
    class Invaders : IGame
    {
        private Dictionary<Keys, byte> keyMap = new Dictionary<Keys, byte>();

        public Invaders()
        {
            InitKeyMap();
        }

        public Dictionary<Keys, byte> GetKeyOverride()
        {
            return keyMap;
        }

        public byte[] RomData()
        {
            return GameDataLoader.LoadRomFromAssembly("Chip8Emulator.Roms.INVADERS");
        }

        private void InitKeyMap()
        {
            keyMap.Add(Keys.Space, 0x5);
            keyMap.Add(Keys.Left, 0x4);
            keyMap.Add(Keys.Right, 0x6);
        }
    }
}
