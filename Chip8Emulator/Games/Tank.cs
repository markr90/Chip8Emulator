
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Chip8Emulator.Games
{
    class Tank: IGame
    {
        private Dictionary<Keys, byte> keyMap = new Dictionary<Keys, byte>();

        public Tank()
        {
            InitKeyMap();
        }

        public Dictionary<Keys, byte> GetKeyOverride()
        {
            return keyMap;
        }

        public byte[] RomData()
        {
            return GameDataLoader.LoadRomFromAssembly("Chip8Emulator.Roms.TANK");
        }

        private void InitKeyMap()
        {
            keyMap.Add(Keys.Space, 0x5);
            keyMap.Add(Keys.Left, 0x4);
            keyMap.Add(Keys.Right, 0x6);
            keyMap.Add(Keys.Down, 0x2);
            keyMap.Add(Keys.Up, 0x8);
        }
    }
}
