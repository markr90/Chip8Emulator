using System.Reflection;

namespace Chip8Emulator.Games
{
    static class GameDataLoader
    {
        public static byte[] LoadRomFromAssembly(string assemblyFileName)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(assemblyFileName))
            {
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                return data;
            }
        }
    }
}
