using Chip8Emulator.Core;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Chip8Emulator.Architecture
{
    public class GraphicsProcessor
    {
        public const int ScreenWidth = 64, ScreenHeight = 32;
        private bool[,] _pixels;
        private Color _color = Color.FromArgb(255, 0, 255, 0);
        private Color _bgColor = Color.Black;

        public GraphicsProcessor()
        {
            _pixels = new bool[64, 32];
        }

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

        public void PrintConsole()
        {
            string s = "";
            for (int y = 0; y < ScreenHeight; y++)
            {
                for (int x = 0; x < ScreenWidth; x++)
                {
                    s += _pixels[x, y] ? "1" : "0";
                }
                s += System.Environment.NewLine;
            }

            Console.WriteLine(s);
        }

        public Bitmap Draw()
        {
            Bitmap screen = new Bitmap(ScreenWidth, ScreenHeight);
            for (var y = 0; y < screen.Height; y++)
            {
                for (var x = 0; x < screen.Width; x++)
                {
                    if (_pixels[x, y])
                        screen.SetPixel(x, y, _color);
                    else
                        screen.SetPixel(x, y, _bgColor);
                }
            }
            return screen;
        }

    }
}
