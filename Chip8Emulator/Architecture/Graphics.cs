using System;

namespace Chip8Emulator.Architecture
{
    public class Graphics
    {
        public const int ScreenWidth = 64, ScreenHeight = 32;
        private bool[,] _pixels = new bool[64, 32];

        public void SetPixel(int x, int y, bool value)
        {
            _pixels[x, y] = value;
        }

        public void ClearPixel(int x, int y)
        {
            _pixels[x, y] = false;
        }

        public bool Pixel(int x, int y)
        {
            return _pixels[x, y];
        }

        public void Reset()
        {
            Array.Clear(_pixels, 0, _pixels.Length);
        }
    }
}
